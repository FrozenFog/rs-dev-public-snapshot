using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.CodeCompletion;

namespace RelertSharp.SubWindows.INIEditor
{
    public static class IntelligenceManager
    {
        private static Dictionary<string, string> completionData = new Dictionary<string, string>();

        static IntelligenceManager()
        {
            // TODO 
            // Initialize Intelligense Dictionary
            // SAMPLE
            // completionData.Add("Completion content","Description");
            completionData.Add("Key1", "Desc1");
            completionData.Add("Key2", "Desc2");
            completionData.Add("Key3", "Desc3");
        }

        public static void ShowCompletionWindow(this ICSharpCode.AvalonEdit.TextEditor editor, string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return;

            var completionWindow = editor.GetCompletionWindow();

            IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
            data.Clear();
            foreach (var word in completionData.Where(word => word.Key.StartsWith(s)))
                data.Add(new CompletionData(word.Key, s.Length, word.Value));

            if (data.Count == 0) return;
            completionWindow.CompletionList.ListBox.SelectedIndex = 0;
            completionWindow.Show();
            completionWindow.Closed += delegate
            {
                completionWindow = null;
            };
        }
        private static CompletionWindow GetCompletionWindow(this ICSharpCode.AvalonEdit.TextEditor editor)
        {
            var completionWindow = new CompletionWindow(editor.TextArea);

            completionWindow.Foreground = new SolidColorBrush(Colors.White);

            (completionWindow.Content as Control).Background = new SolidColorBrush(Colors.Transparent);

            completionWindow.CompletionList.Background = new SolidColorBrush(Colors.Transparent);
            completionWindow.CompletionList.Foreground = new SolidColorBrush(Colors.White);
            return completionWindow;
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
