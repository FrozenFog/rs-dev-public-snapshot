using RelertSharp.Common;
using System;
using System.Collections.Generic;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Objects
{
    public class AircraftLayer : ObjectBase<AircraftItem>
    {
        public AircraftLayer() { }
    }


    public class AircraftItem : ObjectItemBase, ICombatObject
    {
        public AircraftItem(string _id, string[] _args) : base(_id, _args)
        {
            try
            {
                if (_args.Length != Constant.MapStructure.ArgLenAircraft)
                {
                    throw new Exception();
                }
                Rotation = int.Parse(_args[5]);
                Status = _args[6];
                TagId = _args[7];
                VeterancyPercentage = int.Parse(_args[8]);
                Group = _args[9];
                AutoNORecruitType = IniParseBool(_args[10]);
                AutoYESRecruitType = IniParseBool(_args[11]);
                ObjectType = MapObjectType.Aircraft;
            }
            catch
            {
                GlobalVar.Log.Critical(string.Format("Aircraft item id: {0} has unreadable data, please verify in map file!", _id));
                ObjectType = MapObjectType.Aircraft;
            }
        }
        public AircraftItem(AircraftItem src) : base(src)
        {
            Rotation = src.Rotation;
            Status = src.Status;
            TagId = src.TagId;
            VeterancyPercentage = src.VeterancyPercentage;
            Group = src.Group;
            AutoNORecruitType = src.AutoNORecruitType;
            AutoYESRecruitType = src.AutoYESRecruitType;
            ObjectType = MapObjectType.Aircraft;
        }
        public AircraftItem(string regname)
        {
            RegName = regname;
            ObjectType = MapObjectType.Aircraft;
        }
        internal AircraftItem()
        {
            ObjectType = MapObjectType.Aircraft;
        }


        #region Public Methods
        public string[] ExtractParameter()
        {
            return new string[]
            {
                Id,
                OwnerHouse,
                RegName,
                HealthPoint.ToString(),
                X.ToString(),
                Y.ToString(),
                Rotation.ToString(),
                Status,
                TagId,
                VeterancyPercentage.ToString(),
                Group,
                AutoNORecruitType.ZeroOne(),
                AutoYESRecruitType.ZeroOne()
            };
        }
        public IMapObject ConstructFromParameter(string[] parameter)
        {
            ParameterReader reader = new ParameterReader(parameter);
            AircraftItem air = new AircraftItem()
            {
                Id = reader.ReadString(),
                OwnerHouse = reader.ReadString(),
                RegName = reader.ReadString(),
                HealthPoint = reader.ReadInt(256),
                X = reader.ReadInt(),
                Y = reader.ReadInt(),
                Rotation = reader.ReadInt(),
                Status = reader.ReadString(),
                TagId = reader.ReadString(),
                VeterancyPercentage = reader.ReadInt(100),
                Group = reader.ReadString(),
                AutoNORecruitType = reader.ReadBool(),
                AutoYESRecruitType = reader.ReadBool(true)
            };
            return air;
        }
        #endregion


        #region Public Calls
        public List<object> SaveData
        {
            get
            {
                return new List<object>()
                {
                    OwnerHouse, RegName, HealthPoint, X, Y, Rotation, Status, TagId, VeterancyPercentage, Group, 
                    AutoNORecruitType.ToInt(), AutoYESRecruitType.ToInt()
                };
            }
        }
        #endregion
    }
}
