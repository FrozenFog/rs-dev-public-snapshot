using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.MapStructure.Logic
{
    public class LocalVarCollection
    {
        private Dictionary<string, bool> data = new Dictionary<string, bool>();


        #region Ctor - LocalVarCollection
        public LocalVarCollection() { }
        #endregion


        #region Public Methods - LocalVarCollection
        public List<IniSystem.TechnoPair> ToTechno()
        {
            List<IniSystem.TechnoPair> result = new List<IniSystem.TechnoPair>();
            foreach (string name in data.Keys)
            {
                string[] tmp = name.Split(new char[] { ',' });
                IniSystem.TechnoPair p = new IniSystem.TechnoPair(tmp[0], tmp[1]);
                result.Add(p);
            }
            return result;
        }
        #endregion


        #region Public Calls - LocalValCollection
        /// <summary>
        /// index,name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool this[string name]
        {
            get
            {
                if (data.Keys.Contains(name)) return data[name];
                return false;
            }
            set
            {
                data[name] = value;
            }
        }
        #endregion
    }
}
