using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using RelertSharp.MapStructure.Logic;

namespace RelertSharp.Common
{
    public partial class ModConfig
    {
        private XmlDocument doc = new XmlDocument();
        private string path;
        private XmlNode Root { get { return doc.SelectSingleNode("RsModConfig"); } }

        public ModConfig(string path)
        {
            if (File.Exists(path))
            {
                this.path = path;
                XmlReaderSettings setting = new XmlReaderSettings();
                setting.IgnoreComments = true;
                XmlReader r = XmlReader.Create(path, setting);
                doc.Load(r);
            }
            else CreateDefaultConfig();

            ReadAttributeInfo();
        }

        #region Private Methods
        private void CreateDefaultConfig()
        {
            this.path = Constant.Config.Path;
            doc.LoadXml(Properties.Resources._default);
            SaveConfig();
        }
        #endregion



        #region Public Methods
        public void SaveConfig()
        {
            doc.Save(this.path);
        }
        #endregion
        #region Public Calls

        #endregion
    }
}


namespace RelertSharp.Common
{
    internal static class XmlNodeExtension
    {
        public static string GetAttribute(this XmlNode src, string key)
        {
            return (src as XmlElement).GetAttribute(key);
        }
    }
}