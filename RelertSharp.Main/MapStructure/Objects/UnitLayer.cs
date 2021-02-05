using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;
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
                TaggedTrigger = _args[7];
                VeterancyPercentage = int.Parse(_args[8]);
                Group = int.Parse(_args[9]);
                IsAboveGround = IniParseBool(_args[10]);
                FollowsIndex = _args[11];
                AutoNORecruitType = IniParseBool(_args[12]);
                AutoYESRecruitType = IniParseBool(_args[13]);
                ObjectType = MapObjectType.Unit;
            }
            catch
            {
                GlobalVar.Log.Critical(string.Format("Unit item id: {0} has unreadable data, please verify in map file!", _id));
                ObjectType = MapObjectType.Unit;
            }
        }
        public UnitItem(UnitItem src) : base(src)
        {
            FollowsIndex = src.FollowsIndex;
            Rotation = src.Rotation;
            Status = src.Status;
            TaggedTrigger = src.TaggedTrigger;
            VeterancyPercentage = src.VeterancyPercentage;
            Group = src.Group;
            IsAboveGround = src.IsAboveGround;
            AutoNORecruitType = src.AutoNORecruitType;
            AutoYESRecruitType = src.AutoYESRecruitType;
            ObjectType = MapObjectType.Unit;
        }
        public UnitItem(string regname)
        {
            RegName = regname;
            ObjectType = MapObjectType.Unit;
        }
        public UnitItem()
        {
            ObjectType = MapObjectType.Unit;
        }


        #region Public Methods
        public override void ApplyAttributeFrom(ICombatObject src)
        {
            if (src is UnitItem u)
            {
                FollowsIndex = u.FollowsIndex;
                IsAboveGround = u.IsAboveGround;
                AutoYESRecruitType = u.AutoYESRecruitType;
                AutoNORecruitType = u.AutoNORecruitType;
                base.ApplyAttributeFrom(src);
            }
        }
        #endregion


        #region Public Calls
        public List<object> SaveData
        {
            get
            {
                return new List<object>()
                {
                    OwnerHouse, RegName, HealthPoint, X, Y, Rotation, Status, TaggedTrigger, VeterancyPercentage, Group, 
                    IsAboveGround.ToInt(), FollowsIndex, AutoNORecruitType.ToInt(),AutoYESRecruitType.ToInt()
                };
            }
        }
        public string FollowsIndex { get; set; }
        #endregion
    }
}
