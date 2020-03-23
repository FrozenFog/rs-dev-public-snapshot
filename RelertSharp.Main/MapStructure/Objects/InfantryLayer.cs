using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Objects
{
    public class InfantryLayer : ObjectBase<InfantryItem>
    {
        public InfantryLayer() { }
    }

    public class InfantryItem : ObjectItemBase
    {
        private int subcell;


        #region Ctor - InfantryItem
        public InfantryItem(string _id, string[] _args) : base(_id, _args)
        {
            ID = _id;
            OwnerHouse = _args[0];
            NameID = _args[1];
            HealthPoint = int.Parse(_args[2]);
            X = int.Parse(_args[3]);
            Y = int.Parse(_args[4]);
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
        #endregion


        #region Public Calls - InfantryItem
        public int SubCells
        {
            get { return subcell; }
            set { if (value >= 0 && value <= 2) subcell = value; }
        }
        #endregion
    }
}
