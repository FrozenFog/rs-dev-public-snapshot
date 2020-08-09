using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using RelertSharp.MapStructure.Logic;
using RelertSharp.FileSystem;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.Language;
using static RelertSharp.GUI.GuiUtils;
using BrightIdeasSoftware;
using RelertSharp.SubWindows.LogicEditor;
using System.CodeDom;
using RelertSharp.Utils;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal partial class LogicEditor : Form
    {
        #region Team
        #region btn
        private void btnNewTeam_Click(object sender, EventArgs e)
        {
            TeamItem item = map.NewTeam();
            AddTo(lbxTeamList, item, ref updatingLbxTeamList);
        }

        private void btnDelTeam_Click(object sender, EventArgs e)
        {
            if (lbxTeamList.SelectedItem == null) return;
            int index = lbxTeamList.SelectedIndex;
            TeamItem item = lbxTeamList.SelectedItem as TeamItem;
            map.RemoveTeam(item);
            RemoveAt(lbxTeamList, index, ref updatingLbxTeamList);
        }

        private void btnCopyTeam_Click(object sender, EventArgs e)
        {
            if (lbxTeamList.SelectedItem == null) return;
            TeamItem copied = _CurrentTeam.Copy(map.NewID);
            map.Teams[copied.ID] = copied;
            AddTo(lbxTeamList, copied, ref updatingLbxTeamList);
        }
        #endregion
        #region lbx
        private bool updatingLbxTeamList = false;
        private void lbxTeamList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxTeamList.SelectedItem == null) return;
            if (!updatingLbxTeamList)
            {
                olvTeamConfig.ClearObjects();
                olvTeamConfig.SetObjects(_CurrentTeamUnit.Data);
            }
        }
        #endregion
        #region olv
        private void olvTeamConfig_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if (e.SubItemIndex != 1) return;
            KeyValuePair<string, TeamUnit.TeamUnitNode> valuePair = (KeyValuePair<string, TeamUnit.TeamUnitNode>)e.RowObject;
            ComboBox cmb = new ComboBox();
            /*
            if (e.Control.GetType().FullName == "BrightIdeasSoftware.BooleanCellEditor2")
            {
                cmb.FlatStyle = FlatStyle.Flat;
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb.Bounds = e.CellBounds;
                cmb.Items.Add("False");
                cmb.Items.Add("True");
                cmb.SelectedIndex = ((bool)e.Value == false) ? 0 : 1;
                e.Control = cmb;
            }
            
            else */if (e.Control.GetType().FullName == "BrightIdeasSoftware.EnumCellEditor")
            {
                cmb.FlatStyle = FlatStyle.Flat;
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb.Bounds = e.CellBounds;
                Type type;
                switch (valuePair.Key)
                {
                    case "VeteranLevel":
                        type = typeof(TeamVeteranLevel);
                        break;
                    case "MCDecision":
                    default:
                        type = typeof(TeamMCDecision);
                        break;
                }
                StaticHelper.LoadToObjectCollection(cmb, type);
                int idxE;
                for (idxE = 0; idxE < cmb.Items.Count; idxE++)
                    if ((EnumDisplayClass)(cmb.Items[idxE]) == (int)valuePair.Value.Value)
                        break;
                try { cmb.SelectedIndex = idxE; } catch { }
                e.Control = cmb;
            }
            else if (valuePair.Key == "Waypoint")
            {
                cmb.FlatStyle = FlatStyle.Flat;
                cmb.DropDownStyle = ComboBoxStyle.DropDown;
                cmb.Bounds = e.CellBounds;
                var waypoints = GlobalVar.CurrentMapDocument.Map.Waypoints.ToList();
                waypoints.Sort((a, b) => int.Parse(a.Num) - int.Parse(b.Num));
                LoadToObjectCollection(cmb, waypoints.AsEnumerable());
                cmb.SelectedItem = waypoints.Find(s => s.Num == e.Control.Text);
                if (cmb.SelectedItem == null && waypoints.Count > 0)  cmb.SelectedIndex = 0;
                e.Control = cmb;
            }
            else switch (valuePair.Key) 
                {
                    case "TechLevel":
                            cmb.FlatStyle = FlatStyle.Flat;
                            cmb.DropDownStyle = ComboBoxStyle.DropDown;
                            cmb.Bounds = e.CellBounds;
                            cmb.Items.Add("0");
                            cmb.Items.Add("1");
                            cmb.Items.Add("2");
                            cmb.Items.Add("3");
                            cmb.Items.Add("4");
                            cmb.Items.Add("5");
                            cmb.Items.Add("6");
                            cmb.Items.Add("7");
                            cmb.Items.Add("8");
                            cmb.Items.Add("9");
                            cmb.Items.Add("10");
                            cmb.Text = e.Control.Text;
                            e.Control = cmb;
                            break;
                    case "TaskforceID":
                        cmb.FlatStyle = FlatStyle.Flat;
                        cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                        cmb.Bounds = e.CellBounds;
                        cmb.Items.AddRange(GlobalVar.CurrentMapDocument.Map.TaskForces.ToArray());
                        cmb.SelectedItem = GlobalVar.CurrentMapDocument.Map.TaskForces.ToList().Find(s => s.ID == e.Control.Text);
                        e.Control = cmb;
                        break;
                    case "ScriptID":
                        cmb.FlatStyle = FlatStyle.Flat;
                        cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                        cmb.Bounds = e.CellBounds;
                        cmb.Items.AddRange(GlobalVar.CurrentMapDocument.Map.Scripts.ToArray());
                        cmb.SelectedItem = GlobalVar.CurrentMapDocument.Map.Scripts.ToList().Find(s => s.ID == e.Control.Text);
                        e.Control = cmb;
                        break;
                    case "TagID":
                        cmb.FlatStyle = FlatStyle.Flat;
                        cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                        cmb.Bounds = e.CellBounds;
                        cmb.Items.Add("None");
                        cmb.Items.AddRange(GlobalVar.CurrentMapDocument.Map.Tags.ToArray());
                        if (string.IsNullOrEmpty(e.Control.Text)) cmb.SelectedIndex = 0;
                        else cmb.SelectedItem = GlobalVar.CurrentMapDocument.Map.Tags.ToList().Find(s => s.ID == e.Control.Text);
                        e.Control = cmb;
                        break;
                    case "House":
                        cmb.Bounds = e.CellBounds;
                        cmb.FlatStyle = FlatStyle.Flat;
                        cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                        cmb.Items.AddRange(map.Countries.ToArray());
                        CountryItem country = map.Countries.GetCountry((string)valuePair.Value.Value);
                        if (country == null) cmb.SelectedIndex = 0;
                        else cmb.SelectedItem = country;
                        Utils.Misc.AdjustComboBoxDropDownWidth(ref cmb);
                        e.Control = cmb;
                        break;
                    default:
                        e.Control.Bounds = e.CellBounds;
                        break;
                }
            Utils.Misc.AdjustComboBoxDropDownWidth(ref cmb);
        }
        private void olvTeamConfig_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if(!e.Cancel && e.SubItemIndex == 1)
            {
                // Do save works
                KeyValuePair<string, TeamUnit.TeamUnitNode> valuePair = (KeyValuePair<string, TeamUnit.TeamUnitNode>)e.RowObject;
                TeamUnit.TeamUnitNode ret = _CurrentTeamUnit.Data[valuePair.Key] = new TeamUnit.TeamUnitNode(valuePair.Value.ShowName, valuePair.Value.Value);
                // _CurrentTeamUnit.Data[valuePair.Key].Value;
                if (Constant.TeamBoolIndex.Contains(valuePair.Key))
                {
                    //ComboBox cmb = e.Control as ComboBox;
                    //ret.Value = cmb.SelectedIndex == 0 ? false : true;
                    //_CurrentTeamUnit.Data[valuePair.Key] = ret;
                    CheckBox ckb = e.Control as CheckBox;
                    ret.Value = ckb.Checked;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "Waypoint")
                {
                    ComboBox cmb = e.Control as ComboBox;
                    int val = 0;
                    try
                    {
                        string text = cmb.Text.Split('-')[0].Trim();
                        val = int.Parse(text);
                    }
                    catch
                    {
                        val = 0;
                    }
                    if (val < 0 || val > 701) val = 0;
                    if (GlobalVar.CurrentMapDocument.Map.Waypoints.ToList().Find(s => s.Num == val.ToString()) == null) val = 0;
                    ret.Value = val;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "TechLevel")
                {
                    ComboBox cmb = e.Control as ComboBox;
                    string text = cmb.Text;
                    int val = 0;
                    try
                    {
                        val = int.Parse(text);
                    }
                    catch
                    {
                        val = 0;
                    }
                    ret.Value = val;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "Group")
                {
                    string text = e.Control.Text;
                    int val = -1;
                    try
                    {
                        val = int.Parse(text);
                    }
                    catch
                    {
                        val = -1;
                    }
                    ret.Value = val;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "Priority")
                {
                    string text = e.Control.Text;
                    int val = 0;
                    try
                    {
                        val = int.Parse(text);
                    }
                    catch
                    {
                        val = 0;
                    }
                    ret.Value = val;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "TeamCapacity")
                {
                    string text = e.Control.Text;
                    int val = 0;
                    try
                    {
                        val = int.Parse(text);
                    }
                    catch
                    {
                        val = 0;
                    }
                    ret.Value = val;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "VeteranLevel")
                {
                    ret.Value = (TeamVeteranLevel)((EnumDisplayClass)((ComboBox)e.Control).SelectedItem).Value;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "MCDecision")
                {
                    ret.Value = (TeamMCDecision)((EnumDisplayClass)((ComboBox)e.Control).SelectedItem).Value;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "TaskforceID")
                {
                    ComboBox cmb = e.Control as ComboBox;
                    ret.Value = ((TaskforceItem)cmb.SelectedItem).ID;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "ScriptID")
                {
                    ComboBox cmb = e.Control as ComboBox;
                    ret.Value = ((TeamScriptGroup)cmb.SelectedItem).ID;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "TagID")
                {
                    ComboBox cmb = e.Control as ComboBox;
                    ret.Value = cmb.Text == "None" ? string.Empty : ((TagItem)cmb.SelectedItem).ID;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else ret.Value = e.Control.Text;
                _CurrentTeamUnit.Data[valuePair.Key] = ret;
            }
        }
        private void olvTeamConfig_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            olvTeamConfig.ClearObjects();
            olvTeamConfig.AddObjects(_CurrentTeamUnit.Data);
            _CurrentTeam.SetFromUnit(_CurrentTeamUnit);
            UpdateAt(lbxTeamList, _CurrentTeam, ref updatingLbxTeamList);
        }
        #endregion
        #endregion

        #region AI Trigger
        #region btn
        private void btnNewAI_Click(object sender, EventArgs e)
        {
            AITriggerItem item = map.NewAITrigger();
            AddTo(lbxAIList, item, ref updatingLbxAIList);
        }
        private void btnDelAI_Click(object sender, EventArgs e)
        {
            if (lbxAIList.SelectedItem == null) return;
            int index = lbxAIList.SelectedIndex;
            AITriggerItem item = lbxAIList.SelectedItem as AITriggerItem;
            map.RemoveAITrigger(item);
            RemoveAt(lbxAIList, index, ref updatingLbxAIList);
        }
        private void btnCopyAI_Click(object sender, EventArgs e)
        {
            if (lbxAIList.SelectedItem == null) return;
            AITriggerItem copied = _CurrentAITrigger.Copy(map.NewID);
            map.AiTriggers[copied.ID] = copied;
            AddTo(lbxAIList, copied, ref updatingLbxAIList);
        }
        #endregion
        #region lbx
        private bool updatingLbxAIList = false;
        private void lbxAIList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxAIList.SelectedItem == null) return;
            if (!updatingLbxAIList)
            {
                olvAIConfig.ClearObjects();
                _CurrentAITrigger.GetToUnit = new AITriggerUnit(_CurrentAITrigger);
                olvAIConfig.SetObjects(_CurrentAITriggerUnit.Data);
            }
        }
        #endregion
        #region olv
        private void olvAIConfig_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            KeyValuePair<string, AITriggerUnit.AITriggerShowItem> valuePair = (KeyValuePair<string, AITriggerUnit.AITriggerShowItem>)e.RowObject;
            /*if (e.Control.GetType().FullName == "BrightIdeasSoftware.BooleanCellEditor2")
            {
                ComboBox boolCbb = new ComboBox();
                boolCbb.FlatStyle = FlatStyle.Flat;
                boolCbb.DropDownStyle = ComboBoxStyle.DropDownList;
                boolCbb.Bounds = e.CellBounds;
                boolCbb.Items.Add("False");
                boolCbb.Items.Add("True");
                boolCbb.SelectedIndex = ((bool)e.Value == false) ? 0 : 1;
                e.Control = boolCbb;
            }
            else */if (e.Control.GetType().FullName == "BrightIdeasSoftware.EnumCellEditor")
            {
                ComboBox enumCbb = new ComboBox();
                enumCbb.FlatStyle = FlatStyle.Flat;
                enumCbb.DropDownStyle = ComboBoxStyle.DropDownList;
                
                Type type;
                switch (valuePair.Key)
                {
                    case "Operator":
                        type = typeof(AITriggerConditionOperator);
                        break;
                    case "Condition":
                    default:
                        type = typeof(AITriggerConditionType);
                        break;
                }
                StaticHelper.LoadToObjectCollection(enumCbb, type);
                int idxE;
                for (idxE = 0; idxE < enumCbb.Items.Count; idxE++)
                    if ((EnumDisplayClass)(enumCbb.Items[idxE]) == (int)valuePair.Value.Value)
                        break;
                try { enumCbb.SelectedIndex = idxE; } catch { }
                e.Control = enumCbb;
            }
            else if(e.Control.GetType().FullName== "BrightIdeasSoftware.FloatCellEditor")
            {
                FloatCellEditor cellEditors = (FloatCellEditor)e.Control;
                if(valuePair.Value.Value.GetType()== typeof(int))
                {
                    cellEditors.DecimalPlaces = 0;
                    cellEditors.Increment = 1;
                }
                else if (valuePair.Value.Value.GetType() == typeof(double))
                {
                    cellEditors.DecimalPlaces = 6;
                    cellEditors.Increment = 0.1M;
                    cellEditors.Minimum = 0;
                    cellEditors.Maximum = 5000;
                }
            }
            else
            {
                switch (valuePair.Key)
                {
                    case "Team1":
                    case "Team2":
                        ComboBox teamCbb = new ComboBox();
                        teamCbb.FlatStyle = FlatStyle.Flat;
                        teamCbb.DropDownStyle = ComboBoxStyle.DropDownList;
                        teamCbb.Items.Add("<none>");
                        teamCbb.Items.AddRange(map.Teams.ToArray());
                        int idxT;
                        if ((string)valuePair.Value.Value == "<none>") idxT = 0;
                        else
                        {
                            for (idxT = 1; idxT < teamCbb.Items.Count; idxT++)
                                if (((TeamItem)teamCbb.Items[idxT]).ID == (string)valuePair.Value.Value)
                                    break;
                            if (idxT == teamCbb.Items.Count) idxT = 0;
                        }
                        try { teamCbb.SelectedIndex = idxT; } catch { }
                        Utils.Misc.AdjustComboBoxDropDownWidth(ref teamCbb);
                        e.Control = teamCbb;
                        break;
                    case "Owner":
                        ComboBox houseCbb = new ComboBox();
                        houseCbb.FlatStyle = FlatStyle.Flat;
                        houseCbb.DropDownStyle = ComboBoxStyle.DropDownList;
                        houseCbb.Items.Add("<all>");
                        houseCbb.Items.AddRange(map.Countries.ToArray());
                        if ((string)valuePair.Value.Value == "<all>") houseCbb.SelectedIndex = 0;
                        else
                        {
                            CountryItem country = map.Countries.GetCountry((string)valuePair.Value.Value);
                            if (country == null) houseCbb.SelectedIndex = 0;
                            else houseCbb.SelectedItem = country;
                        }
                        Utils.Misc.AdjustComboBoxDropDownWidth(ref houseCbb);
                        e.Control = houseCbb;
                        break;
                    case "SideIndex":
                        ComboBox sideCbb = new ComboBox();
                        sideCbb.FlatStyle = FlatStyle.Flat;
                        sideCbb.DropDownStyle = ComboBoxStyle.DropDownList;
                        sideCbb.Items.Add("0 All");
                        INIEntity iNIPairs = GlobalVar.GlobalConfig["SidesList"];
                        foreach(INIPair pair in iNIPairs)
                            sideCbb.Items.Add(pair.Name + " " + pair.Value);
                        int idxS;
                        for (idxS = 0; idxS < sideCbb.Items.Count; idxS++)
                            if (idxS == (int)valuePair.Value.Value)
                                break;
                        try { sideCbb.SelectedIndex = idxS; } catch { }
                        Utils.Misc.AdjustComboBoxDropDownWidth(ref sideCbb);
                        e.Control = sideCbb;
                        break;
                    case "CondObj":
                        ComboBox objCbb = new ComboBox();
                        objCbb.FlatStyle = FlatStyle.Flat;
                        objCbb.DropDownStyle = ComboBoxStyle.DropDown;
                        List<TechnoPair> technoPairs = new List<TechnoPair>();
                        technoPairs.AddRange(GlobalVar.GlobalRules.BuildingList);
                        technoPairs.AddRange(GlobalVar.GlobalRules.InfantryList);
                        technoPairs.AddRange(GlobalVar.GlobalRules.AircraftList);
                        technoPairs.AddRange(GlobalVar.GlobalRules.VehicleList);
                        foreach (TechnoPair techno in technoPairs)
                            techno.ResetAbst(TechnoPair.AbstractType.CsfName, TechnoPair.IndexType.RegName);
                        technoPairs.Add(TechnoPair.NonePair);
                        LoadToObjectCollection(objCbb, technoPairs.AsEnumerable());
                        objCbb.SelectedIndex = technoPairs.FindIndex(p => p.RegName == (string)valuePair.Value.Value);
                        if (objCbb.SelectedItem == null && objCbb.Items.Count > 0) objCbb.SelectedIndex = technoPairs.Count - 1;
                        e.Control = objCbb;
                        break;
                    default:
                        break;
                }
            }
            e.Control.Bounds = e.CellBounds;
        }
        private void olvAIConfig_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if (!e.Cancel)
            {
                KeyValuePair<string, AITriggerUnit.AITriggerShowItem> valuePair = (KeyValuePair<string, AITriggerUnit.AITriggerShowItem>)e.RowObject;
                AITriggerUnit.AITriggerShowItem ret = new AITriggerUnit.AITriggerShowItem(valuePair.Value.ShowName, valuePair.Value.Value);
                switch (valuePair.Key)
                {
                    case "Enabled":
                    case "Skirmish":
                    case "Easy":
                    case "Normal":
                    case "Hard":
                        ret.Value = ((CheckBox)e.Control).Checked;
                        break;
                    case "Operator":
                        ret.Value = (AITriggerConditionOperator)((EnumDisplayClass)((ComboBox)e.Control).SelectedItem).Value;
                        break;
                    case "Condition":
                        ret.Value = (AITriggerConditionType)((EnumDisplayClass)((ComboBox)e.Control).SelectedItem).Value;
                        break;
                    case "OperNum":
                    case "TechLevel":
                    case "SideIndex":
                        try { ret.Value = int.Parse(e.Control.Text); }
                        catch { ret.Value = 0; }
                        break;
                    case "StartingWeight":
                    case "MinimumWeight":
                    case "MaximumWeight":
                        try { ret.Value = double.Parse(e.Control.Text); }
                        catch { ret.Value = 0D; }
                        break;
                    case "CondObj":
                        ret.Value = ((TechnoPair)((ComboBox)e.Control).SelectedItem).RegName;
                        break;
                    case "Name":
                    case "Team1":
                    case "Team2":
                    case "Owner":
                    default:
                        ret.Value = e.Control.Text;
                        break;
                }
                _CurrentAITriggerUnit.Data[valuePair.Key] = ret;
            }
        }
        private void olvAIConfig_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            olvAIConfig.ClearObjects();
            olvAIConfig.AddObjects(_CurrentAITriggerUnit.Data);
            _CurrentAITrigger.SetFromUnit(_CurrentAITriggerUnit);
            UpdateAt(lbxAIList, _CurrentAITrigger, ref updatingLbxTeamList);
        }
        #endregion
        #endregion

        #region Misc Page
        #region Local Variables

        #region Button
        private void btnNewLocalVar_Click(object sender, EventArgs e)
        {
            LocalVarItem localVar = new LocalVarItem("New Local Variable", false, localVarList.Count);
            localVarList.Add(localVar);
            localVarSource.DataSource = null;
            localVarSource.DataSource = localVarList; 
            chklbxLocalVar.SelectedIndex = chklbxLocalVar.Items.Count - 1;
            for (int idx = 0, count = localVarList.Count; idx < count; ++idx)
                chklbxLocalVar.SetItemChecked(idx, localVarList[idx].InitState);
        }
        #endregion

        private void chklbxLocalVar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (localVarList == null) return;
            txbLocalName.Text = localVarList[chklbxLocalVar.SelectedIndex].Name;
            return;
        }

        private void txbLocalName_TextChanged(object sender, EventArgs e)
        {
            localVarList[chklbxLocalVar.SelectedIndex].Name = txbLocalName.Text;
            return;
        }

        private void chklbxLocalVar_Leave(object sender, EventArgs e)
        {
            for (int idx = 0, count = localVarList.Count; idx < count; ++idx)
                localVarList[idx].InitState = chklbxLocalVar.GetItemChecked(idx);
            map.LocalVariables.UpdateData(localVarList);
            ;
        }
        #endregion

        #region Houses
        #region lbx
        private bool updatingLbxHousesList = false;
        private void lbxHouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxHouses.SelectedItem == null) return;
            if (!updatingLbxHousesList)
            {
                olvHouse.ClearObjects();
                olvHouse.SetObjects(_CurrentHouseUnit.Data);
                UpdateHouseAlliance();
            }
        }
        private void lbxHouseAllie_Enter(object sender, EventArgs e)
        {
            lbxHouseEnemy.SelectedIndices.Clear();
        }
        private void lbxHouseEnemy_Enter(object sender, EventArgs e)
        {
            lbxHouseAllie.SelectedIndices.Clear();
        }
        #endregion
        #region olv
        private void olvHouse_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            KeyValuePair<string, HouseUnit.HouseShowUnit> keyValuePair = (KeyValuePair<string, HouseUnit.HouseShowUnit>)e.RowObject;
            switch (keyValuePair.Key)
            {
                case "IQ":
                    ComboBox iqCbb = new ComboBox();
                    iqCbb.FlatStyle = FlatStyle.Flat;
                    iqCbb.DropDownStyle = ComboBoxStyle.DropDownList;
                    iqCbb.Items.Add("0");
                    iqCbb.Items.Add("1");
                    iqCbb.Items.Add("2");
                    iqCbb.Items.Add("3");
                    iqCbb.Items.Add("4");
                    iqCbb.Items.Add("5");
                    if ((int)keyValuePair.Value.Value > 5 || (int)keyValuePair.Value.Value < 0) iqCbb.SelectedIndex = 0;
                    else iqCbb.SelectedIndex = (int)keyValuePair.Value.Value;
                    Utils.Misc.AdjustComboBoxDropDownWidth(ref iqCbb);
                    e.Control = iqCbb;
                    break;
                case "PlayerControl":
                    ComboBox boolCombobox = new ComboBox();
                    boolCombobox.FlatStyle = FlatStyle.Flat;
                    boolCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
                    boolCombobox.Items.Add("False");
                    boolCombobox.Items.Add("True");
                    boolCombobox.SelectedIndex = (bool)keyValuePair.Value.Value ? 1 : 0;
                    Utils.Misc.AdjustComboBoxDropDownWidth(ref boolCombobox);
                    e.Control = boolCombobox;
                    break;
                case "Edge":
                    ComboBox enumCbb = new ComboBox();
                    enumCbb.FlatStyle = FlatStyle.Flat;
                    enumCbb.DropDownStyle = ComboBoxStyle.DropDownList;
                    StaticHelper.LoadToObjectCollection(enumCbb, typeof(HouseEdges));
                    e.Control = enumCbb;
                    break;
                case "Country":
                    ComboBox countryCbb = new ComboBox();
                    countryCbb.FlatStyle = FlatStyle.Flat;
                    countryCbb.DropDownStyle = ComboBoxStyle.DropDownList;
                    LoadToObjectCollection(countryCbb, map.Countries);
                    CountryItem countryItem = map.Countries.GetCountry((string)keyValuePair.Value.Value);
                    countryCbb.SelectedItem = countryItem;
                    Utils.Misc.AdjustComboBoxDropDownWidth(ref countryCbb);
                    e.Control = countryCbb;
                    break;
                case "ColorName":
                    List<string> colorList = new List<string>();
                    var iniPairs= GlobalVar.GlobalRules["Colors"].ToList();
                    foreach (INIPair pair in iniPairs) colorList.Add(pair.Name);
                    ComboBox colorCbb = new ComboBox();
                    colorCbb.FlatStyle = FlatStyle.Flat;
                    colorCbb.DropDownStyle = ComboBoxStyle.DropDownList;
                    colorCbb.Items.AddRange(colorList.ToArray());
                    if (colorList.Exists(s => s == (string)keyValuePair.Value.Value))
                        colorCbb.SelectedItem = (string)keyValuePair.Value.Value;
                    Utils.Misc.AdjustComboBoxDropDownWidth(ref colorCbb);
                    e.Control = colorCbb;
                    break;
                case "NodeCounts":
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
            e.Control.Bounds = e.CellBounds;
        }

        private void olvHouse_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if (!e.Cancel)
            {
                string value = e.Control.Text;
                KeyValuePair<string, HouseUnit.HouseShowUnit> keyValuePair = (KeyValuePair<string, HouseUnit.HouseShowUnit>)e.RowObject;
                HouseUnit.HouseShowUnit ret = new HouseUnit.HouseShowUnit(keyValuePair.Value.ShowName, keyValuePair.Value.Value);
                switch (keyValuePair.Key)
                {
                    case "IQ":
                        ret.Value = ((ComboBox)e.Control).SelectedIndex;
                        _CurrentHouseUnit.Data["IQ"] = ret;
                        break;
                    case "PlayerControl":
                        ret.Value = ((ComboBox)e.Control).SelectedIndex == 0 ? false : true;
                        _CurrentHouseUnit.Data["PlayerControl"] = ret;
                        break;
                    case "Country":
                        ret.Value = string.IsNullOrEmpty(e.Control.Text) ? keyValuePair.Value.Value : ret.Value = e.Control.Text;
                        _CurrentHouseUnit.Data["Country"] = ret;
                        break;
                    case "ColorName":
                        ret.Value = string.IsNullOrEmpty(e.Control.Text) ? keyValuePair.Value.Value : ret.Value = e.Control.Text;
                        _CurrentHouseUnit.Data["ColorName"] = ret;
                        break;
                    case "TechLevel":
                        try { ret.Value = int.Parse(e.Control.Text); }
                        catch { ret.Value = 10; }
                        _CurrentHouseUnit.Data["TechLevel"] = ret;
                        break;
                    case "Credits":
                        try { ret.Value = int.Parse(e.Control.Text); }
                        catch { ret.Value = 0; }
                        if ((int)ret.Value < 0) ret.Value = 0;
                        _CurrentHouseUnit.Data["Credits"] = ret;
                        break;
                    case "PercentBuilt":
                        try { ret.Value = double.Parse(e.Control.Text); }
                        catch { ret.Value = 100d; }
                        if ((double)ret.Value < 0) ret.Value = 100d;
                        _CurrentHouseUnit.Data["PercentBuilt"] = ret;
                        break;
                    case "Edge":
                        ret.Value = (HouseEdges)(((ComboBox)e.Control).SelectedIndex + 1);
                        _CurrentHouseUnit.Data["Edge"] = ret;
                        break;
                    default:
                        break;
                }
            }
        }

        private void olvHouse_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            olvHouse.ClearObjects();
            olvHouse.AddObjects(_CurrentHouseUnit.Data);
            _CurrentHouse.SetFromUnit(_CurrentHouseUnit);
            UpdateAt(lbxHouses, _CurrentHouse, ref updatingLbxHousesList);
        }
        #endregion
        #region btn
        private void btnGoEnemy_Click(object sender, EventArgs e)
        {
            foreach(string house in lbxHouseAllie.SelectedItems)
                _CurrentHouse.AlliesWith.Remove(house);
            UpdateHouseAlliance();
        }

        private void btnGoAllie_Click(object sender, EventArgs e)
        {
            foreach (string house in lbxHouseEnemy.SelectedItems)
                _CurrentHouse.AlliesWith.Add(house);
            UpdateHouseAlliance();
        }
        private void btnDelHouse_Click(object sender, EventArgs e)
        {
            if (_CurrentHouse == null) return;
            if (MessageBox.Show(DICT["LGCbtnHouseDelMsgboxMain"], DICT["LGCbtnHouseDelMsgboxTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                map.Countries.Remove(map.Countries.GetCountry(_CurrentHouse.Country).Index);
                map.Houses.Remove(_CurrentHouse.Index);
                int idx = lbxHouses.SelectedIndex;
                RemoveAt(lbxHouses, idx, ref updatingLbxHousesList);
            }
                
        }

        public static HouseItem retHouse = new HouseItem();
        private void btnNewHouse_Click(object sender, EventArgs e)
        {
            dlgNewHouse dlgNew = new dlgNewHouse();
            if (dlgNew.ShowDialog() == DialogResult.OK)
                AddTo(lbxHouses, retHouse, ref updatingLbxHousesList);
        }
        #endregion
        #region txb
        private void txbHouseAllies_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _CurrentHouse.AlliesWith.Clear();
                string[] allies = txbHouseAllies.Text.Split(',');
                foreach (string house in allies)
                {
                    bool houseExist = false;
                    foreach (HouseItem item in map.Houses)
                        if (item.Name == house)
                        {
                            houseExist = true;
                            break;
                        }
                    if (houseExist) _CurrentHouse.AlliesWith.Add(house);
                }
            }
            catch
            {
                e.Cancel = true;
            }
        }

        private void txbHouseAllies_Validated(object sender, EventArgs e)
        {
            UpdateHouseAlliance();
        }
        #endregion
        #endregion
        #endregion

        #region Form
        private void LogicEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
        private void LogicEditor_ResizeBegin(object sender, EventArgs e)
        {
            this.BeginUpdate();
        }
        private void LogicEditor_ResizeEnd(object sender, EventArgs e)
        {
            this.EndUpdate();
        }

        
        #endregion
    }
}
