using RelertSharp.IniSystem;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Xml;
using static RelertSharp.Common.Constant.Config.DefaultComboType;

namespace RelertSharp.Common
{
    public partial class ModConfig
    {
        private Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }
        private Rules Rules { get { return GlobalVar.GlobalRules; } }

        private bool IsDefaultCombo(XmlNode n)
        {
            return (n as XmlElement).GetAttribute("default").ParseBool();
        }
        private IEnumerable<IIndexableItem> ReadCombo(string type)
        {
            XmlNode comboList = Root.SelectSingleNode("ComboList");
            foreach (XmlNode n in comboList.ChildNodes)
            {
                string name = n.GetAttribute("type");
                if (name == type && !IsDefaultCombo(n))
                {
                    string baseOn = n.GetAttribute("base");
                    List<IIndexableItem> items = new List<IIndexableItem>();
                    if (!baseOn.IsNullOrEmpty())
                    {
                        IEnumerable<IIndexableItem> based = GetCombo(baseOn);
                        items.AddRange(based);
                    }
                    List<IIndexableItem> front = new List<IIndexableItem>();
                    foreach (XmlNode nItem in n.ChildNodes)
                    {
                        string key = nItem.GetAttribute("key");
                        string desc = nItem.GetAttribute("desc");
                        string value = nItem.GetAttribute("value");
                        bool isFront = nItem.GetAttribute("front").ParseBool();
                        ComboItem cb = new ComboItem(key, desc, value);
                        if (isFront) front.Add(cb);
                        else items.Add(cb);
                    }
                    if (front.Count > 0)
                    {
                        front.AddRange(items);
                        return front;
                    }
                    else return items;
                }
            }
            return new List<IIndexableItem>();
        }
        public IEnumerable<IIndexableItem> GetCombo(string type)
        {
            switch (type)
            {
                case TYPE_WAYPOINT:
                    return Map.Waypoints;
                case TYPE_SCRIPTS:
                    return Map.Scripts;
                case TYPE_TEAMS:
                    return Map.Teams;
                case TYPE_TASKFORCES:
                    return Map.Taskforces;
                case TYPE_TRIGGERS:
                    return Map.Triggers;
                case TYPE_TECHTYPE:
                    return Rules.TechnoList;
                case TYPE_TAGS:
                    return Map.Tags;
                case TYPE_HOUSES:
                    return Map.Houses;
                case TYPE_OWNERCOUNTRY:
                    return Map.Countries;
                default:
                    return ReadCombo(type);
            }
        }
    }
}
