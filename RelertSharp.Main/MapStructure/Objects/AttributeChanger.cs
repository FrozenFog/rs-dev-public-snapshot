using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.MapStructure.Objects
{
    public class AttributeChanger
    {
        #region Ctor - AttributeChanger
        public AttributeChanger(bool allTrue = false)
        {
            if (allTrue)
            {
                bOwnerHouse = true;
                bHealthPoint = true;
                bTaggedTrigger = true;
                bRotation = true;
                bVeteran = true;
                bGroup = true;
                bStatus = true;
            }
            Host = new InfantryItem();
        }
        public AttributeChanger()
        {
            Host = new InfantryItem();
        }
        #endregion


        #region Public Methods - AttributeChanger
        public static AttributeChanger FromCombatObject(ICombatObject src)
        {
            AttributeChanger dest = new AttributeChanger()
            {
                Host = src,
                bOwnerHouse = true,
                bHealthPoint = true,
                bTaggedTrigger = true,
                bRotation = true,
                bVeteran = true,
                bGroup = true,
                bStatus = true
            };
            return dest;
        }
        #endregion


        #region Public Calls - AttributeChanger
        public ICombatObject Host { get; set; }
        public bool bOwnerHouse { get; set; }
        public bool bHealthPoint { get; set; }
        public bool bTaggedTrigger { get; set; }
        public bool bRotation { get; set; }
        public bool bVeteran { get; set; }
        public bool bGroup { get; set; }
        public bool bStatus { get; set; }
        #endregion
    }
}
