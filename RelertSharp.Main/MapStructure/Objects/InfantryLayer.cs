using RelertSharp.Common;
using RelertSharp.IniSystem.Serialization;
using RelertSharp.IniSystem;
using System;
using System.Collections.Generic;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Objects
{
    [IniEntitySerialize(Constant.MapStructure.ENT_INF)]
    public class InfantryLayer : ObjectBase<InfantryItem>
    {
        public InfantryLayer() { }
        protected override InfantryItem InvokeCtor()
        {
            return new InfantryItem();
        }

        #region Public Methods - InfantryLayer
        #endregion
    }

    public class InfantryItem : ObjectItemBase, ICombatObject, IPosition
    {
        private int subcell = 4;


        #region Ctor - InfantryItem
        public InfantryItem(InfantryItem src) : base(src)
        {
            SubCell = src.SubCell;
            Status = src.Status;
            Rotation = src.Rotation;
            TagId = src.TagId;
            VeterancyPercentage = src.VeterancyPercentage;
            Group = src.Group;
            IsAboveGround = src.IsAboveGround;
            AutoNORecruitType = src.AutoNORecruitType;
            AutoYESRecruitType = src.AutoYESRecruitType;
            ObjectType = MapObjectType.Infantry;
        }
        public InfantryItem(string regname)
        {
            RegName = regname;
            ObjectType = MapObjectType.Infantry;
        }
        internal InfantryItem()
        {
            ObjectType = MapObjectType.Infantry;
        }

        public override void ReadFromIni(INIPair src)
        {
            ParameterReader reader = new ParameterReader(src.ParseStringList());
            base.ReadFromIni(reader, src.Name);
            SubCell = reader.ReadInt();
            Status = reader.ReadString();
            Rotation = reader.ReadInt();
            TagId = reader.ReadString();
            VeterancyPercentage = reader.ReadInt();
            Group = reader.ReadString();
            IsAboveGround = reader.ReadBool();
            AutoNORecruitType = reader.ReadBool();
            AutoYESRecruitType = reader.ReadBool();
            if (reader.ReadError) GlobalVar.Monitor.LogCritical(Id, RegName, ObjectType, this);
        }

        public override INIPair SaveAsIni()
        {
            ParameterWriter writer = new ParameterWriter();
            INIPair p = new INIPair(Id);
            base.SaveToWriter(writer);
            writer.Write(SubCell);
            writer.Write(Status);
            writer.Write(Rotation);
            writer.Write(TagId);
            writer.Write(VeterancyPercentage);
            writer.Write(Group);
            writer.Write(IsAboveGround);
            writer.Write(AutoNORecruitType);
            writer.Write(AutoYESRecruitType);
            p.Value = writer.ToString();
            return p;
        }
        #endregion


        #region Public Methods - InfantryItem
        public override void MoveTo(I3dLocateable pos, int subcell = -1)
        {
            SubCell = subcell;
            base.MoveTo(pos, subcell);
        }
        public override void ApplyConfig(IMapObjectBrushConfig config, IObjectBrushFilter filter, bool applyPosAndName = false)
        {
            base.ApplyConfig(config, filter, applyPosAndName);
            if (applyPosAndName) SubCell = config.SubCell;
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
                subcell.ToString(),
                Status,
                Rotation.ToString(),
                TagId,
                VeterancyPercentage.ToString(),
                Group,
                IsAboveGround.ZeroOne(),
                AutoNORecruitType.ZeroOne(),
                AutoYESRecruitType.ZeroOne()
            };
        }
        public int GetChecksum()
        {
            return ExtractParameter().GetHashCode();
        }
        public IMapObject ConstructFromParameter(string[] command)
        {
            ParameterReader reader = new ParameterReader(command);
            InfantryItem inf = new InfantryItem()
            {
                Id = reader.ReadString(),
                Owner = reader.ReadString(),
                RegName = reader.ReadString(),
                HealthPoint = reader.ReadInt(256),
                X = reader.ReadInt(),
                Y = reader.ReadInt(),
                subcell = reader.ReadInt(4),
                Status = reader.ReadString(),
                Rotation = reader.ReadInt(),
                TagId = reader.ReadString(),
                VeterancyPercentage = reader.ReadInt(100),
                Group = reader.ReadString(),
                IsAboveGround = reader.ReadBool(),
                AutoNORecruitType = reader.ReadBool(),
                AutoYESRecruitType = reader.ReadBool(true)
            };
            return inf;
        }
        #endregion


        #region Public Calls - InfantryItem
        public List<object> SaveData
        {
            get
            {
                return new List<object>()
                {
                    Owner, RegName, HealthPoint, X,Y,subcell,Status, Rotation,TagId,VeterancyPercentage,Group,
                    IsAboveGround.ToInt(), AutoNORecruitType.ToInt(), AutoYESRecruitType.ToInt()
                };
            }
        }
        [IniPairItem(5)]
        public int SubCell
        {
            get { return subcell; }
            set
            {
                if (value >= 2 && value <= 4)
                {
                    subcell = value;
                }
                else
                {
                    subcell = 4;
                }
            }
        }
        [IniPairItem(7)]
        public override int Rotation { get => base.Rotation; set => base.Rotation = value; }
        [IniPairItem(8)]
        public override string TagId { get => base.TagId; set => base.TagId = value; }
        [IniPairItem(9)]
        public override int VeterancyPercentage { get => base.VeterancyPercentage; set => base.VeterancyPercentage = value; }
        [IniPairItem(10)]
        public override string Group { get => base.Group; set => base.Group = value; }
        [IniPairItem(11, IniBoolCastType.ZeroOne)]
        public override bool IsAboveGround { get => base.IsAboveGround; set => base.IsAboveGround = value; }
        [IniPairItem(12, IniBoolCastType.ZeroOne)]
        public override bool AutoNORecruitType { get => base.AutoNORecruitType; set => base.AutoNORecruitType = value; }
        [IniPairItem(13, IniBoolCastType.ZeroOne)]
        public override bool AutoYESRecruitType { get => base.AutoYESRecruitType; set => base.AutoYESRecruitType = value; }
        #endregion
    }
}
