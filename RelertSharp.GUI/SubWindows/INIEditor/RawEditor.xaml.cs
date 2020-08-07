using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;

namespace RelertSharp.SubWindows.INIEditor
{
    /// <summary>
    /// RawEditor.xaml 的交互逻辑
    /// </summary>
    public partial class RawEditor : UserControl
    {
        public RawEditor()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            LoadHightLighting();
            LoadEvents();
            LoadFoldingManager();
        }

        private void LoadHightLighting()
        {
            string INIRuleXml = GUI.Properties.Resources.ResourceManager.GetString("INIRuleXml");
            MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(INIRuleXml));
            XmlTextReader reader = new XmlTextReader(ms);
            rawEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            ms.Dispose();
        }

        private void LoadEvents()
        {
            rawEditor.TextArea.TextEntered += TextArea_TextEntered;
            rawEditor.TextArea.KeyUp += (o, e) => Task.Run(UpdateEditor);
            rawEditor.TextArea.MouseMove += (o, e) => Task.Run(UpdateEditor);
        }


        private FoldingManager foldingManager;
        private FoldingStrategy foldingStrategy;
        private void LoadFoldingManager()
        {
            foldingManager = FoldingManager.Install(rawEditor.TextArea);
            foldingStrategy = new FoldingStrategy();
            UpdateEditor();
        }

        private void UpdateEditor()
        {
            rawEditor.Dispatcher.Invoke(() => foldingStrategy.UpdateFoldings(foldingManager, rawEditor.Document));
        }

        private void TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            rawEditor.ShowCompletionWindow(rawEditor.GetCursorWord());
            Task.Run(UpdateEditor);
        }
    }
}
