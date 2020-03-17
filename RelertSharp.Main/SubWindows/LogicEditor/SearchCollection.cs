using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.IniSystem;
using static RelertSharp.Language;

namespace RelertSharp.SubWindows.LogicEditor
{
    internal class SearchCollection : IEnumerable<SearchItem>
    {
        private Dictionary<string, SearchItem> data = new Dictionary<string, SearchItem>();
        private string keyword = "";


        #region Ctor - SearchCollection
        public SearchCollection() { }
        #endregion


        #region Public Methods - SearchCollection
        public void SetKeyword(string kw)
        {
            data.Clear();
            keyword = kw.ToLower();
        }
        public IEnumerable<ListViewItem> SearchIn(IEnumerable<IRegistable> srcs, SearchItem.SearchType type)
        {
            List<ListViewItem> result = new List<ListViewItem>();
            foreach (IRegistable reg in srcs)
            {
                if (reg.ID.ToLower().IndexOf(keyword) != -1 || reg.Name.ToLower().IndexOf(keyword) != -1)
                {
                    SearchItem item = new SearchItem(reg.ID);
                    item.Type = type;
                    item.Value = reg.Name;
                    data[item.Regname] = item;
                    result.Add((ListViewItem)item);
                }
            }
            return result;
        }
        #region Enumerator
        public IEnumerator<SearchItem> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }
        #endregion
        #endregion


        #region Public Calls - SearchCollection
        public int Length { get { return data.Count; } }
        #endregion
    }


    internal class SearchItem
    {
        public enum SearchType
        {
            LGCckbTrig, LGCckbTag, LGCckbLocal, LGCckbTeam, LGCckbTF, LGCckbTScp, LGCckbAiTrg, LGCckbHouse,
            LGCckbCsf, LGCckbTechno, LGCckbSnd, LGCckbEva, LGCckbMus, LGCckbAnim, LGCckbSuper, LGCckbGlobal,
            Undefined = -1
        }


        #region Ctor - SearchItem
        public SearchItem(string regname)
        {
            Regname = regname;
        }
        #endregion


        #region Public Methods - SearchItem
        public static explicit operator ListViewItem(SearchItem src)
        {
            ListViewItem dest = new ListViewItem();
            dest.Text = src.Regname;
            dest.SubItems.Add(DICT[src.Type.ToString()]);
            dest.SubItems.Add(src.Value);
            dest.SubItems.Add(src.ExValue);
            return dest;
        }
        #endregion


        #region Public Calls - SearchItem
        public SearchType Type { get; set; }
        public string Regname { get; set; }
        public string Value { get; set; }
        public string ExValue { get; set; }
        #endregion
    }
}
