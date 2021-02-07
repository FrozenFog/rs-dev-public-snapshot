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
    }
}
