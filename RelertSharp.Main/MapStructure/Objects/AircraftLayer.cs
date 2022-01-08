using RelertSharp.Common;
using RelertSharp.IniSystem.Serialization;
using RelertSharp.IniSystem;
using System;
using System.Collections.Generic;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Objects
{
    [IniEntitySerialize(Constant.MapStructure.ENT_AIR)]
    public class AircraftLayer : ObjectBase<AircraftItem>
    {
        public AircraftLayer() { }

        protected override AircraftItem InvokeCtor()
        {
            return new AircraftItem();
        }
        public override void ReadFromIni(INIEntity src)
        {
            foreach (var p in src)
            {
                var item = new AircraftItem();
                item.ReadFromIni(p);
                this[p.Name] = item;
            }
        }
    }


    public class AircraftItem : ObjectItemBase, ICombatObject
    {
        public override void ReadFromIni(INIPair src)
        {
            ParameterReader reader = new ParameterReader(src.ParseStringList());
            base.ReadFromIni(reader, src.Name);
            Rotation = reader.ReadInt();
            Status = reader.ReadString();
            TagId = reader.ReadString();
            VeterancyPercentage = reader.ReadInt();
            Group = reader.ReadString();
            AutoNORecruitType = reader.ReadBool();
            AutoYESRecruitType = reader.ReadBool();
            if (reader.ReadError) GlobalVar.Monitor.LogCritical(Id, RegName, ObjectType, this);
        }

        public override INIPair SaveAsIni()
        {
            INIPair p = new INIPair(Id);
            ParameterWriter writer = new ParameterWriter();
            base.SaveToWriter(writer);
            writer.Write(Rotation);
            writer.Write(Status);
            writer.Write(TagId);
            writer.Write(VeterancyPercentage);
            writer.Write(Group);
            writer.Write(AutoNORecruitType);
            writer.Write(AutoYESRecruitType);
            p.Value = writer.ToString();
            return p;
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
                AutoNORecruitType.ZeroOne(),
                AutoYESRecruitType.ZeroOne()
            };
        }
        public int GetChecksum()
        {
            return ExtractParameter().GetHashCode();
        }
        public IMapObject ConstructFromParameter(string[] parameter)
        {
            ParameterReader reader = new ParameterReader(parameter);
            AircraftItem air = new AircraftItem()
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
                    Owner, RegName, HealthPoint, X, Y, Rotation, Status, TagId, VeterancyPercentage, Group, 
                    AutoNORecruitType.ToInt(), AutoYESRecruitType.ToInt()
                };
            }
        }
        [IniPairItem(10, IniBoolCastType.ZeroOne)]
        public override bool AutoNORecruitType { get => base.AutoNORecruitType; set => base.AutoNORecruitType = value; }
        [IniPairItem(11, IniBoolCastType.ZeroOne)]
        public override bool AutoYESRecruitType { get => base.AutoYESRecruitType; set => base.AutoYESRecruitType = value; }
        #endregion
    }
}
