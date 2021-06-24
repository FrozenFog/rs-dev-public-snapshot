using RelertSharp.Common;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RelertSharp.Wpf.ToolBoxes
{
    /// <summary>
    /// AnimationPreview.xaml 的交互逻辑
    /// </summary>
    public partial class AnimationPreview : UserControl, IRsView
    {
        private const uint COLOR_DEFAULT = 0xFF252525;
        private const string GLYPH_PLAY = "▶";
        private const string GLYPH_STOP = "■";
        private bool isPlaying = false;
        private bool init = false;
        private string animRegName;
        private int currentFrame = 0, maxFrame = 0;
        private Color bgc;
        private AnimationComponent anim;
        private PalFile pal;
        private Image currentImg = new Image();

        private BackgroundWorker worker;

        public GuiViewType ViewType => GuiViewType.AnimationPreview;
        public AvalonDock.Layout.LayoutAnchorable ParentAncorable { get; set; }
        public AvalonDock.Layout.LayoutDocument ParentDocument { get; set; }

        #region ctor
        public AnimationPreview()
        {
            InitializeComponent();
            worker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true
            };
            canvas.Children.Add(currentImg);
            worker.DoWork += Worker_DoWork;
            Engine.Api.EngineApi.TheaterReloaded += ReloadPalettes;
            bgc = WpfWindowsExtensions.FromArgb(COLOR_DEFAULT);
            NavigationHub.PlayAnimationRequest += LoadAnimation;
        }
        #endregion



        #region Api
        public void ReloadPalettes(object sender, EventArgs e)
        {
            LoadDefaultPalette();
            init = true;
        }
        public void LoadAnimation(string regName)
        {
            anim?.Dispose();
            if (isPlaying)
            {
                StopAnimation();
            }
            if (!init)
            {
                LoadDefaultPalette();
                init = true;
            }
            anim = GlobalVar.GlobalRules.GetAnimByRegName(regName);
            sldProgress.Maximum = anim.FrameCount > 0 ? anim.FrameCount - 1 : 0;
            maxFrame = anim.FrameCount;
            sldProgress.Value = 0;
            animRegName = regName;
        }
        #endregion


        #region Handler
        private void btnBgc_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color c = dlg.Color;
                bgc = Color.FromArgb(c.A, c.R, c.G, c.B);
                btnBgc.Background = new SolidColorBrush(bgc);
                ResetBgc();
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
            {
                btnPlay.Content = GLYPH_PLAY;
                isPlaying = false;
                StopAnimation();
            }
            else
            {
                btnPlay.Content = GLYPH_STOP;
                isPlaying = true;
                PlayAnimation();
            }
        }

        private void PlayerProgressChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!isPlaying)
            {
                currentFrame = (int)e.NewValue;
                SetInfoLabel();
                SetAnimationByFrame();
            }
        }

        private void cbbPalType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = cbbPalType.SelectedItem;
            if (item == null) return;
            string palName = item.ToString();
            if (GlobalVar.GlobalDir.TryGetRawByte(palName, out byte[] data))
            {
                pal = new PalFile(data, palName);
                if (anim != null)
                {
                    anim.ResetBitmap();
                    SetAnimationByFrame();
                }
            }
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            int frameGap = 1000;
            while (isPlaying)
            {
                frameGap = (int)(1000 / GetFrameRate());
                watch.Restart();
                this.Dispatcher.Invoke(() =>
                {
                    if (!anim.IsValid) return;
                    int frame = currentFrame + 1;
                    if (frame > (int)sldProgress.Maximum) frame = 0;
                    sldProgress.Value = frame;
                    currentFrame = frame;
                    SetInfoLabel();
                    anim.SetFrame(currentFrame, bgc.ToGdiColor(), pal);
                    ShowFrame();
                });
                watch.Stop();
                if (watch.ElapsedMilliseconds < frameGap)
                {
                    Thread.Sleep(frameGap - (int)watch.ElapsedMilliseconds);
                }
            }
        }
        #endregion



        #region Private Methods
        private double GetFrameRate()
        {
            double d = 30;
            this.Dispatcher.Invoke(() =>
            {
                if (double.TryParse(txbFrameRate.Text, out double db)) d = db;
            });
            return d;
        }
        private void LoadDefaultPalette()
        {
            cbbPalType.Items.Clear();
            cbbPalType.Items.Add(string.Format("unit{0}.pal", GlobalVar.TileDictionary.TheaterSub));
            cbbPalType.Items.Add(string.Format("iso{0}.pal", GlobalVar.TileDictionary.TheaterSub));
            cbbPalType.Items.Add(string.Format("{0}.pal", GlobalVar.TileDictionary.TheaterSub));
            cbbPalType.Items.Add("anim.pal");
            cbbPalType.SelectedIndex = 0;
        }
        private void PlayAnimation()
        {
            txbFrameRate.IsEnabled = false;
            cbbPalType.IsEnabled = false;
            btnBgc.IsEnabled = false;
            worker.RunWorkerAsync();
        }
        private void SetAnimationByFrame()
        {
            if (!anim.IsValid) return;
            anim.SetFrame(currentFrame, bgc.ToGdiColor(), pal);
            ShowFrame();
        }
        private void StopAnimation()
        {
            worker.CancelAsync();
            txbFrameRate.IsEnabled = true;
            cbbPalType.IsEnabled = true;
            btnBgc.IsEnabled = true;
        }
        private void ResetBgc()
        {
            canvas.Background = new SolidColorBrush(bgc);
            if (anim != null)
            {
                anim.ResetBitmap();
                SetAnimationByFrame();
            }
        }
        private void ShowFrame()
        {
            if (!anim.IsValid) return;
            BitmapImage source = anim.GetResultImage(currentFrame, bgc.ToGdiColor()).ToWpfImage();
            currentImg.Source = source;
            double scale = this.GetScale();
            anim.FrameXY(currentFrame, out int x, out int y);
            anim.FrameWH(currentFrame, out int w, out int h);
            double dx = (canvas.ScaledWidth() - w) / 2 + x;
            double dy = (canvas.ScaledHeight() - h) / 2 + y;
            Canvas.SetLeft(currentImg, dx / scale);
            Canvas.SetTop(currentImg, dy / scale);
        }
        private void SetInfoLabel()
        {
            lblFrameInfo.Content = string.Format("{2} - {0}/{1}", currentFrame, maxFrame, animRegName);
        }
        #endregion
    }
}
