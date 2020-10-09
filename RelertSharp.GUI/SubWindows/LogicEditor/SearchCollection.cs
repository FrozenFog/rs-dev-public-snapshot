using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using static RelertSharp.Language;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal class SearchCollection : IEnumerable<SearchItem>
    {
        private Dictionary<string, SearchItem> data = new Dictionary<string, SearchItem>();
        private string keyword = "";
        private int previousIndex = -1;
        private bool decend = false;


        #region Ctor - SearchCollection
        public SearchCollection() { }
        #endregion


        #region Public Methods - SearchCollection
        public void SetKeyword(string kw)
        {
            data.Clear();
            keyword = kw.ToLower().TrimEnd(' ');
            previousIndex = -1;
        }
        public IEnumerable<ListViewItem> SearchIn(IEnumerable<ILogicItem> srcs, SearchItem.SearchType type)
        {
            List<ListViewItem> result = new List<ListViewItem>();
            foreach (ILogicItem reg in srcs)
            {
                if (reg.ID.ToLower().IndexOf(keyword) != -1 || reg.Name.ToLower().IndexOf(keyword) != -1)
                {
                    SearchItem item = new SearchItem(reg.ID);
                    item.Type = type;
                    item.Value = reg.Name;

                    item.DumpSubitems();
                    data[item.Regname] = item;
                    result.Add(item);
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


        #region Private Methods - SearchCollection
        #endregion


        #region Public Calls - SearchCollection
        public int Length { get { return data.Count; } }
        #endregion
    }


    internal class SearchItem : ListViewItem
    {
        private Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }
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
        public void DumpSubitems()
        {
            Text = Regname;
            SubItems.Add(DICT[Type.ToString()]);
            SubItems.Add(Value);
            SubItems.Add(ExValue);
        }
        #endregion


        #region Private Methods - SearchItem
        private string FindDetailInformation()
        {
            switch (Type)
            {
                case SearchType.LGCckbAiTrg:
                    AITriggerItem ai = Map.AiTriggers[Regname];
                    TeamItem t1 = Map.Teams[ai.Team1ID];
                    TeamItem t2 = Map.Teams[ai.Team2ID];
                    return string.Format("Use team:\nTeam1:{0} - {1}\nTeam2:{2} - {3}",
                        t1 == null ? "None" : t1.ID, t1 == null ? "" : t1.Name,
                        t2 == null ? "None" : t2.ID, t2 == null ? "" : t2.Name);
                case SearchType.LGCckbCsf:
                    return string.Format("ID:{0}\nContent:{1}", Regname, GlobalVar.GlobalCsf[Regname].ContentString);
                case SearchType.LGCckbSuper:
                case SearchType.LGCckbTechno:
                    return GlobalVar.GlobalRules[Regname].SaveString();
                default:
                    return string.Empty;
            }
        }
        #endregion


        #region Public Calls - SearchItem
        public SearchType Type { get; set; }
        public string Regname { get; set; }
        public string Value { get; set; }
        public string ExValue { get; set; }
        public string DetailDescription { get { return FindDetailInformation(); } }
        #endregion
    }
}
