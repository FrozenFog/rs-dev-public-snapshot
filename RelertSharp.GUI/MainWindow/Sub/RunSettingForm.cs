using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.GUI
{
    public partial class RunSettingForm : Form
    {
        private INIEntity setting;
        private static Dictionary<int, string> diff = new Dictionary<int, string>()
        {
            {0, "Easy"},
            {1, "Normal" },
            {2, "Hard" }
        };
        private bool IsSkirmish { get { return rdbSkirmish.Checked; } }
        public RunSettingForm()
        {
            InitializeComponent();
        }
        public void ReadSettingFromLocal()
        {
            setting = GlobalConfig.Local.RunningConfig;

            int gamemode = setting.ParseInt("Gamemode", 1);
            rdbSingle.Checked = false;
            rdbSkirmish.Checked = false;
            if (gamemode == 0) rdbSingle.Checked = true;
            else rdbSkirmish.Checked = true;
            txbExeName.Text = setting["ExeName"];
            ckbShortGame.Checked = setting.ParseBool("ShortGame");
            ckbBudAlly.Checked = setting.ParseBool("BuildOffAlly");
            ckbSuper.Checked = setting.ParseBool("Superweapon");
            ckbCrates.Checked = setting.ParseBool("Crates");
            ckbMcvRe.Checked = setting.ParseBool("McvRedeploy");
            ckbAlly.Checked = setting.ParseBool("Ally");
            mtxbMoney.Text = setting["Money"];
            mtxbUnitNum.Text = setting["UnitNum"];
            trkbGameSpeed.Value = Constant.GameRunning.MaxGameSpeed - setting.ParseInt("GameSpeed");
            trkbSingleDiff.Value = setting.ParseInt("SingleDiff");
        }
        public void SaveSettingToLocal()
        {
            int gamemode = IsSkirmish ? 1 : 0;
            setting["Gamemode"] = gamemode;
            setting["ExeName"] = txbExeName.Text;
            setting["ShortGame"] = ckbShortGame.Checked;
            setting["BuildOffAlly"] = ckbBudAlly.Checked;
            setting["Superweapon"] = ckbSuper.Checked;
            setting["Crates"] = ckbCrates.Checked;
            setting["McvRedeploy"] = ckbMcvRe.Checked;
            setting["Ally"] = ckbAlly.Checked;
            setting["Money"] = mtxbMoney.Text;
            setting["UnitNum"] = mtxbUnitNum.Text;
            setting["GameSpeed"] = Constant.GameRunning.MaxGameSpeed - trkbGameSpeed.Value;
            setting["SingleDiff"] = trkbSingleDiff.Value;

            GlobalConfig.Local.RunningConfig = setting;
        }
        public INIFile GetSpawn()
        {
            Random r = new Random();
            INIFile spawn = new INIFile("spawn.ini", true);
            INIEntity spawnsetting = new INIEntity("Settings");
            if (IsSkirmish)
            {
                spawnsetting["Name"] = "RelertSharp";
                spawnsetting["UIGameMode"] = "Relert Sharp Testing Mode";
                spawnsetting["UIMapName"] = CurrentMapDocument.Map.Info.MapName;
                spawnsetting["PlayerCount"] = 1;
                spawnsetting["IsSpectator"] = false;
                spawnsetting["Color"] = 0;
                spawnsetting["AIPlayers"] = 1;
                spawnsetting["Seed"] = Math.Abs(r.Next());
                DumpSettingToSpawn(spawnsetting);
                spawnsetting["TechLevel"] = 10;
                spawnsetting["FogOfWar"] = false;

                INIEntity handicaps = new INIEntity("HouseHandicaps");
                handicaps["Multi2"] = 2;
                INIEntity housecon = new INIEntity("HouseCountries");
                housecon["Multi2"] = 1;
                INIEntity housecolor = new INIEntity("HouseColors");
                housecolor["Multi2"] = 1;
                INIEntity locat = new INIEntity("SpawnLocations");
                locat["Multi1"] = 0;
                locat["Multi2"] = 1;
                spawn["HouseHandicaps"] = handicaps;
                spawn["HouseCountries"] = housecon;
                spawn["HouseColors"] = housecolor;
                spawn["SpawnLocations"] = locat;
            }
            else
            {
                spawnsetting["GameSpeed"] = 2;
                spawnsetting["Firestorm"] = false;
                spawnsetting["IsSinglePlayer"] = true;
                spawnsetting["SidebarHack"] = false;
                spawnsetting["BuildOffAlly"] = true;
                spawnsetting["DifficultyModeHuman"] = trkbSingleDiff.Value;
                spawnsetting["DifficultyModeComputer"] = trkbSingleDiff.Maximum - trkbSingleDiff.Value;
                INIFile ra2xx = new INIFile(GlobalConfig.GamePath + GlobalConfig.Ra2xxName + ".ini");
                ra2xx["Options"]["Difficulty"] = trkbSingleDiff.Value;
                ra2xx.SaveIni(GlobalConfig.GamePath + GlobalConfig.Ra2xxName + ".ini");
            }
            spawnsetting["Scenario"] = "spawnmap.ini";
            spawnsetting["Side"] = 0;
            spawn["Settings"] = spawnsetting;

            return spawn;
        }


        private void DumpSettingToSpawn(INIEntity dest)
        {
            dest["ShortGame"] = ckbShortGame.Checked;
            dest["Superweapons"] = ckbSuper.Checked;
            dest["MCVRedeploy"] = ckbMcvRe.Checked;
            dest["BuildOffAlly"] = ckbBudAlly.Checked;
            dest["Crates"] = ckbCrates.Checked;
            dest["AlliesAllowed"] = ckbAlly.Checked;
            dest["UnitCount"] = mtxbUnitNum.Text;
            dest["GameSpeed"] = Constant.GameRunning.MaxGameSpeed - trkbGameSpeed.Value;
            dest["Credits"] = mtxbMoney.Text;
        }





        private void trkbGameSpeed_Scroll(object sender, EventArgs e)
        {
            toolTip.SetToolTip(trkbGameSpeed, trkbGameSpeed.Value.ToString());
        }

        private void rdbSingle_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Tag.ToString() == "0")
            {
                gpbSkirmishSetting.Enabled = false;
                trkbSingleDiff.Enabled = true;
            }
            else
            {
                gpbSkirmishSetting.Enabled = true;
                trkbSingleDiff.Enabled = false;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void trkbSingleDiff_Scroll(object sender, EventArgs e)
        {
            toolTip.SetToolTip(trkbSingleDiff, diff[trkbSingleDiff.Value]);
        }


        public string GameName { get { return txbExeName.Text; } }
        public bool UseSyringe { get { return rdbSyr.Checked; } }
    }
}
