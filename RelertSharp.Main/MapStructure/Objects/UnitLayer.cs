using RelertSharp.Common;
using RelertSharp.IniSystem.Serialization;
using System;
using System.Collections.Generic;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Objects
{
    public class UnitLayer : ObjectBase<UnitItem>
    {
        public UnitLayer() { }


        #region Public Calls - UnitLayer

        #endregion
    }

    public class UnitItem : ObjectItemBase, ICombatObject
    {
        public UnitItem(string _id, string[] _args) : base(_id, _args)
        {
            try
            {
                if (_args.Length != Constant.MapStructure.ArgLenUnit)
                {
                    throw new Exception();
                }
                Rotation = int.Parse(_args[5]);
                Status = _args[6];
                TagId = _args[7];
                VeterancyPercentage = int.Parse(_args[8]);
                Group = _args[9];
                IsAboveGround = IniParseBool(_args[10]);
                FollowsIndex = _args[11];
                AutoNORecruitType = IniParseBool(_args[12]);
                AutoYESRecruitType = IniParseBool(_args[13]);
                ObjectType = MapObjectType.Vehicle;
            }
            catch
            {
                GlobalVar.Log.Critical(string.Format("Unit item id: {0} has unreadable data, please verify in map file!", _id));
                ObjectType = MapObjectType.Vehicle;
            }
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
