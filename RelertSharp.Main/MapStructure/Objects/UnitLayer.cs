using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.FileSystem;
using RelertSharp.Common;
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
            if (_args.Length != 15)
            {
                //logger
                return;
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
        public string FollowsIndex { get; set; }
    }
}
