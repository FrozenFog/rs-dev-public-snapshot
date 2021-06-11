﻿using RelertSharp.Common;
using RelertSharp.IniSystem;
using System;
using System.Collections.Generic;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Objects
{
    public class StructureLayer : ObjectBase<StructureItem>
    {
        public StructureLayer() { }


        #region Public Methods - StructureLayer
        #endregion
    }


    public class StructureItem : ObjectItemBase, ICombatObject
    {
        private int sizeX = 0, sizeY = 0;


        #region Ctor
        public StructureItem(string _id, string[] _args) : base(_id, _args)
        {
            try
            {
                if (_args.Length != Constant.MapStructure.ArgLenStructure)
                {
                    throw new Exception();
                }
                Rotation = int.Parse(_args[5]);
                TaggedTrigger = _args[6];
                AISellable = IniParseBool(_args[7]);
                IsPowered = IniParseBool(_args[9]);
                UpgradeNum = int.Parse(_args[10]);
                SpotlightType = (BuildingSpotlightType)(int.Parse(_args[11]));
                Upgrade1 = _args[12];
                Upgrade2 = _args[13];
                Upgrade3 = _args[14];
                AIRepairable = IniParseBool(_args[15]);
                ObjectType = MapObjectType.Building;
            }
            catch
            {
                GlobalVar.Log.Critical(string.Format("Building item id: {0} has unreadable data, please verify in map file!", _id));
                ObjectType = MapObjectType.Building;
            }
        }
        public StructureItem(StructureItem src) : base(src)
        {
            Rotation = src.Rotation;
            TaggedTrigger = src.TaggedTrigger;
            AISellable = src.AISellable;
            IsPowered = src.IsPowered;
            UpgradeNum = src.UpgradeNum;
            SpotlightType = src.SpotlightType;
            Upgrade1 = src.Upgrade1;
            Upgrade2 = src.Upgrade2;
            Upgrade3 = src.Upgrade3;
            AIRepairable = src.AIRepairable;
            ObjectType = MapObjectType.Building;
        }
        public StructureItem(string regname)
        {
            RegName = regname;
            ObjectType = MapObjectType.Building;
        }
        #endregion


        #region Public Methods - StructureItem
        public override void ApplyConfig(IMapObjectBrushConfig config, IObjectBrushFilter filter, bool applyPosAndName = false)
        {
            base.ApplyConfig(config, filter, applyPosAndName);
            if (filter.Sellable) AISellable = config.IsSellable;
            if (filter.Rebuild) AIRebuildable = config.AiRebuildable;
            if (filter.Powered) IsPowered = config.Powered;
            if (filter.UpgradeNum) UpgradeNum = config.UpgradeNum;
            if (filter.Upg1) Upgrade1 = config.Upg1;
            if (filter.Upg2) Upgrade2 = config.Upg2;
            if (filter.Upg3) Upgrade3 = config.Upg3;
            if (filter.AiRepairable) AIRepairable = config.AiRepairable;
            if (filter.Spotlight) SpotlightType = config.SpotlightType;
        }
        #endregion


        #region Public Calls - StructureItem
        public List<object> SaveData
        {
            get
            {
                return new List<object>()
                {
                    OwnerHouse, RegName, HealthPoint, X, Y, Rotation, TaggedTrigger, AISellable.ToInt(), AIRebuildable.ToInt(), IsPowered.ToInt(), 
                    UpgradeNum, (int)SpotlightType, Upgrade1,Upgrade2,Upgrade3,AIRepairable.ToInt(), Nominal.ToInt() 
                };
            }
        }
        public bool AISellable { get; set; } = true;
        public bool AIRebuildable { get; private set; } = false;
        public bool IsPowered { get; set; } = true;
        public int UpgradeNum { get; set; }
        public BuildingSpotlightType SpotlightType { get; set; }
        public string Upgrade1 { get; set; } = "None";
        public string Upgrade2 { get; set; } = "None";
        public string Upgrade3 { get; set; } = "None";
        public bool AIRepairable { get; set; } = true;
        public bool Nominal { get; private set; } = false;
        public int SizeX
        {
            get
            {
                if (sizeX == 0)
                {
                    GlobalVar.GlobalRules.GetBuildingShapeData(RegName, out int height, out int sizeX, out int sizeY);
                    this.sizeX = sizeX;
                    this.sizeY = sizeY;
                }
                return sizeX;
            }
        }
        public int SizeY
        {
            get
            {
                if (sizeY == 0)
                {
                    GlobalVar.GlobalRules.GetBuildingShapeData(RegName, out int height, out int sizeX, out int sizeY);
                }
                return sizeY;
            }
        }
        public bool IsNaval
        {
            get
            {
                return GlobalVar.GlobalRules.GetEnt(RegName).ParseBool("WaterBound");
            }
        }
        private List<bool> shape;
        public List<bool> BuildingShape
        {
            get
            {
                if (shape == null)
                {
                    shape = GlobalVar.GlobalRules.GetBuildingCustomShape(RegName, SizeX, SizeY);
                }
                return shape;
            }
        }
        #endregion


        #region Drawing

        #endregion
    }
}
