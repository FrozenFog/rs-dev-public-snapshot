using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Objects
{
    public class StructureLayer : ObjectBase<StructureItem>
    {
        public StructureLayer() { }


        #region Public Methods - StructureLayer
        public override StructureItem FindByCoord(I2dLocateable pos)
        {
            foreach (StructureItem item in this)
            {
                foreach (I2dLocateable target in new Square2D(item, item.SizeX, item.SizeY))
                {
                    if (target.X == pos.X && target.Y == pos.Y) return item;
                }
            }
            return null;
        }
        #endregion
    }


    public class StructureItem : ObjectItemBase, ICombatObject
    {
        private int sizeX = 0, sizeY = 0;


        public StructureItem(string _id, string[] _args) : base(_id, _args)
        {
            if (_args.Length != Constant.MapStructure.ArgLenStructure)
            {
                //logger
                return;
            }
            Rotation = int.Parse(_args[5]);
            TaggedTrigger = _args[6];
            AISellable = ParseBool(_args[7]);
            BuildingOnline = ParseBool(_args[9]);
            UpgradeNum = int.Parse(_args[10]);
            SpotlightType = (BuildingSpotlightType)(int.Parse(_args[11]));
            Upgrade1 = _args[12];
            Upgrade2 = _args[13];
            Upgrade3 = _args[14];
            AIRepairable = ParseBool(_args[15]);
        }
        #region Public Calls - StructureItem
        public bool AISellable { get; set; }
        public bool AIRebuildable { get; private set; } = false;
        public bool BuildingOnline { get; set; }
        public int UpgradeNum { get; set; }
        public BuildingSpotlightType SpotlightType { get; set; }
        public string Upgrade1 { get; set; }
        public string Upgrade2 { get; set; }
        public string Upgrade3 { get; set; }
        public bool AIRepairable { get; set; }
        public bool Nominal { get; private set; } = false;
        public int SizeX
        {
            get
            {
                if (sizeX == 0)
                {
                    GlobalVar.GlobalRules.GetBuildingShapeData(RegName, out int height, out int sizeX, out int sizeY);
                    this.sizeX = sizeX;
                    this.sizeY = sizeY;
                }
                return sizeX;
            }
        }
        public int SizeY
        {
            get
            {
                if (sizeY == 0)
                {
                    GlobalVar.GlobalRules.GetBuildingShapeData(RegName, out int height, out int sizeX, out int sizeY);
                }
                return sizeY;
            }
        }
        #endregion

    }
}
