using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.IniSystem
{
    public interface IIniEntitySerializable
    {
        void ReadFromIni(INIEntity src);
        INIEntity SaveAsIni();
    }
    public interface IIniPairSerializable
    {
        void ReadFromIni(INIPair src);
        INIPair SaveAsIni();
    }
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class IniEntitySerializeAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string entName;

        // This is a positional argument
        public IniEntitySerializeAttribute(string iniEntName)
        {
            this.entName = iniEntName;
        }

        public string EntName
        {
            get { return entName; }
        }
    }
    public static class IniSerializeExtension
    {
        internal static string GetEntName(this IIniEntitySerializable src)
        {
            var attr = src.GetType().GetCustomAttributes(typeof(IniEntitySerializeAttribute), true).FirstOrDefault() as IniEntitySerializeAttribute;
            if (attr != null) return attr.EntName;
            return string.Empty;
        }
        internal static INIEntity GetNamedEnt(this IIniEntitySerializable src)
        {
            return new INIEntity(src.GetEntName());
        }
    }
}
