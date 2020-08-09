using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static RelertSharp.Language;

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
            LoadSearchBox();
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
            rawEditor.TextArea.PreviewKeyDown += (o, e) =>
              {
                  if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.F)
                      searchBox.txbContent.Focus();
              };
        }


        private FoldingManager foldingManager;
        private FoldingStrategy foldingStrategy;
        private void LoadFoldingManager()
        {
            foldingManager = FoldingManager.Install(rawEditor.TextArea);
            foldingStrategy = new FoldingStrategy();
            UpdateEditor();
        }

        private void LoadSearchBox()
        {
            searchBox.btnSearch.Click += SearchBox_BtnSearch_Click;
            searchBox.btnSearch.ToolTip = DICT["INIbtnSearch"];
            searchBox.txbContent.KeyDown += (o,e) => 
            {
                if (e.Key == Key.Enter)
                    SearchBox_BtnSearch_Click(o, e);
            };

            // Disable regex control characters
            searchRegHelper.Add('^');
            searchRegHelper.Add('[');
            searchRegHelper.Add(']');
            searchRegHelper.Add('$');
            searchRegHelper.Add('.');
            searchRegHelper.Add('*');
            searchRegHelper.Add('\\');
            searchRegHelper.Add('?');
            searchRegHelper.Add('+');
            searchRegHelper.Add('{');
            searchRegHelper.Add('}');
            searchRegHelper.Add('|');
            searchRegHelper.Add('(');
            searchRegHelper.Add(')');
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

        HashSet<char> searchRegHelper = new HashSet<char>();
        private void SearchBox_BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string input = searchBox.txbContent.Text;
            StringBuilder sb = new StringBuilder();
            foreach(char c in input)
            {
                if (searchRegHelper.Contains(c))
                    sb.Append('\\');
                sb.Append(c);
            }

            Regex regex = new Regex(sb.ToString(), RegexOptions.IgnoreCase);
            Match match = regex.Match(rawEditor.Text, rawEditor.CaretOffset);

            Action foundAction = delegate
            {
                rawEditor.CaretOffset = match.Index;
                var lineNum = rawEditor.Document.GetLineByOffset(match.Index);
                rawEditor.ScrollTo(lineNum.LineNumber, 1);
                rawEditor.Select(match.Index, match.Length);
                searchBox.txbContent.Focus();
            };

            if (match.Success)
                foundAction();
            else
            {
                match = regex.Match(rawEditor.Text);
                if (match.Success)
                    foundAction();
                else
                    MessageBox.Show("NOT FOUND");
            }
        }
    }
}