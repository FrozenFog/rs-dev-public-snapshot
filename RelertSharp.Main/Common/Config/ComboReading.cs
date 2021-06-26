using RelertSharp.Common.Config.Model;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Xml;
using static RelertSharp.Common.Constant.Config.DefaultComboType;
using static RelertSharp.IniSystem.RulesComboExtension;

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
                        if (combo.IsZeroIndex)
                        {
                            int i = 0;
                            foreach (IIndexableItem item in front)
                            {
                                item.Id = i++.ToString();
                            }
                        }
                        return front;
                    }
                    else
                    {
                        if (combo.IsZeroIndex)
                        {
                            int i = 0;
                            IEnumerable<IIndexableItem> tmp = new List<IIndexableItem>(items);
                            items.Clear();
                            foreach (IIndexableItem item in tmp)
                            {
                                ComboItem cb = new ComboItem(item.Id, item.Name);
                                if (cb.Name.IsNullOrEmpty()) cb.Name = cb.Id;
                                cb.Id = i++.ToString();
                                items.Add(cb);
                            }
                        }
                        return items;
                    }
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
                case TYPE_IDX_HOUSE:
                    return Map.Houses.ToZeroIdx();
                case TYPE_OWNERCOUNTRY:
                    return Map.Countries;
                case TYPE_GLOBAL:
                    return Rules.GlobalVar;
                case TYPE_LOCAL:
                    return Map.LocalVariables;
                case TYPE_BUDREG:
                    return Rules.BuildingList(false, true);
                case TYPE_IDX_UNT:
                    return Rules.VehicleList(true);
                case TYPE_IDX_INF:
                    return Rules.InfantryList(true);
                case TYPE_IDX_AIR:
                    return Rules.AircraftList(true);
                case TYPE_IDX_ANIM:
                    return Rules.AnimationList(true);
                case TYPE_IDX_PRTC:
                    return Rules.ParticalList(true);
                case TYPE_IDX_WEP:
                    return Rules.WeaponList(true);
                case TYPE_IDX_VOX:
                    return Rules.VoxAnimList(true);
                case TYPE_IDX_SUP:
                    return Rules.SuperWeaponList(true);
                case TYPE_REG_SUP:
                    return Rules.SuperWeaponList(false, true);
                case TYPE_MOVIE:
                    return Rules.MovieList();
                case TYPE_REG_SND:
                    return GlobalVar.GlobalSound.SoundList;
                case TYPE_REG_THM:
                    return GlobalVar.GlobalSound.ThemeList;
                case TYPE_REG_EVA:
                    return GlobalVar.GlobalSound.EvaList;
                case TYPE_CSF:
                    return GlobalVar.GlobalCsf;
                default:
                    return ReadCombo(type);
            }
        }
    }
}
