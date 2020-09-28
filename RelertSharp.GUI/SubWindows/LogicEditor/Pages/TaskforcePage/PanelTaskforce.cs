using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal partial class PanelTaskforce : UserControl
    {
        internal event TaskforceHandler TaskforceNameUpdated;
        internal event TaskforceHandler TaskforceAdded;
        internal event TaskforceHandler TaskforceDeleted;


        internal TaskforceItem CurrentCollection { get; set; }
        internal TaskforceUnit CurrentUnit { get; set; }
        private Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }


        private ListBox lbxTaskList;
        private Dictionary<string, Image> pcxDictionary = new Dictionary<string, Image>();


        public PanelTaskforce()
        {
            InitializeComponent();
        }


        public void Initialize(ListBox lbx)
        {
            List<TechnoPair> technoPairs = new List<TechnoPair>();
            technoPairs.Add(new TechnoPair(string.Empty, "(Nothing)"));
            technoPairs.AddRange(GlobalVar.GlobalRules.InfantryList);
            technoPairs.AddRange(GlobalVar.GlobalRules.AircraftList);
            technoPairs.AddRange(GlobalVar.GlobalRules.VehicleList);
            technoPairs.ForEach(x => x.ResetAbst(TechnoPair.AbstractType.CsfName, TechnoPair.IndexType.RegName));
            cbbTaskType.LoadAs(technoPairs);
            SetLanguage();
            lbxTaskList = lbx;
        }
        public void Reload(TaskforceItem item)
        {
            CurrentCollection = item;
            if (CurrentCollection.Members.Count > 0) CurrentUnit = CurrentCollection.Members.First();
            RefreshControl();
        }


        #region OnChanging
        protected virtual void OnTaskforceNameChanged()
        {
            TaskforceNameUpdated?.Invoke(this, CurrentCollection);
        }
        protected virtual void OnNewTaskforceCreated()
        {
            TaskforceAdded?.Invoke(this, CurrentCollection);
        }
        protected virtual void OnTaskforceDeleted()
        {
            TaskforceDeleted?.Invoke(this, CurrentCollection);
        }
        #endregion


        private void SetLanguage()
        {
            foreach (Control c in Controls) Language.SetControlLanguage(c);
        }
        private void GetPcx(string regname)
        {
            string pcxName = GlobalVar.GlobalRules.GetPcxName(regname);
            if (!pcxDictionary.Keys.Contains(pcxName))
            {
                Image pcx = GlobalVar.GlobalDir.GetPcxImage(regname);
                pcxDictionary[pcxName] = pcx;
                if (pcx != null)
                    imglstPcx.Images.Add(pcxName, pcx);
                else
                    imglstPcx.Images.Add(pcxName, new Bitmap(60, 30));
            }
        }
        private void ClearPcx()
        {
            imglstPcx.Images.Clear();
            pcxDictionary.Clear();
        }
        private bool isControlRefreshing = false;
        private void RefreshControl()
        {
            isControlRefreshing = true;
            if (CurrentCollection != null)
            {
                txbTaskID.Text = CurrentCollection.ID;
                txbTaskName.Text = CurrentCollection.Name;
                mtxbTaskGroup.Text = CurrentCollection.Group.ToString();
                ClearPcx();
                lvTaskforceUnits.SelectedItems.Clear();
                lvTaskforceUnits.Items.Clear();
                if (CurrentCollection.Members.Count > 0)
                {
                    pcxDictionary = GlobalVar.GlobalDir.GetPcxImages(CurrentCollection.MemberPcxNames);
                    foreach (string key in pcxDictionary.Keys)
                    {
                        imglstPcx.Images.Add(key, pcxDictionary[key]);
                    }
                    IEnumerable<TaskforceUnit> units = CurrentCollection.Members;
                    IEnumerable<ListViewItem> items = TaskforceItem.ToListViewItems(units);
                    lvTaskforceUnits.LoadAs(items);
                    lvTaskforceUnits.SelectedIndices.Add(0);
                    cbbTaskType.Text = CurrentUnit.RegName;
                    mtxbTaskNum.Text = CurrentUnit.UnitNum.ToString();
                }
            }
            else
            {
                foreach (Control c in Controls) c.ClearContent();
            }
            isControlRefreshing = false;
        }
        private void btnNewTask_Click(object sender, EventArgs e)
        {
            TaskforceItem item = Map.NewTaskforce();
            CurrentCollection = item;
            OnNewTaskforceCreated();
        }

        private void btnDelTask_Click(object sender, EventArgs e)
        {
            if (CurrentCollection == null) return;
            Map.RemoveTaskforce(CurrentCollection);
            OnTaskforceDeleted();
        }

        private void btnCopyTask_Click(object sender, EventArgs e)
        {
            if (CurrentCollection == null) return;
            TaskforceItem copied = CurrentCollection.Copy(Map.NewID);
            Map.TaskForces[copied.ID] = copied;
            CurrentCollection = copied;
            OnNewTaskforceCreated();
        }

        private void btnAddTaskMem_Click(object sender, EventArgs e)
        {
            if (CurrentCollection == null) return;
            //if (lvTaskforceUnits.Items.Count > 5) return; // ?
            TechnoPair p = cbbTaskType.Items[0] as TechnoPair;
            CurrentUnit = CurrentCollection.NewUnitItem(p.RegName, 1);
            GetPcx(CurrentUnit.RegName);
            lvTaskforceUnits.Items.Add(CurrentUnit.ToListviewItem());
            lvTaskforceUnits.SelectedIndices.Clear();
            lvTaskforceUnits.SelectedIndices.Add(lvTaskforceUnits.Items.Count - 1);
        }

        private void btnDelTaskMem_Click(object sender, EventArgs e)
        {
            if (CurrentCollection == null || CurrentUnit == null) return;
            int index = lvTaskforceUnits.SelectedIndices[0];
            CurrentCollection.Members.RemoveAt(lvTaskforceUnits.SelectedIndices[0]);
            CurrentUnit = null;
            lvTaskforceUnits.RemoveAt(index, ref updatingLvTaskforceUnits);
        }

        private void btnCopyTaskMem_Click(object sender, EventArgs e)
        {
            //if (lbxTaskList.SelectedItem == null || lvTaskforceUnits.SelectedIndices.Count < 1) return;
            //if (lvTaskforceUnits.Items.Count > 5) return;
            //TechnoPair p = cbbTaskType.SelectedItem as TechnoPair;
            //TaskforceUnit u = TaskforceUnit.FromListviewItem(lvTaskforceUnits.SelectedItems[0]);
            //TaskforceUnit newunit = _CurrentTaskforce.NewUnitItem(u.RegName, u.UnitNum);
            //int index = lvTaskforceUnits.Items.Count;
            //UpdateTaskforceContent(index);
        }

        private bool updatingLvTaskforceUnits = false;
        private void lvTaskforceUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updatingLvTaskforceUnits && !isControlRefreshing)
            {
                if (lvTaskforceUnits.SelectedIndices.Count == 1)
                {
                    isControlRefreshing = true;
                    CurrentUnit = CurrentCollection.Members[lvTaskforceUnits.SelectedIndices[0]];
                    cbbTaskType.Text = CurrentUnit.RegName;
                    mtxbTaskNum.Text = CurrentUnit.UnitNum.ToString();
                    isControlRefreshing = false;
                }
                else CurrentUnit = null;
            }
        }

        private void txbTaskName_Validated(object sender, EventArgs e)
        {
            if (!isControlRefreshing)
            {
                CurrentCollection.Name = txbTaskName.Text;
                OnTaskforceNameChanged();
            }
        }

        private void cbbTaskType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!isControlRefreshing)
            {
                TechnoPair p = cbbTaskType.SelectedItem as TechnoPair;
                if (p != null && CurrentUnit != null)
                {
                    CurrentUnit.RegName = p.RegName;
                    GetPcx(CurrentUnit.RegName);
                    lvTaskforceUnits.UpdateAt(CurrentUnit.ToListviewItem(), ref updatingLvTaskforceUnits);
                }
            }
        }

        private void mtxbTaskNum_Validated(object sender, EventArgs e)
        {
            if (!isControlRefreshing)
            {
                if (CurrentUnit != null)
                {
                    CurrentUnit.UnitNum = int.Parse(mtxbTaskNum.Text);
                    lvTaskforceUnits.UpdateAt(CurrentUnit.ToListviewItem(), ref updatingLvTaskforceUnits);
                }
            }
        }

        private void mtxbTaskGroup_Validated(object sender, EventArgs e)
        {
            if (!isControlRefreshing)
            {
                if (CurrentCollection != null)
                {
                    try
                    {
                        CurrentCollection.Group = int.Parse(mtxbTaskGroup.Text);
                    }
                    catch
                    {
                        CurrentCollection.Group = -1;
                        mtxbTaskGroup.Text = "-1";
                    }
                }
            }
        }
    }
}
