using RelertSharp.Common;
using RelertSharp.FileSystem;
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
    public partial class AnimationPreview : UserControl
    {
        private const string GLYPH_PLAY = "▶";
        private const string GLYPH_STOP = "■";
        private bool isPlaying = false;
        private bool init = false;
        private string animRegName;
        private int currentFrame = 0, maxFrame = 0;
        private double shpMaxHeight, shpMaxWidth;
        private Color bgc;
        private ShpFile shp;
        private PalFile pal;
        private Image currentImg = new Image();

        private BackgroundWorker worker;

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
            if (!init)
            {
                LoadDefaultPalette();
                init = true;
            }
            shp?.Dispose();
            animRegName = regName + Constant.EX_SHP;
            if (GlobalVar.GlobalDir.TryGetRawByte(animRegName, out byte[] data))
            {
                shp = new ShpFile(data, animRegName);
                shpMaxWidth = shp.Frames.Max(a => a.Width);
                shpMaxHeight = shp.Frames.Max(a => a.Height);
                sldProgress.Maximum = shp.Count;
                maxFrame = shp.Count;
                sldProgress.Value = 0;
            }
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
            string palName = cbbPalType.SelectedItem.ToString();
            if (GlobalVar.GlobalDir.TryGetRawByte(palName, out byte[] data))
            {
                pal = new PalFile(data, palName);
                if (shp != null) SetAnimationByFrame();
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
                    int frame = currentFrame + 1;
                    if (frame >= (int)sldProgress.Maximum) frame = 0;
                    sldProgress.Value = frame;
                    currentFrame = frame;
                    SetInfoLabel();
                    shp.Frames[currentFrame].SetBitmap(pal, bgc.ToGdiColor());
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
            shp.Frames[currentFrame].SetBitmap(pal, bgc.ToGdiColor());
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
            if (shp != null) SetAnimationByFrame();
        }
        private void ShowFrame()
        {
            // frameHeight + dy = CONSTANT
            // CONSTANT = canvasHeight/2 + shpMaxHeight/2
            // dy = (canvasHeight + shpMaxHeight) / 2 - frameHeight
            //
            // dx always center
            // dx = (canvasWidth - frameWidth) / 2
            //canvas.Children.Remove(currentImg);
            BitmapImage source = shp.Frames[currentFrame].Image.ToWpfImage();
            currentImg.Source = source;
            double dx = (canvas.ScaledWidth() - source.PixelWidth) / 2;
            double dy = (canvas.ScaledHeight() + shp.Height) / 2 - source.Height;
            Canvas.SetLeft(currentImg, dx);
            Canvas.SetTop(currentImg, dy);
            //canvas.Children.Add(currentImg);
        }
        private void SetInfoLabel()
        {
            lblFrameInfo.Content = string.Format("{0}/{1}", currentFrame, maxFrame);
        }
        #endregion
    }
}
