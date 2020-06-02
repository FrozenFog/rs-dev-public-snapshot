using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.Language;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal partial class LogicEditor : Form
    {
        private Map map { get { return GlobalVar.CurrentMapDocument.Map; } }
        private DescriptCollection descriptCollection = new DescriptCollection();
        private SoundManager soundPlayer = new SoundManager();

        private List<LocalVarItem> localVarList = new List<LocalVarItem>();
        private BindingSource localVarSource = new BindingSource();
        private Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }


        #region Ctor - LogicEditor
        public LogicEditor()
        {
            InitializeComponent();
            SetLanguage();
            InitialTriggerPage();
            SetGlobal();
            UpdateTrgList(TriggerItem.DisplayingType.IDandName);
            LoadTaskforceList();
            LoadScriptList();
            LoadTeamList();
            LoadAITrgList();
            LoadLocalVariables();
            LoadHouseList();

            pnlSearch.Initialize();
            lbxTriggerList.SelectedIndex = lbxTriggerList.Items.Count > 0 ? 0 : -1;
        }
        #endregion


        #region Private Methods - LogicEditor
        #region Initialization Utils
        private void SetLanguage()
        {
            foreach (Control c in Controls) SetControlLanguage(c);
            Text = DICT[Text];
        }
        private void SetGlobal()
        {
            foreach (HouseItem house in map.Houses)
            {
                if (house.PlayerControl)
                {
                    CountryItem country = map.Countries.GetCountry(house.Country);
                    if (country == null) break;
                    GlobalVar.PlayerSide = country.Side;
                    break;
                }
            }
        }
        private void UpdateTrgList(TriggerItem.DisplayingType type = TriggerItem.DisplayingType.Remain)
        {
            lbxTriggerList.Tag = (int)type;
            map.Triggers.SetToString(type);
            LoadToObjectCollection(lbxTriggerList, map.Triggers);
            pnlTriggerTag.RefreshAttatchedList();
        }

        private void LoadTaskforceList()
        {
            LoadToObjectCollection(lbxTaskList, map.TaskForces);
            List<TechnoPair> technoPairs = new List<TechnoPair>();
            technoPairs.Add(new TechnoPair(string.Empty, "(Nothing)"));
            technoPairs.AddRange(GlobalVar.GlobalRules.InfantryList);
            technoPairs.AddRange(GlobalVar.GlobalRules.AircraftList);
            technoPairs.AddRange(GlobalVar.GlobalRules.VehicleList);
            foreach (TechnoPair techno in technoPairs)
                //if (!string.IsNullOrEmpty(techno.UIName))
                techno.ResetAbst(TechnoPair.AbstractType.CsfName, TechnoPair.IndexType.RegName);
            //technoPairs.Sort((x, y) => x.RegName.CompareTo(y.RegName));
            LoadToObjectCollection(cbbTaskType, technoPairs);
            if (lbxTaskList.Items.Count > 0) lbxTaskList.SelectedIndex = 0;
        }
        private void LoadScriptList()
        {
            LoadToObjectCollection(lbxScriptList, map.Scripts);
            if (lbxScriptList.Items.Count > 0) lbxScriptList.SelectedIndex = 0;
        }
        private void LoadTeamList()
        {
            LoadToObjectCollection(lbxTeamList, map.Teams);
            if (lbxTeamList.Items.Count > 0) lbxTeamList.SelectedIndex = 0;
        }
        private void LoadAITrgList()
        {
            LoadToObjectCollection(lbxAIList, map.AiTriggers);
            if (lbxAIList.Items.Count > 0) lbxAIList.SelectedIndex = 0;
        }
        private void LoadLocalVariables()
        {
            chklbxLocalVar.Items.Clear();
            chklbxLocalVar.DataSource = localVarSource;
            localVarList = map.LocalVariables.ToList();
            if (localVarList == null) return;
            localVarSource.DataSource = localVarList;
            for (int idx = 0, count = localVarList.Count; idx < count; ++idx)
                chklbxLocalVar.SetItemChecked(idx, localVarList[idx].InitState);
        }
        private void LoadHouseList()
        {
            LoadToObjectCollection(lbxHouses, map.Houses);
            if (lbxHouses.Items.Count > 0) lbxHouses.SelectedIndex = 0;
        }
        #endregion

        #region General GUI Update Utils
        private void UpdateTaskforceContent(int lvSelectedindex)
        {
            TaskforceItem taskforce = lbxTaskList.SelectedItem as TaskforceItem;
            txbTaskName.Text = taskforce.Name;
            mtxbTaskGroup.Text = taskforce.Group.ToString();
            txbTaskID.Text = taskforce.ID;

            imglstPcx.Images.Clear();
            lvTaskforceUnits.SelectedIndices.Clear();
            lvTaskforceUnits.Items.Clear();
            if (taskforce.Members.Count > 0)
            {
                Dictionary<string, Image> dict = GlobalVar.GlobalDir.GetPcxImages(taskforce.MemberPcxNames);
                foreach (string key in dict.Keys)
                {
                    imglstPcx.Images.Add(key, dict[key]);
                }
                IEnumerable<TaskforceUnit> units = taskforce.Members;
                IEnumerable<ListViewItem> items = TaskforceItem.ToListViewItems(units);
                LoadToObjectCollection(lvTaskforceUnits, items);
                lvTaskforceUnits.SelectedIndices.Add(lvSelectedindex);
            }
        }
        private void UpdateHouseAlliance()
        {
            lbxHouseAllie.Items.Clear();
            lbxHouseEnemy.Items.Clear();
            txbHouseAllies.Clear();
            string displayAllies = _CurrentHouse.Name;
            List<string> enemies = new List<string>();
            List<string> allies = _CurrentHouse.AlliesWith;
            try { allies.Remove(_CurrentHouse.Name); }
            catch { }
            for (int i = allies.Count - 1; i >= 0; i--)
                if (!map.Houses.ValueExists(map.Houses.GetHouse(allies[i])))  
                    allies.RemoveAt(i);
            if (allies.Count > 0) displayAllies += ",";
            for (int i = 0; i < allies.Count - 1; i++)
                displayAllies += (allies[i] + ",");
            displayAllies += allies.LastOrDefault();
            txbHouseAllies.Text = displayAllies;

            lbxHouseAllie.Items.AddRange(allies.ToArray());

            foreach (HouseItem house in map.Houses)
                if (!_CurrentHouse.AlliesWith.Exists(s => s == house.Name) && _CurrentHouse.Name != house.Name)
                    enemies.Add(house.Name);
            lbxHouseEnemy.Items.AddRange(enemies.ToArray());
        }
        #endregion

        #region Trace Utils
        private async void ManageSound(TriggerParam param, TechnoPair p)
        {
            if (soundPlayer.IsPlaying)
            {
                soundPlayer.Stop();
                UseWaitCursor = false;
            }
            else
            {
                string name = soundPlayer.GetSoundName(p, (SoundType)param.ComboType);
                await Task.Run(() =>
                {
                    soundPlayer.LoadWav(GlobalVar.GlobalSoundBank.GetSound(name));
                });
                soundPlayer.Play();
                UseWaitCursor = false;
            }
        }
        #endregion

        #region Misc Utils
        #endregion

        #endregion


        #region Private Calls - LogicEditor
        private TaskforceItem _CurrentTaskforce { get { return map.TaskForces[txbTaskID.Text]; } }
        private TaskforceUnit _CurrentBoxTaskforceUnit { get { return TaskforceUnit.FromListviewItem(lvTaskforceUnits.SelectedItems[0]); } }
        private TaskforceUnit _CurrentTaskforceUnit { get { return _CurrentTaskforce.Members[lvTaskforceUnits.SelectedIndices[0]]; } }
        private TeamItem _CurrentTeam { get { return lbxTeamList.SelectedItem as TeamItem; } }
        private TeamUnit _CurrentTeamUnit { get { return _CurrentTeam.GetToUnit; } set { _CurrentTeam.GetToUnit = value; } }
        private AITriggerItem _CurrentAITrigger { get { return lbxAIList.SelectedItem as AITriggerItem; } }
        private AITriggerUnit _CurrentAITriggerUnit { get { return _CurrentAITrigger.GetToUnit; } set { _CurrentAITrigger.GetToUnit = value; } }
        private HouseItem _CurrentHouse { get { return lbxHouses.SelectedItem as HouseItem; } }
        private HouseUnit _CurrentHouseUnit { get { return _CurrentHouse.GetToUnit; } set { _CurrentHouse.GetToUnit = value; } }
        #endregion
    }
}
