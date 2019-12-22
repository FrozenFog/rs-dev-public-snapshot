using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relert_sharp.MapStructure.Logic
{
    public class LocalVarCollection
    {
        private Dictionary<string, bool> data = new Dictionary<string, bool>();
        public LocalVarCollection() { }
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
    }
}
