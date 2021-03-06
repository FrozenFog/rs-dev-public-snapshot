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
using System.Windows.Input;
using System.IO;

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
                    Fatal(string.Format("{0}\nError message: {1}\nFile name: {3}\n\nTrace:\n{2}", errorMsg, mx.Message, e.StackTrace, mx.FileName));
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
        public static string FormatException(Exception e)
        {
            return string.Format("Error message: {0}\nStack trace:\n{1}", e.Message, e.StackTrace);
        }
        public static void Asterisk(string content)
        {
            MessageBox.Show(content, "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        public static void Fatal(string content, Exception ex)
        {
            Fatal(string.Format("{0}\nException:\n{1}", content, FormatException(ex)));
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
        public static bool YesNoWarning(string content)
        {
            return MessageBox.Show(content, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
        }
        public static MessageBoxResult YesNoCancel(string content)
        {
            return MessageBox.Show(content, "Relert Sharp", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
        }

        public static void ScaleWpfMousePoint(ref System.Windows.Point p)
        {
            p.X *= MonitorScale * EngineApi.ScaleFactor;
            p.Y *= MonitorScale * EngineApi.ScaleFactor;
        }

        public static bool IsKeyDown(params Key[] keys)
        {
            foreach (var k in keys)
            {
                if (Keyboard.IsKeyDown(k)) return true;
            }
            return false;
        }
        public static bool IsControlDown()
        {
            return Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);
        }
        public static bool IsShiftDown()
        {
            return Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
        }
        public static bool IsAltDown()
        {
            return Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt);
        }
        public static bool IsKeyUp(params Key[] keys)
        {
            foreach (var k in keys)
            {
                if (Keyboard.IsKeyDown(k)) return false;
            }
            return true;
        }
        public static long CalcFileSizes(string pathToFind, string fileFilter)
        {
            string[] files = Directory.GetFiles(pathToFind, fileFilter);
            long sum = 0;
            foreach (string name in files)
            {
                FileInfo info = new FileInfo(name);
                sum += info.Length;
            }
            return sum;
        }
        public static void SafeDelete(string filePath)
        {
            if (filePath.IsNullOrEmpty()) return;
            if (File.Exists(filePath)) File.Delete(filePath);
        }
    }
}
