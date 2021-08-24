using RelertSharp.Common;
using RelertSharp.Engine.Api;
using RelertSharp.Wpf.ViewModel;
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
using System.Windows.Shapes;

namespace RelertSharp.Wpf.Dialogs
{
    /// <summary>
    /// DlgLoading.xaml 的交互逻辑
    /// </summary>
    public partial class DlgLoading : Window
    {
        private bool complete;
        private Dictionary<int, LoadingProgressVm> progresses = new Dictionary<int, LoadingProgressVm>();
        private Stopwatch watch = new Stopwatch();
        public DlgLoading()
        {
            InitializeComponent();
            EngineApi.ProgressRegisted += ProgressRegisted;
            EngineApi.DrawingProgressTick += ProgressTicked;
            EngineApi.MapDrawingComplete += DrawingComplete;
            EngineApi.DrawingProgressCompleted += ProgressCompleted;
        }

        private void ProgressCompleted(MapObjectType type)
        {
            ReportProgress((int)type);
        }

        private void DrawingComplete()
        {
            complete = true;
            Thread.Sleep(1500);
            Close();
        }

        private void ProgressTicked(MapObjectType type)
        {
            IncreProgress((int)type);
            if (watch.ElapsedMilliseconds >= 17)
            {
                ReportProgress((int)type);
                watch.Restart();
            }
        }

        private void ProgressRegisted(string label, int maxCount, MapObjectType type)
        {
            RegistProgress((int)type, label, maxCount);
            watch.Start();
        }

        public void RegistProgress(int id, string label, int maxCount)
        {
            Dispatcher.Invoke(() =>
            {
                var vm = new LoadingProgressVm()
                {
                    CurrentCount = 0,
                    MaxCount = maxCount,
                    Label = label,
                    Id = id
                };
                progresses[id] = vm;
                lbxMain.Items.Add(vm);
            });
        }
        public void IncreProgress(int id)
        {
            Dispatcher.Invoke(() => { progresses[id].Incre(); });
        }
        public void ReportProgress(int id)
        {
            Dispatcher.Invoke(() => { progresses[id].Report(); });
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            if (complete) base.OnClosing(e);
            else e.Cancel = true;
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
