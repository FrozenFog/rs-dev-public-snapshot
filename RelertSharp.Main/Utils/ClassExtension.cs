﻿using RelertSharp.IniSystem;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RelertSharp.Utils
{
    public static class RSPointExtension
    {

    }

    public static class RSRectangleExtension
    {

    }

    public static class HashSetExtension
    {
        public static void AddRange(this HashSet<string> hashset, IEnumerable<string> src)
        {
            foreach (var obj in src)
                hashset.Add(obj);
        }
    }

    public static class DictionaryExtension
    {
        public static Dictionary<string, INIEntity> Clone(this Dictionary<string, INIEntity> src)
        {
            return new Dictionary<string, INIEntity>(src);
        }
    }

    /// <summary>
    /// This class aims to extend some useful functions which some controls don't owns.
    /// Like DataGridView, which does't have BeginUpdate/EndUpdate
    /// </summary>
    public static class ControlExtension
    {
        [DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SendMessage(IntPtr hwnd, uint uMsg, int wParam, int lParam);
        private const int WM_SETREDRAW = 0xB;

        public static void BeginUpdate(this Control control)
        {
            SendMessage(control.Handle, WM_SETREDRAW, 0, 0);
        }

        /// <summary>
        /// callRedraw decides if the control will redraw now
        /// </summary>
        /// <param name="callRedraw"></param>
        public static void EndUpdate(this Control control, bool callRedraw = true)
        {
            SendMessage(control.Handle, WM_SETREDRAW, 1, 0);
            if (callRedraw) control.Refresh();
        }
    }
}
