using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
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
            if (_args.Length != Constant.MapStructure.ArgLenInfantry)
            {
                //logger
                return;
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
        }
        public InfantryItem() { }
        #endregion


        #region Public Methods - InfantryItem
        public void MoveTo(I2dLocateable pos, int subcell)
        {
            if (subcell != -1) this.subcell = subcell;
            MoveTo(pos);
        }
        #endregion


        #region Public Calls - InfantryItem
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
    }
}
