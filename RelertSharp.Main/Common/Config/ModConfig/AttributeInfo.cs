using RelertSharp.Common.Config.Model;
using System;
using System.Collections.Generic;
using System.Xml;

namespace RelertSharp.Common
{
    public partial class ModConfig
    {
        private void ReadAttributeInfo()
        {
            ReadSection(data.LogicAttributes.Team, TeamClasses, TeamItems);
        }
        private void ReadSection(AttributeGroup src, Dictionary<string, AttributeClass> destClass, Dictionary<string, AttributeItem> destItems)
        {
            foreach (AttributeClass c in src.Classes)
            {
                destClass[c.Id] = c;
            }
            foreach (AttributeItem item in src.Items)
            {
                destItems[item.Name] = item;
            }
        }


        public Dictionary<string, AttributeClass> TeamClasses { get; set; } = new Dictionary<string, AttributeClass>();
        public Dictionary<string, AttributeItem> TeamItems { get; set; } = new Dictionary<string, AttributeItem>();
    }
    //public class AttributeItem
    //{
    //    public const string TYPE_BOOL = "bool";
    //    public const string TYPE_INT = "int";
    //    public const string TYPE_STRING = "string";
    //    public const string PARSE_WP = "WPIndex";
    //    public string Name { get; set; }
    //    public string ValueType { get; set; }
    //    public string ClassId { get; set; }
    //    public string ParseMethod { get; set; }
    //    public string Desc { get; set; }
    //    private string label;
    //    public string Label { get { return label.IsNullOrEmpty() ? Name : label; } set { label = value; } }
    //}
    //public class AttributeClass
    //{
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //    public string Desc { get; set; }
    //}
}
