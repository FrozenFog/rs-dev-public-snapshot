using System.Collections.Generic;
using System.Linq;
using System.Text;

using ICSharpCode.AvalonEdit.CodeCompletion;

namespace RelertSharp.GUI.SubWindows.WPF.INIEditor.Data
{
    public static class IntellisenseManager
    {
        // 关键字字典
        private static Dictionary<string, string> dictionary = new Dictionary<string, string>();

        static IntellisenseManager()
        {
            dictionary.Add("Key", "键1");
            dictionary.Add("key", "键2");
        }

        public static void ShowCompletionWindow(this ICSharpCode.AvalonEdit.TextEditor editor, string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return;
            // Open code completion after the user has pressed dot:
            var completionWindow = new CompletionWindow(editor.TextArea);
            IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
            data.Clear();
            foreach (var word in dictionary)
            {
                if (word.Key.StartsWith(s))
                {
                    data.Add(new CompletionData(word.Key, s.Length));
                }
            }
            completionWindow.Show();
            completionWindow.Closed += delegate
            {
                completionWindow = null;
            };
        }
    }

    public static class AvalonEditExtension
    {
        public static string GetCursorWord(this ICSharpCode.AvalonEdit.TextEditor editor)
        {
            var locate = editor.TextArea.Document.GetLocation(editor.CaretOffset);
            var line = editor.TextArea.Document.GetLineByNumber(locate.Line);
            var s = editor.TextArea.Document.GetText(line.Offset, line.Length);
            if (string.IsNullOrWhiteSpace(s)) return s;
            var separator = new char[] { ' ', '.' };
            int spacenum = (s.Substring(0, locate.Column - 1).Where(ch => separator.Contains(ch))).Count();
            var words = s.Split(separator);


            return words[spacenum];
        }
    }
}
