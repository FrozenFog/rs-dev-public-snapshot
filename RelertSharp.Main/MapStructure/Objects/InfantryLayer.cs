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
        public InfantryItem FindByCoord(I2dLocateable pos, int subcell)
        {
            foreach (InfantryItem inf in this)
            {
                if (inf.X == pos.X && inf.Y == pos.Y && inf.SubCells == subcell) return inf;
            }
            return null;
        }
        public void RemoveByCoord(I2dLocateable pos, int subcell)
        {
            Dictionary<string, InfantryItem> tmp = new Dictionary<string, InfantryItem>(GetDictionary());
            foreach(InfantryItem inf in tmp.Values)
            {
                if (inf.X == pos.X & inf.Y == pos.Y && inf.SubCells == subcell)
                {
                    RemoveItem(inf);
                    return;
                }
            }
        }
        #endregion
    }

    public class InfantryItem : ObjectItemBase
    {
        private int subcell;


        #region Ctor - InfantryItem
        public InfantryItem(string _id, string[] _args) : base(_id, _args)
        {
            ID = _id;
            OwnerHouse = _args[0];
            RegName = _args[1];
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
