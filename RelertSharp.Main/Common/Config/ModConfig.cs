using System.Xml;
using System.IO;
using RelertSharp.Common.Config.Model;
using System.Xml.Serialization;

namespace RelertSharp.Common
{
    public partial class ModConfig
    {
        private string path;
        private RsModConfig data;

        public ModConfig(string path)
        {
            if (File.Exists(path))
            {
                this.path = path;
                ReadConfig(path);
            }
            else CreateDefaultConfig();

            ReadAttributeInfo();
        }

        #region Private Methods
        private void ReadConfig(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(RsModConfig));
            TextReader reader = new StreamReader(path);
            data = serializer.Deserialize(reader) as RsModConfig;
        }
        private void CreateDefaultConfig()
        {
            this.path = Constant.Config.Path;
            XmlSerializer serializer = new XmlSerializer(typeof(RsModConfig));
            TextReader reader = new StringReader(Properties.Resources._default);
            data = serializer.Deserialize(reader) as RsModConfig;
            SaveConfig();
        }
        #endregion



        #region Public Methods
        public void SaveConfig()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(RsModConfig));
            if (!File.Exists(path)) File.Create(path);
            TextWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, data);
            writer.Close();
        }
#endregion
#region Public Calls

#endregion
    }
}