using System;
using System.Windows;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvalonDock;
using AvalonDock.Layout;
using RelertSharp.Common;
using Microsoft.Win32;
using RelertSharp.Engine.Api;

namespace RelertSharp.Wpf
{
    internal static class GuiUtil
    {
        internal static double MonitorScale = 1;
        internal static readonly Color defBackColor = Color.FromArgb(30, 30, 30);
        public static bool SafeRun(Action a, string errorMsg, Action failsafe = null)
        {
#if RELEASE
            try
            {
#endif
            a.Invoke();
            return true;
#if RELEASE
        }
            catch (Exception e)
            {
                if (e.GetType() == typeof(RSException.MixEntityNotFoundException))
                {
                    RSException.MixEntityNotFoundException mx = e as RSException.MixEntityNotFoundException;
                    Fatal(string.Format("{0}\nError message: {1}\nFile name: {3}\n\nTrace:\n{2}", errorMsg, mx.RSMessage, e.StackTrace, mx.FileName));
                }
                else
                {
                    Fatal(string.Format("{0}\nError message: {1}\n\nTrace:\n{2}", errorMsg, e.Message, e.StackTrace));
                }
                try
                {
                    failsafe?.Invoke();
                }
                catch (Exception fail)
                {
                    Fatal(string.Format("Failsafe Error!!\nTrace:\n{0}", fail.StackTrace));
                }
                return false;
            }
#endif
        }


        public static void Fatal(string content)
        {
            MessageBox.Show(content, "Fatal", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void Fatal(string format, params object[] obj)
        {
            Fatal(string.Format(format, obj));
        }
        public static void Warning(string content)
        {
            MessageBox.Show(content, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public static void Warning(string format, params object[] obj)
        {
            Warning(string.Format(format, obj));
        }

        public static void ScaleWpfMousePoint(ref System.Windows.Point p)
        {
            p.X *= MonitorScale * EngineApi.ScaleFactor;
            p.Y *= MonitorScale * EngineApi.ScaleFactor;
        }
    }
}
