using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                data.Add(localVar.Index.ToString(), localVar);
            return true;
        }
        public List<IniSystem.TechnoPair> ToTechno()
        {
            List<IniSystem.TechnoPair> result = new List<IniSystem.TechnoPair>();
            foreach (LocalVarItem var in this)
            {
                IniSystem.TechnoPair p = new IniSystem.TechnoPair(var.Index.ToString(), var.Name);
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
        public LocalVarItem this[string name] { 
            get { return data.Keys.Contains(name) ? data[name] : null; }
            set { data[name] = value; }
        }
        
        #endregion
    }


    public class LocalVarItem : IniSystem.IRegistable
    {
        #region Ctor - LocalVarItem
        public LocalVarItem(string name, bool init, string index) : this(name, init, int.Parse(index)) { }
        public LocalVarItem(string name, bool init, int index)
        {
            Name = name;
            InitState = init;
            Index = index;
        }
        #endregion


        #region Public Calls - LocalVarItem
        public override string ToString() { return ID + ' ' + Name; }
        public string Name { get; set; }
        public bool InitState { get; set; }
        public int Index { get; set; }
        public string ID { get { return Index.ToString(); } }
        #endregion
    }
}
