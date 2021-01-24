using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;
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
                TaggedTrigger = _args[7];
                VeterancyPercentage = int.Parse(_args[8]);
                Group = int.Parse(_args[9]);
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
            TaggedTrigger = src.TaggedTrigger;
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


        #region Public Methods
        public override void ApplyAttributeFrom(ICombatObject src)
        {
            if (src is AircraftItem a)
            {
                AutoYESRecruitType = a.AutoYESRecruitType;
                AutoNORecruitType = a.AutoNORecruitType;
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
                    AutoNORecruitType.ToInt(), AutoYESRecruitType.ToInt()
                };
            }
        }
        public new PresentUnit SceneObject { get { return (PresentUnit)base.SceneObject; } set { base.SceneObject = value; } }
        IPresentBase IMapScenePresentable.SceneObject { get { return base.SceneObject; } set { base.SceneObject = value; } }
        #endregion
    }
}
