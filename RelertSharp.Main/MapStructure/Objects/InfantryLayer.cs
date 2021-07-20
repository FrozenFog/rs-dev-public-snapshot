﻿using RelertSharp.Common;
using System;
using System.Collections.Generic;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Objects
{
    public class InfantryLayer : ObjectBase<InfantryItem>
    {
        public InfantryLayer() { }


        #region Public Methods - InfantryLayer
        #endregion
    }

    public class InfantryItem : ObjectItemBase, ICombatObject, IPosition
    {
        private int subcell = 4;


        #region Ctor - InfantryItem
        public InfantryItem(string _id, string[] _args) : base(_id, _args)
        {
            try
            {
                if (_args.Length != Constant.MapStructure.ArgLenInfantry)
                {
                    throw new Exception();
                }
                SubCell = int.Parse(_args[5]);
                Status = _args[6];
                Rotation = int.Parse(_args[7]);
                TaggedTrigger = _args[8];
                VeterancyPercentage = int.Parse(_args[9]);
                Group = _args[10];
                IsAboveGround = IniParseBool(_args[11]);
                AutoNORecruitType = IniParseBool(_args[12]);
                AutoYESRecruitType = IniParseBool(_args[13]);
                ObjectType = MapObjectType.Infantry;
            }
            catch
            {
                GlobalVar.Log.Critical(string.Format("Infantry item id: {0} has unreadable data, please verify in map file!", _id));
                ObjectType = MapObjectType.Infantry;
            }
        }
        public InfantryItem(InfantryItem src) : base(src)
        {
            SubCell = src.SubCell;
            Status = src.Status;
            Rotation = src.Rotation;
            TaggedTrigger = src.TaggedTrigger;
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
                OwnerHouse,
                RegName,
                HealthPoint.ToString(),
                X.ToString(),
                Y.ToString(),
                subcell.ToString(),
                Status,
                Rotation.ToString(),
                TaggedTrigger,
                VeterancyPercentage.ToString(),
                Group,
                IsAboveGround.ZeroOne(),
                AutoNORecruitType.ZeroOne(),
                AutoYESRecruitType.ZeroOne()
            };
        }
        public IMapObject ConstructFromParameter(string[] command)
        {
            ParameterReader reader = new ParameterReader(command);
            InfantryItem inf = new InfantryItem()
            {
                Id = reader.ReadString(),
                OwnerHouse = reader.ReadString(),
                RegName = reader.ReadString(),
                HealthPoint = reader.ReadInt(256),
                X = reader.ReadInt(),
                Y = reader.ReadInt(),
                subcell = reader.ReadInt(4),
                Status = reader.ReadString(),
                Rotation = reader.ReadInt(),
                TaggedTrigger = reader.ReadString(),
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
                    OwnerHouse, RegName, HealthPoint, X,Y,subcell,Status, Rotation,TaggedTrigger,VeterancyPercentage,Group,
                    IsAboveGround.ToInt(), AutoNORecruitType.ToInt(), AutoYESRecruitType.ToInt()
                };
            }
        }
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
        #endregion
    }
}
