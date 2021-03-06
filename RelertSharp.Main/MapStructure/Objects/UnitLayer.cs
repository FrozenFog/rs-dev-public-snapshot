using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.IniSystem.Serialization;
using System;
using System.Collections.Generic;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Objects
{
    [IniEntitySerialize(Constant.MapStructure.ENT_UNIT)]
    public class UnitLayer : ObjectBase<UnitItem>
    {
        public UnitLayer() { }
        protected override UnitItem InvokeCtor()
        {
            return new UnitItem();
        }

        #region Public Calls - UnitLayer

        #endregion
    }

    public class UnitItem : ObjectItemBase, ICombatObject
    {
        public override void ReadFromIni(INIPair src)
        {
            ParameterReader r = new ParameterReader(src.ParseStringList());
            base.ReadFromIni(r, src.Name);
            Rotation = r.ReadInt();
            Status = r.ReadString();
            TagId = r.ReadString();
            VeterancyPercentage = r.ReadInt();
            Group = r.ReadString();
            IsAboveGround = r.ReadBool();
            FollowsIndex = r.ReadString();
            AutoNORecruitType = r.ReadBool();
            AutoYESRecruitType = r.ReadBool();
            if (r.ReadError) GlobalVar.Monitor.LogCritical(Id, RegName, ObjectType, this);
        }
        public override INIPair SaveAsIni()
        {
            ParameterWriter w = new ParameterWriter();
            INIPair p = new INIPair(Id);
            base.SaveToWriter(w);
            w.Write(Rotation);
            w.Write(Status);
            w.Write(TagId);
            w.Write(VeterancyPercentage);
            w.Write(Group);
            w.Write(IsAboveGround);
            w.Write(FollowsIndex);
            w.Write(AutoNORecruitType);
            w.Write(AutoYESRecruitType);
            p.Value = w.ToString();
            return p;
        }
        public UnitItem(UnitItem src) : base(src)
        {
            FollowsIndex = src.FollowsIndex;
            Rotation = src.Rotation;
            Status = src.Status;
            TagId = src.TagId;
            VeterancyPercentage = src.VeterancyPercentage;
            Group = src.Group;
            IsAboveGround = src.IsAboveGround;
            AutoNORecruitType = src.AutoNORecruitType;
            AutoYESRecruitType = src.AutoYESRecruitType;
            ObjectType = MapObjectType.Vehicle;
        }
        public UnitItem(string regname)
        {
            RegName = regname;
            ObjectType = MapObjectType.Vehicle;
        }
        internal UnitItem()
        {
            ObjectType = MapObjectType.Vehicle;
        }


        #region Public Methods
        public override void ApplyConfig(IMapObjectBrushConfig config, IObjectBrushFilter filter, bool applyPosAndName = false)
        {
            base.ApplyConfig(config, filter, applyPosAndName);
            if (filter.Follows) FollowsIndex = config.FollowsIndex;
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
                Status,
                TagId,
                VeterancyPercentage.ToString(),
                Group,
                IsAboveGround.ZeroOne(),
                FollowsIndex,
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
            UnitItem u = new UnitItem()
            {
                Id = reader.ReadString(),
                Owner = reader.ReadString(),
                RegName = reader.ReadString(),
                HealthPoint = reader.ReadInt(256),
                X = reader.ReadInt(),
                Y = reader.ReadInt(),
                Rotation = reader.ReadInt(),
                Status = reader.ReadString(),
                TagId = reader.ReadString(),
                VeterancyPercentage = reader.ReadInt(100),
                Group = reader.ReadString(),
                IsAboveGround = reader.ReadBool(),
                FollowsIndex = reader.ReadString(),
                AutoNORecruitType = reader.ReadBool(),
                AutoYESRecruitType = reader.ReadBool(true)
            };
            return u;
        }
        #endregion


        #region Public Calls
        public List<object> SaveData
        {
            get
            {
                return new List<object>()
                {
                    Owner, RegName, HealthPoint, X, Y, Rotation, Status, TagId, VeterancyPercentage, Group, 
                    IsAboveGround.ToInt(), FollowsIndex, AutoNORecruitType.ToInt(),AutoYESRecruitType.ToInt()
                };
            }
        }
        [IniPairItem(10, IniBoolCastType.ZeroOne)]
        public override bool IsAboveGround { get => base.IsAboveGround; set => base.IsAboveGround = value; }
        [IniPairItem(11)]
        public string FollowsIndex { get; set; } = Constant.ID_INVALID;
        [IniPairItem(12, IniBoolCastType.ZeroOne)]
        public override bool AutoNORecruitType { get => base.AutoNORecruitType; set => base.AutoNORecruitType = value; }
        [IniPairItem(13, IniBoolCastType.ZeroOne)]
        public override bool AutoYESRecruitType { get => base.AutoYESRecruitType; set => base.AutoYESRecruitType = value; }
        #endregion
    }
}
