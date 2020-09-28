using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;

namespace RelertSharp.GUI
{
    internal static class GuiUtils
    {
        public static TreeNode NewNode(string text, string name)
        {
            TreeNode n = new TreeNode(Language.DICT[text]);
            n.Name = name;
            return n;
        }
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
        public static DialogResult YesNoWarning(string content)
        {
            DialogResult result = MessageBox.Show(content, "Warning", MessageBoxButtons.YesNo);
            return result;
        }
        public static void Warning(string content)
        {
            MessageBox.Show(content, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Warning(string format, params object[] obj)
        {
            Warning(string.Format(format, obj));
        }
        public static void Fatal(string content)
        {
            MessageBox.Show(content, "Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void Fatal(string format, params object[] obj)
        {
            Fatal(string.Format(format, obj));
        }
        public static void Complete(string content)
        {
            MessageBox.Show(content, Language.DICT["RSMainTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        public static void ClearControlContent(params Control[] controls)
        {
            foreach (Control c in controls)
            {
                c.ClearContent();
            }
        }
        public static void GoEnter(KeyEventArgs e, Action a)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                a.Invoke();
            }
        }
    }
}
