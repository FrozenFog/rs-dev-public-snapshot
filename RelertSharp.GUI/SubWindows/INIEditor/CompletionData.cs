using System;
using System.Windows.Controls;

using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace RelertSharp.SubWindows.INIEditor
{
    // Inplement AvalonEdit.CodeCompletion.ICompletionData interface
    public class CompletionData : ICompletionData
    {
        public CompletionData(string text, int inputnum, string description)
        {
            Text = text;
            InputNum = inputnum;
            Description = description;
        }
        protected int InputNum;
        
        // Image
        public System.Windows.Media.ImageSource Image => null;

        // Text
        public string Text { get; private set; }

        // Control
        public object Content => Text;
        
        // Desc
        public object Description { get; private set; }

        // Priority
        public double Priority => 1;

        public void Complete(
            TextArea textArea,
            ISegment completionSegment,
            EventArgs insertionRequestEventArgs
            )
            => textArea.Document.Replace(completionSegment, Text.Substring(InputNum));
    }
}
