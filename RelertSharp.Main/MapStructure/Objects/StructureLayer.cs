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
    }


    public class StructureItem : ObjectItemBase
    {
        public StructureItem(string _id, string[] _args) : base(_id, _args)
        {
            ID = _id;
            OwnerHouse = _args[0];
            NameID = _args[1];
            HealthPoint = int.Parse(_args[2]);
            X = int.Parse(_args[3]);
            Y = int.Parse(_args[4]);
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
        #endregion

    }
}
