using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Objects
{
    public class StructureLayer : ObjectBase<StructureItem>
    {
        public StructureLayer() { }


        #region Public Methods - StructureLayer
        #endregion
    }


    public class StructureItem : ObjectItemBase, ICombatObject
    {
        private int sizeX = 0, sizeY = 0;


        #region Ctor
        public StructureItem(string _id, string[] _args) : base(_id, _args)
        {
            try
            {
                if (_args.Length != Constant.MapStructure.ArgLenStructure)
                {
                    throw new Exception();
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
            catch
            {
                GlobalVar.Log.Critical(string.Format("Building item id: {0} has unreadable data, please verify in map file!", _id));
            }
        }
        public StructureItem(StructureItem src) : base(src)
        {
            Rotation = src.Rotation;
            TaggedTrigger = src.TaggedTrigger;
            AISellable = src.AISellable;
            BuildingOnline = src.BuildingOnline;
            UpgradeNum = src.UpgradeNum;
            SpotlightType = src.SpotlightType;
            Upgrade1 = src.Upgrade1;
            Upgrade2 = src.Upgrade2;
            Upgrade3 = src.Upgrade3;
            AIRepairable = src.AIRepairable;
        }
        public StructureItem(string regname)
        {
            RegName = regname;
        }


        #endregion
        #region Public Calls - StructureItem
        public bool AISellable { get; set; }
        public bool AIRebuildable { get; private set; } = false;
        public bool BuildingOnline { get; set; }
        public int UpgradeNum { get; set; }
        public BuildingSpotlightType SpotlightType { get; set; }
        public string Upgrade1 { get; set; } = "None";
        public string Upgrade2 { get; set; } = "None";
        public string Upgrade3 { get; set; } = "None";
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


        #region Drawing
        public new PresentStructure SceneObject { get { return (PresentStructure)base.SceneObject; } set { base.SceneObject = value; } }
        IPresentBase IMapScenePresentable.SceneObject { get { return base.SceneObject; } set { base.SceneObject = value; } }
        #endregion
    }
}
