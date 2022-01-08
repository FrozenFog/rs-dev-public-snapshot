using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.IniSystem.Serialization;
using System;
using System.Collections.Generic;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Objects
{
    [IniEntitySerialize(Constant.MapStructure.ENT_STR)]
    public class StructureLayer : ObjectBase<StructureItem>
    {
        public StructureLayer() { }
        protected override StructureItem InvokeCtor()
        {
            return new StructureItem();
        }

        #region Public Methods - StructureLayer
        #endregion
    }


    public class StructureItem : ObjectItemBase, ICombatObject
    {
        private int sizeX = 0, sizeY = 0;


        #region Ctor
        internal StructureItem()
        {
            ObjectType = MapObjectType.Building;
        }
        public override void ReadFromIni(INIPair src)
        {
            ParameterReader reader = new ParameterReader(src.ParseStringList());
            base.ReadFromIni(reader, src.Name);
            Rotation = reader.ReadInt();
            TagId = reader.ReadString();
            AISellable = reader.ReadBool();
            reader.Skip();
            IsPowered = reader.ReadBool();
            UpgradeNum = reader.ReadInt();
            SpotlightType = (BuildingSpotlightType)reader.ReadInt();
            Upgrade1 = reader.ReadString();
            Upgrade2 = reader.ReadString();
            Upgrade3 = reader.ReadString();
            AIRepairable = reader.ReadBool();
            if (reader.ReadError) GlobalVar.Monitor.LogCritical(Id, RegName, ObjectType, this);
        }

        public override INIPair SaveAsIni()
        {
            ParameterWriter w = new ParameterWriter();
            INIPair p = new INIPair(Id);
            base.SaveToWriter(w);
            w.Write(Rotation);
            w.Write(TagId);
            w.Write(AISellable);
            w.Write(AIRebuildable);
            w.Write(IsPowered);
            w.Write(UpgradeNum);
            w.Write((int)SpotlightType);
            w.Write(Upgrade1);
            w.Write(Upgrade2);
            w.Write(Upgrade3);
            w.Write(AIRepairable);
            w.Write(Nominal);
            p.Value = w.ToString();
            return p;
        }
        internal StructureItem(StructureItem src) : base(src)
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
        public int GetChecksum()
        {
            return ExtractParameter().GetHashCode();
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
        [IniPairItem(7)]
        public bool AISellable { get; set; } = true;
        /// <summary>
        /// Default: true
        /// </summary>
        public bool AIRebuildable { get; set; } = false;
        /// <summary>
        /// Default: true
        /// </summary>
        [IniPairItem(9)]
        public bool IsPowered { get; set; } = true;
        [IniPairItem(10)]
        public int UpgradeNum { get; set; }
        [IniPairItem(11)]
        public BuildingSpotlightType SpotlightType { get; set; }
        [IniPairItem(12)]
        public string Upgrade1 { get; set; } = Constant.VALUE_NONE;
        [IniPairItem(13)]
        public string Upgrade2 { get; set; } = Constant.VALUE_NONE;
        [IniPairItem(14)]
        public string Upgrade3 { get; set; } = Constant.VALUE_NONE;
        /// <summary>
        /// Default: true
        /// </summary>
        [IniPairItem(15)]
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
                return GlobalVar.GlobalRules[RegName].ParseBool("WaterBound");
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
        [IniPairItem(6)]
        public override string TagId { get => base.TagId; set => base.TagId = value; }
        public override int VeterancyPercentage { get => 0; set { } }
        public override string Group { get => Constant.ID_INVALID; set { } }
        #endregion


        #region Drawing

        #endregion
    }
}
