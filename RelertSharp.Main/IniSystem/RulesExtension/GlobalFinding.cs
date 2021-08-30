using RelertSharp.Common;
using RelertSharp.MapStructure.Logic;
using System.Linq;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.IniSystem
{
    public static partial class RulesExtension
    {
        public static string GetCsfUIName(this Rules r, string regid)
        {
            if (!r.HasIniEnt(regid)) return GlobalCsf[regid].ContentString;
            string uiname = r[regid]["UIName"];
            return GlobalCsf[uiname].ContentString;
        }
        public static INIEntity GetFirstCountry(this Rules r)
        {
            INIEntity lsCountry = r[Constant.RulesHead.HEAD_COUNTRY];
            return r[lsCountry.First().Value];
        }
    }
}
