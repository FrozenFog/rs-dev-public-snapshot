using RelertSharp.Common;
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
        private StructureItem()
        {
            ObjectType = MapObjectType.Building;
        }
        public StructureItem(string _id, string[] _args) : base(_id, _args)
        {
            try
            {
                if (_args.Length != Constant.MapStructure.ArgLenStructure)
                {
                    throw new Exception();
                }
                Rotation = int.Parse(_args[5]);
                TagId = _args[6];
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
            TagId = src.TagId;
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
        public string[] ExtractParameter()
        {
            return new string[]
            {
                Id,
                Owner,
                RegName,
                HealthPoint.ToString(),
                X.ToString(),
                Y.ToString(),
                Rotation.ToString(),
                TagId,
                AISellable.ZeroOne(),
                AIRebuildable.ZeroOne(),
                IsPowered.ZeroOne(),
                UpgradeNum.ToString(),
                ((int)SpotlightType).ToString(),
                Upgrade1,
                Upgrade2,
                Upgrade3,
                AIRepairable.ZeroOne(),
            };
        }
        public IMapObject ConstructFromParameter(string[] command)
        {
            ParameterReader reader = new ParameterReader(command);
            StructureItem bud = new StructureItem()
            {
                Id = reader.ReadString(),
                Owner = reader.ReadString(),
                RegName = reader.ReadString(),
                HealthPoint = reader.ReadInt(256),
                X = reader.ReadInt(),
                Y = reader.ReadInt(),
                Rotation = reader.ReadInt(),
                TagId = reader.ReadString(),
                AISellable = reader.ReadBool(true),
                AIRebuildable = reader.ReadBool(true),
                IsPowered = reader.ReadBool(true),
                UpgradeNum = reader.ReadInt(),
                SpotlightType = (BuildingSpotlightType)reader.ReadInt(),
                Upgrade1 = reader.ReadString(),
                Upgrade2 = reader.ReadString(),
                Upgrade3 = reader.ReadString(),
                AIRepairable = reader.ReadBool(true),
            };
            return bud;
        }
        #endregion


        #region Public Calls - StructureItem
        public List<object> SaveData
        {
            get
            {
                return new List<object>()
                {
                    Owner, RegName, HealthPoint, X, Y, Rotation, TagId, AISellable.ToInt(), AIRebuildable.ToInt(), IsPowered.ToInt(), 
                    UpgradeNum, (int)SpotlightType, Upgrade1,Upgrade2,Upgrade3,AIRepairable.ToInt(), Nominal.ToInt() 
                };
            }
        }
        /// <summary>
        /// Default: true
        /// </summary>
        public bool AISellable { get; set; } = true;
        /// <summary>
        /// Default: true
        /// </summary>
        public bool AIRebuildable { get; set; } = false;
        /// <summary>
        /// Default: true
        /// </summary>
        public bool IsPowered { get; set; } = true;
        public int UpgradeNum { get; set; }
        public BuildingSpotlightType SpotlightType { get; set; }
        public string Upgrade1 { get; set; } = "None";
        public string Upgrade2 { get; set; } = "None";
        public string Upgrade3 { get; set; } = "None";
        /// <summary>
        /// Default: true
        /// </summary>
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
