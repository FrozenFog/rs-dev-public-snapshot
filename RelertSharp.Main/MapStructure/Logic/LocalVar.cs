using RelertSharp.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.MapStructure.Logic
{
    public class LocalVarCollection : IEnumerable<LocalVarItem>
    {
        //private Dictionary<string, bool> data = new Dictionary<string, bool>();
        private Dictionary<string, LocalVarItem> data = new Dictionary<string, LocalVarItem>();


        #region Ctor - LocalVarCollection
        public LocalVarCollection() { }
        #endregion


        #region Public Methods - LocalVarCollection
        public bool UpdateData(List<LocalVarItem> localVars)
        {
            if (localVars == null) return false;
            data.Clear();
            foreach (LocalVarItem localVar in localVars)
                data.Add(localVar.Id, localVar);
            return true;
        }
        public List<IniSystem.TechnoPair> ToTechno()
        {
            List<IniSystem.TechnoPair> result = new List<IniSystem.TechnoPair>();
            foreach (LocalVarItem var in this)
            {
                IniSystem.TechnoPair p = new IniSystem.TechnoPair(var.Id, var.Name);
                result.Add(p);
            }
            return result;
        }
        #region Enumerator
        public IEnumerator<LocalVarItem> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }
        #endregion
        #endregion


        #region Public Calls - LocalValCollection
        /// <summary>
        /// index,name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public LocalVarItem this[string name]
        {
            get { return data.Keys.Contains(name) ? data[name] : null; }
            set { data[name] = value; }
        }

        #endregion
    }


    public class LocalVarItem : IndexableItem
    {
        #region Ctor - LocalVarItem
        public LocalVarItem(string name, bool init, string index) : this(name, init, int.Parse(index)) { }
        public LocalVarItem(string name, bool init, int index)
        {
            Name = name;
            InitState = init;
            Id = index.ToString();
        }
        #endregion


        #region Public Calls - LocalVarItem
        public override string ToString() { return Id + ' ' + Name; }
        public bool InitState { get; set; }
        #endregion
    }
}
