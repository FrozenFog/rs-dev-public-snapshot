using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RelertSharp.Common
{
    public partial class ModConfig
    {
        private void ReadAttributeInfo()
        {
            XmlNode attributes = Root.SelectSingleNode("AttributeInfo");
            ReadSection(attributes.SelectSingleNode("Team"), TeamClasses, TeamItems);
        }
        private void ReadSection(XmlNode root, Dictionary<string, AttributeClass> destClass, Dictionary<string, AttributeItem> destItems)
        {
            foreach (XmlNode nClass in root.SelectNodes("class"))
            {
                string id = nClass.GetAttribute("id");
                string name = nClass.GetAttribute("name");
                string desc = nClass.GetAttribute("desc");
                destClass[id] = new AttributeClass()
                {
                    Id = id,
                    Desc = desc,
                    Name = name
                };
            }
            foreach (XmlNode nItem in root.SelectNodes("attr"))
            {
                string name = nItem.GetAttribute("name");
                string desc = nItem.GetAttribute("desc");
                string valueType = nItem.GetAttribute("type");
                string classId = nItem.GetAttribute("class");
                string label = nItem.GetAttribute("label");
                string parse = nItem.GetAttribute("parsing");
                destItems[name] = new AttributeItem()
                {
                    ClassId = classId,
                    Desc = desc,
                    Name = name,
                    ValueType = valueType,
                    Label = label,
                    ParseMethod = parse
                };
            }
        }


        public Dictionary<string, AttributeClass> TeamClasses { get; set; } = new Dictionary<string, AttributeClass>();
        public Dictionary<string, AttributeItem> TeamItems { get; set; } = new Dictionary<string, AttributeItem>();
    }
    public class AttributeItem
    {
        public const string TYPE_BOOL = "bool";
        public const string TYPE_INT = "int";
        public const string TYPE_STRING = "string";
        public const string PARSE_WP = "WPIndex";
        public string Name { get; set; }
        public string ValueType { get; set; }
        public string ClassId { get; set; }
        public string ParseMethod { get; set; }
        public string Desc { get; set; }
        private string label;
        public string Label { get { return label.IsNullOrEmpty() ? Name : label; } set { label = value; } }
    }
    public class AttributeClass
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }
}
