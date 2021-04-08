using AvalonDock;
using AvalonDock.Layout;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace RelertSharp.Wpf.LayoutManaging
{
    public static class LayoutManagerExtension
    {
        #region Api
        public static void SaveLayoutToXml(this DockingManager manager, string path)
        {
            RsLayoutData data = new RsLayoutData()
            {
                Center = DumpCenter(manager),
                Right = DumpSide(() => { return manager.Layout.RightSide; }),
                Left = DumpSide(() => { return manager.Layout.LeftSide; }),
                Bottom = DumpSide(() => { return manager.Layout.BottomSide; }),
                Top = DumpSide(() => { return manager.Layout.TopSide; })
            };

            XmlSerializer serializer = new XmlSerializer(typeof(RsLayoutData));
            TextWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, data);
            writer.Close();
        }
        public static void LoadLayoutFromXml(this DockingManager manager, string path, MainWindow dest)
        {
            TextReader reader = new StreamReader(path);
            XmlSerializer serializer = new XmlSerializer(typeof(RsLayoutData));
            RsLayoutData data = serializer.Deserialize(reader) as RsLayoutData;
        }
        #endregion

        private static object GetContentByType(GuiViewType type, MainWindow src)
        {
            FieldInfo[] fields = typeof(MainWindow).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo info in fields)
            {
                RsViewComponentAttribute attr = info.GetCustomAttribute<RsViewComponentAttribute>();
                if (attr != null && type == attr.ViewType)
                {
                    return info.GetValue(src);
                }
            }
            return null;
        }
        private static List<LayoutPaneData> DumpSide(Func<LayoutAnchorSide> sideFunc)
        {
            LayoutAnchorSide side = sideFunc.Invoke();
            List<LayoutPaneData> data = new List<LayoutPaneData>();
            foreach (LayoutAnchorGroup group in side.Children)
            {
                LayoutPaneData d = new LayoutPaneData()
                {
                    Contents = new List<GuiViewType>()
                };
                foreach (LayoutAnchorable anc in group.Children)
                {
                    if (anc.Content.GetViewType(out GuiViewType type))
                    {
                        d.Contents.Add(type);
                    }
                }
                data.Add(d);
            }
            return data;
        }
        private static List<LayoutPaneData> DumpCenter(DockingManager manager)
        {
            LayoutPanel center = manager.Layout.Children.OfType<LayoutPanel>().FirstOrDefault();
            List<LayoutPaneData> data = new List<LayoutPaneData>();
            foreach (ILayoutPanelElement elem in center.Children)
            {
                if (elem is LayoutAnchorablePane pane)
                {
                    LayoutPaneData d = new LayoutPaneData()
                    {
                        Width = pane.DockWidth.Value,
                        Height = pane.DockHeight.Value,
                        Contents = new List<GuiViewType>()
                    };
                    foreach (LayoutAnchorable anc in pane.Children)
                    {
                        if (anc.Content.GetViewType(out GuiViewType type))
                        {
                            d.Contents.Add(type);
                        }
                    }
                    data.Add(d);
                }
                else if (elem is LayoutDocumentPaneGroup docPg)
                {
                    LayoutPaneData d = new LayoutPaneData()
                    {
                        Width = docPg.DockWidth.Value,
                        Height = docPg.DockHeight.Value,
                        IsCenter = true,
                        Contents = new List<GuiViewType>()
                    };
                    foreach (LayoutDocumentPane docPane in docPg.Children)
                    {
                        foreach (LayoutContent con in docPane.Children)
                        {
                            if (con.Content.GetViewType(out GuiViewType type))
                            {
                                d.Contents.Add(type);
                            }
                        }
                    }
                    data.Add(d);
                }
            }
            return data;
        }


        private static bool GetViewType(this object src, out GuiViewType type)
        {
            type = GuiViewType.Undefined;
            if (src is IRsView view)
            {
                type = view.ViewType;
                return true;
            }
            return false;
        }
    }


    public class RsLayoutData
    {
        [XmlArrayItem("Pane")]
        public List<LayoutPaneData> Center { get; set; } = new List<LayoutPaneData>();
        [XmlArrayItem("Pane")]
        public List<LayoutPaneData> Left { get; set; } = new List<LayoutPaneData>();
        [XmlArrayItem("Pane")]
        public List<LayoutPaneData> Right { get; set; } = new List<LayoutPaneData>();
        [XmlArrayItem("Pane")]
        public List<LayoutPaneData> Top { get; set; } = new List<LayoutPaneData>();
        [XmlArrayItem("Pane")]
        public List<LayoutPaneData> Bottom { get; set; } = new List<LayoutPaneData>();
    }
    public class LayoutPaneData
    {
        [XmlArrayItem("ViewType")]
        public List<GuiViewType> Contents { get; set; }
        [XmlAttribute("width")]
        public double Width { get; set; }
        [XmlAttribute("height")]
        public double Height { get; set; }
        [XmlAttribute("center")]
        public bool IsCenter { get; set; } = false;
    }
}
