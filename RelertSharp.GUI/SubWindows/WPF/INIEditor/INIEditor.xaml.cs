using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

using RelertSharp.GUI.SubWindows.WPF.INIEditor.Data;

namespace RelertSharp.GUI.SubWindows.WPF.INIEditor
{
    /// <summary>
    /// INIEditor.xaml 的交互逻辑
    /// </summary>
    public partial class INIEditor : Window
    {

        public INIEditor()
        {
            InitializeComponent();
            InitializeMessageMap();
            using (var reader = new System.Xml.XmlTextReader("IniRule.xml"))
            {
                MainEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            }
            MainEditor.Text = "[Section]\nName = 114514 ; This is the comment";
        }

        private void InitializeMessageMap()
        {
            MainEditor.DocumentChanged += MainEditor_DocumentChanged;
            MainEditor.TextArea.TextEntering += MainEditor_TextArea_TextEntering;
            MainEditor.TextArea.TextEntered += MainEditor_TextArea_TextEntered;
            MainEditor.MouseMove += (o, e) => MainEditor_SelectionUpdate();
            MainEditor.KeyUp += (o, e) => MainEditor_SelectionUpdate();
        }

        private void MainEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void MainEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            MainEditor.ShowCompletionWindow(MainEditor.GetCursorWord());
        }

        private void MainEditor_DocumentChanged(object sender, EventArgs e)
        {
            MainEditor_SelectionUpdate();
        }

        private void MainEditor_SelectionUpdate()
        {
            var line = MainEditor.TextArea.Document.GetLineByOffset(MainEditor.CaretOffset);
            tbCurrentLine.Text = MainEditor.TextArea.Document.GetText(line.Offset, line.Length);
        }
    }
}
