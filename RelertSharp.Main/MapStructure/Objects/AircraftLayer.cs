using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.DrawingEngine.Presenting;
using RelertSharp.Common;
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
            if (_args.Length != Constant.MapStructure.ArgLenAircraft)
            {
                //logger
                return;
            }
            Rotation = int.Parse(_args[5]);
            Status = _args[6];
            TaggedTrigger = _args[7];
            VeterancyPercentage = int.Parse(_args[8]);
            Group = int.Parse(_args[9]);
            AutoNORecruitType = ParseBool(_args[10]);
            AutoYESRecruitType = ParseBool(_args[11]);
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
        }


        #region Public Calls
        public new PresentUnit SceneObject { get { return (PresentUnit)base.SceneObject; } set { base.SceneObject = value; } }
        IPresentBase IMapScenePresentable.SceneObject { get { return base.SceneObject; } set { base.SceneObject = value; } }
        #endregion
    }
}
