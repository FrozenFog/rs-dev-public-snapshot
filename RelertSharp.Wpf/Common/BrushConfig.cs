using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.Common
{
    internal class ObjectBrushFilter : IObjectBrushFilter
    {
        #region Calls
        public bool OwnerHouse { get; set; } = true;
        public bool Facing { get; set; } = true;
        public bool Tag { get; set; } = true;
        public bool MissionStatus { get; set; } = true;
        public bool Veteran { get; set; } = true;
        public bool Group { get; set; } = true;
        public bool AboveGround { get; set; } = true;
        public bool HealthPoint { get; set; } = true;
        public bool RecruitYes { get; set; } = true;
        public bool RecruitNo { get; set; } = true;
        public bool Follows { get; set; } = true;
        public bool Sellable { get; set; } = true;
        public bool Rebuild { get; set; } = true;
        public bool Powered { get; set; } = true;
        public bool UpgradeNum { get; set; } = true;
        public bool Upg1 { get; set; } = true;
        public bool Upg2 { get; set; } = true;
        public bool Upg3 { get; set; } = true;
        public bool AiRepairable { get; set; } = true;
        public bool Spotlight { get; set; } = true;
        #endregion
    }
    internal class ObjectBrushConfig : IMapObjectBrushConfig
    {
        public ObjectBrushConfig()
        {
            OwnerHouse = GlobalVar.CurrentMapDocument.Map.Houses.First().Name;
        }


        #region Interface
        public string OwnerHouse { get; set; }

        public string RegName { get; set; }

        public I2dLocateable Pos { get; set; } = new Pnt();

        public int Height { get; set; }

        public int SubCell { get; set; } = -1;

        public int FacingRotation { get; set; }

        public string AttatchedTag { get; set; } = Constant.ITEM_NONE;

        public string MissionStatus { get; set; } = "Area Guard";

        public string Group { get; set; } = "-1";

        public int HealthPoint { get; set; } = 256;

        public int VeterancyPercentage { get; set; }

        public bool AboveGround { get; set; }

        public bool AutoRecruitYes { get; set; }

        public bool AutoRecruitNo { get; set; } = true;

        public string WaypointNum { get; set; } = "0";

        public byte OverlayIndex { get; set; }

        public byte OverlayFrame { get; set; }

        public string FollowsIndex { get; set; }

        public bool IsSellable { get; set; } = true;

        public bool AiRebuildable { get; set; }

        public bool Powered { get; set; } = true;

        public int UpgradeNum { get; set; }

        public string Upg1 { get; set; } = "None";

        public string Upg2 { get; set; } = "None";

        public string Upg3 { get; set; } = "None";

        public bool AiRepairable { get; set; }

        public BuildingSpotlightType SpotlightType { get; set; } = BuildingSpotlightType.None;
        #endregion

    }
}
