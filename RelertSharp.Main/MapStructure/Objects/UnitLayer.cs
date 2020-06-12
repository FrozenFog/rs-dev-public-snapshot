using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.FileSystem;
using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Objects
{
    public class UnitLayer : ObjectBase<UnitItem>
    {
        public UnitLayer() { }


        #region Public Calls - UnitLayer

        #endregion
    }

    public class UnitItem : ObjectItemBase, ICombatObject
    {
        public UnitItem(string _id, string[] _args) : base(_id, _args)
        {
            try
            {
                if (_args.Length != Constant.MapStructure.ArgLenUnit)
                {
                    throw new Exception();
                }
                Rotation = int.Parse(_args[5]);
                Status = _args[6];
                TaggedTrigger = _args[7];
                VeterancyPercentage = int.Parse(_args[8]);
                Group = int.Parse(_args[9]);
                IsAboveGround = ParseBool(_args[10]);
                FollowsIndex = _args[11];
                AutoNORecruitType = ParseBool(_args[12]);
                AutoYESRecruitType = ParseBool(_args[13]);
            }
            catch
            {
                GlobalVar.Log.Critical(string.Format("Unit item id: {0} has unreadable data, please verify in map file!", _id));
            }
        }
        public UnitItem(UnitItem src) : base(src)
        {
            FollowsIndex = src.FollowsIndex;
            Rotation = src.Rotation;
            Status = src.Status;
            TaggedTrigger = src.TaggedTrigger;
            VeterancyPercentage = src.VeterancyPercentage;
            Group = src.Group;
            IsAboveGround = src.IsAboveGround;
            AutoNORecruitType = src.AutoNORecruitType;
            AutoYESRecruitType = src.AutoYESRecruitType;
        }
        public UnitItem() { }
        public string FollowsIndex { get; set; }


        #region Public Methods

        #endregion


        #region Public Calls
        public new PresentUnit SceneObject { get { return (PresentUnit)base.SceneObject; } set { base.SceneObject = value; } }
        IPresentBase IMapScenePresentable.SceneObject { get { return base.SceneObject; } set { base.SceneObject = value; } }
        #endregion
    }
}
