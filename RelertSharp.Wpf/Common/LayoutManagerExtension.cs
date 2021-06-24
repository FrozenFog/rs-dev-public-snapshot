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
using AvalonDock.Layout.Serialization;

namespace RelertSharp.Wpf
{
    public static class LayoutManagerExtension
    {
        #region Api
        public static void SaveLayoutToXml(this DockingManager manager, string filename)
        {
            string path = Path.Combine(filename);
            XmlLayoutSerializer serializer = new XmlLayoutSerializer(manager);
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            XmlWriter xml = XmlWriter.Create(fs);
            serializer.Serialize(xml);
            xml.Flush();
            fs.Close();
        }
        public static void LoadLayoutFromXml(this DockingManager manager, string filename, MainWindow dest)
        {
            string path = Path.Combine(filename);
            XmlLayoutSerializer serializer = new XmlLayoutSerializer(manager);
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            serializer.Deserialize(fs);
            fs.Close();
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
}
