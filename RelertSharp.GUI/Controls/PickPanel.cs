using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI
{
    public partial class PickPanel : UserControl
    {
        private Dictionary<string, string> regname_pcx = new Dictionary<string, string>();


        public PickPanel()
        {
            InitializeComponent();
        }


        public void Initialize()
        {
            LoadHeadImages();
            LoadGeneralObjects();
        }


        private void LoadGeneralObjects()
        {
            LoadBuildings();
            LoadOtherCombatObjects("Infantries", "InfantryTypes", "InfantryRoot", 2);
            LoadUnits();
            LoadOtherCombatObjects("Aircrafts", "AircraftTypes", "AirRoot", 5);
        }
        private void LoadHeadImages()
        {
            imgMain.Images.Add("null", Properties.Resources.nullImg);
            imgMain.Images.Add("headBud", Properties.Resources.headBuilding);
            imgMain.Images.Add("headInf", Properties.Resources.headInfantry);
            imgMain.Images.Add("headUnit", Properties.Resources.headUnit);
            imgMain.Images.Add("headNaval", Properties.Resources.headNaval);
            imgMain.Images.Add("headAir", Properties.Resources.headAir);
        }
        private void LoadBuildings()
        {
            TreeNode building = new TreeNode("Buildings", 1, 1);
            List<TreeNode> budSides = InitializeSideNode();
            TreeNode tech = new TreeNode(Language.DICT["NoSideTech"]);
            foreach (INIPair p in GlobalRules["BuildingTypes"])
            {
                if (GlobalRules.IsTechBuilding(p.Value))
                {
                    AddObjectToNode(tech, p.Value);
                }
                else
                {
                    int side = GlobalRules.GuessSide(p.Value, "BuildingRoot", true);
                    if (side >= 0) AddObjectToNode(budSides[side], p.Value);
                    else AddObjectToNode(budSides.Last(), p.Value);
                }
            }
            budSides.Insert(budSides.Count - 1, tech);
            LoadToTreeNode(building, budSides);
            trvObject.Nodes.Add(building);
        }
        private void LoadOtherCombatObjects(string nodeName, string rulesListName, string rootIndexName, int imgIndex)
        {
            TreeNode inf = new TreeNode(nodeName, imgIndex, imgIndex);
            List<TreeNode> sides = InitializeSideNode();
            foreach (INIPair p in GlobalRules[rulesListName])
            {
                int side = GlobalRules.GuessSide(p.Value, rootIndexName);
                if (side >= 0) AddObjectToNode(sides[side], p.Value);
                else AddObjectToNode(sides.Last(), p.Value);
            }
            LoadToTreeNode(inf, sides);
            trvObject.Nodes.Add(inf);
        }
        private void LoadUnits()
        {
            TreeNode unit = new TreeNode("Units", 3, 3);
            TreeNode naval = new TreeNode("Navals", 4, 4);
            List<TreeNode> navals = InitializeSideNode();
            List<TreeNode> units = InitializeSideNode();
            foreach (INIPair p in GlobalRules["VehicleTypes"])
            {
                if (GlobalRules[p.Value]["MovementZone"] == "Water")
                {
                    int side = GlobalRules.GuessSide(p.Value, "NavalRoot");
                    if (side >= 0) AddObjectToNode(navals[side], p.Value);
                    else AddObjectToNode(navals.Last(), p.Value);
                }
                else
                {
                    int side = GlobalRules.GuessSide(p.Value, "UnitRoot");
                    if (side >= 0) AddObjectToNode(units[side], p.Value);
                    else AddObjectToNode(units.Last(), p.Value);
                }
            }
            LoadToTreeNode(unit, units);
            LoadToTreeNode(naval, navals);
            trvObject.Nodes.Add(unit);
            trvObject.Nodes.Add(naval);
        }
        private void LoadCustomTree()
        {
            Dictionary<string, string[]> customTree = GlobalConfig.Local.GetCustomObjectFolder();
        }
        private void AddObjectToNode(TreeNode dest, string regname)
        {
            dest.Nodes.Add(regname, GlobalRules.FormatTreeNodeName(regname));
        }
        private List<TreeNode> InitializeSideNode()
        {
            int num = GlobalRules.GetSideCount();
            List<TreeNode> nodes = new List<TreeNode>();
            string[] rootnameIndex = GlobalConfig["SideBeloningRoot"].GetPair("SideName").ParseStringList();
            for (int i = 0; i < num; i++)
            {
                nodes.Add(new TreeNode(Language.DICT[rootnameIndex[i]]));
            }
            nodes.Add(new TreeNode(Language.DICT["NoSideMisc"]));
            return nodes;
        }
    }
}
