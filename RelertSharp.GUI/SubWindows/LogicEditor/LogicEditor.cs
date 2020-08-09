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
using RelertSharp.GUI.Controls;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal partial class LogicEditor : Form
    {
        private static RsLog Log { get { return GlobalVar.Log; } }
        private Map map { get { return GlobalVar.CurrentMapDocument.Map; } }
        private DescriptCollection descriptCollection = new DescriptCollection();
        private SoundManager soundPlayer = new SoundManager();

        private List<LocalVarItem> localVarList = new List<LocalVarItem>();
        private BindingSource localVarSource = new BindingSource();
        private Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }


        #region Ctor - LogicEditor
        public LogicEditor()
        {
            Log.Write("Logic Editor Initializing");
            InitializeComponent();
            SetLanguage();
            InitialTriggerPage();
            InitialTaskforcePage();
            SetGlobal();
            LoadTeamList();
            LoadAITrgList();
            LoadLocalVariables();
            LoadHouseList();

            pnlSearch.Initialize();
            lbxTriggerList.SelectedIndex = lbxTriggerList.Items.Count > 0 ? 0 : -1;
            Log.Write("Logic Editor Initialized");
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
        private void UpdateHouseAlliance()
        {
            lbxHouseAllie.BeginUpdate();
            lbxHouseEnemy.BeginUpdate();

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
            lbxHouseAllie.EndUpdate();
            lbxHouseEnemy.EndUpdate();
        }
        #endregion

        #region Trace Utils
        private async void ManageSound(TriggerParam param, TechnoPair p, bool isZero = false)
        {
            if (soundPlayer.IsPlaying)
            {
                soundPlayer.Stop();
                UseWaitCursor = false;
            }
            else
            {
                string name = soundPlayer.GetSoundName(p, (SoundType)param.ComboType, isZero);
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
        private TeamItem _CurrentTeam { get { return lbxTeamList.SelectedItem as TeamItem; } }
        private TeamUnit _CurrentTeamUnit { get { return _CurrentTeam.GetToUnit; } set { _CurrentTeam.GetToUnit = value; } }
        private AITriggerItem _CurrentAITrigger { get { return lbxAIList.SelectedItem as AITriggerItem; } }
        private AITriggerUnit _CurrentAITriggerUnit { get { return _CurrentAITrigger.GetToUnit; } set { _CurrentAITrigger.GetToUnit = value; } }
        private HouseItem _CurrentHouse { get { return lbxHouses.SelectedItem as HouseItem; } }
        private HouseUnit _CurrentHouseUnit { get { return _CurrentHouse.GetToUnit; } set { _CurrentHouse.GetToUnit = value; } }
        #endregion


        #region Public Calls
        public bool ChangeSaved { get; set; } = true;
        #endregion
    }
}
