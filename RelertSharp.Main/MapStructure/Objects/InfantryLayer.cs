using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;
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

    public class InfantryItem : ObjectItemBase, ICombatObject
    {
        private int subcell = 1;


        #region Ctor - InfantryItem
        public InfantryItem(string _id, string[] _args) : base(_id, _args)
        {
            try
            {
                if (_args.Length != Constant.MapStructure.ArgLenInfantry)
                {
                    throw new Exception();
                }
                SubCells = int.Parse(_args[5]);
                Status = _args[6];
                Rotation = int.Parse(_args[7]);
                TaggedTrigger = _args[8];
                VeterancyPercentage = int.Parse(_args[9]);
                Group = int.Parse(_args[10]);
                IsAboveGround = ParseBool(_args[11]);
                AutoNORecruitType = ParseBool(_args[12]);
                AutoYESRecruitType = ParseBool(_args[13]);
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
            SubCells = src.SubCells;
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
        public InfantryItem()
        {
            ObjectType = MapObjectType.Infantry;
        }
        #endregion


        #region Public Methods - InfantryItem
        public void MoveTo(I3dLocateable pos, int subcell)
        {
            if (subcell != -1) this.subcell = subcell;
            X = pos.X;
            Y = pos.Y;
            SceneObject.MoveTo(pos, subcell);
        }
        public override void MoveTo(I3dLocateable pos)
        {
            X = pos.X;
            Y = pos.Y;
            SceneObject.MoveTo(pos, subcell);
        }
        public override void ApplyAttributeFrom(ICombatObject src)
        {
            if (src is InfantryItem inf)
            {
                IsAboveGround = inf.IsAboveGround;
                AutoYESRecruitType = inf.AutoYESRecruitType;
                AutoNORecruitType = inf.AutoNORecruitType;
                base.ApplyAttributeFrom(src);
            }
        }
        #endregion


        #region Public Calls - InfantryItem
        public List<object> SaveData
        {
            get
            {
                return new List<object>()
                {
                    OwnerHouse, RegName, HealthPoint, X,Y,subcell,Status, Rotation,TaggedTrigger,VeterancyPercentage,Group,IsAboveGround, AutoNORecruitType,AutoYESRecruitType
                };
            }
        }
        public int SubCells
        {
            get { return subcell; }
            set
            {
                if (value >= 1 && value <= 3)
                {
                    subcell = value;
                }
                else
                {
                    subcell = 1;
                }
            }
        }
        #endregion


        #region Drawing
        public new PresentInfantry SceneObject { get { return (PresentInfantry)base.SceneObject; } set { base.SceneObject = value; } }
        IPresentBase IMapScenePresentable.SceneObject { get { return base.SceneObject; } set { base.SceneObject = value; } }
        #endregion
    }
}
