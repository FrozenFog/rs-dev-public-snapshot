using RelertSharp.Common.Config.Model;
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

        private IEnumerable<IIndexableItem> ReadCombo(string type)
        {
            foreach (ComboGroup combo in data.ComboList)
            {
                if (combo.Type == type)
                {
                    List<IIndexableItem> items = new List<IIndexableItem>();
                    if (!combo.BasedOn.IsNullOrEmpty())
                    {
                        IEnumerable<IIndexableItem> based = GetCombo(combo.BasedOn);
                        items.AddRange(based);
                    }
                    List<IIndexableItem> front = new List<IIndexableItem>();
                    foreach (var item in combo.Items)
                    {
                        if (item.IsFront) front.Add(item.Convert());
                        else items.Add(item.Convert());
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
