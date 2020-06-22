using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Logic;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.Controls
{
    public partial class PickPanel
    {
        private bool initialized = false;
        private const string TNodeBuilding = "\nBuilding";
        private const string TNodeInfantry = "\nInfantry";
        private const string TNodeUnit = "\nUnit";
        private const string TNodeNaval = "\nNaval";
        private const string TNodeAircraft = "\nAircraft";
        private readonly string[] ObjectRootNodes = { TNodeBuilding, TNodeInfantry, TNodeUnit, TNodeNaval, TNodeAircraft};
        private IMapObject InnerSource;


        private void InitializeGeneralPanel()
        {
            LoadHeadImages();
            LoadGeneralObjects();
            InitializeAttributePanel();
        }
        private void InitializeAttributePanel()
        {
            LoadToObjectCollection(cbbOwner, Map.Houses);
            if (cbbOwner.Items.Count > 0) cbbOwner.SelectedIndex = 0;
            LoadToObjectCollection(cbbAttTag, TagItem.NullTag, Map.Tags);
            cbbSpotlight.Items.Add(BuildingSpotlightType.None);
            cbbSpotlight.Items.Add(BuildingSpotlightType.Arcing);
            cbbSpotlight.Items.Add(BuildingSpotlightType.Circular);
            cbbSpotlight.DropDownWidth = 55;
            cbbSpotlight.SelectedIndex = 0;
            cbbAttTag.Text = "None";
            cbbAttTag.SelectedText = string.Empty;
        }

        private void LoadGeneralObjects()
        {
            LoadBuildings();
            TreeNode inf = new TreeNode("Infantries", 2, 2);
            inf.Name = TNodeInfantry;
            TreeNode air = new TreeNode("Aircrafts", 5, 5);
            air.Name = TNodeAircraft;
            LoadOtherCombatObjects(inf, "InfantryTypes", CombatObjectType.Infantry);
            LoadUnits();
            LoadOtherCombatObjects(air, "AircraftTypes", CombatObjectType.Aircraft);
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
            building.Name = TNodeBuilding;
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
                    int side = GlobalRules.GuessSide(p.Value, CombatObjectType.Building, true);
                    if (side >= 0) AddObjectToNode(budSides[side], p.Value);
                    else AddObjectToNode(budSides.Last(), p.Value);
                }
            }
            budSides.Insert(budSides.Count - 1, tech);
            LoadToTreeNode(building, budSides);
            trvObject.Nodes.Add(building);
        }
        private void LoadOtherCombatObjects(TreeNode dest, string rulesListName, CombatObjectType type)
        {
            List<TreeNode> sides = InitializeSideNode();
            foreach (INIPair p in GlobalRules[rulesListName])
            {
                int side = GlobalRules.GuessSide(p.Value, type);
                if (side >= 0) AddObjectToNode(sides[side], p.Value);
                else AddObjectToNode(sides.Last(), p.Value);
            }
            LoadToTreeNode(dest, sides);
            trvObject.Nodes.Add(dest);
        }
        private void LoadUnits()
        {
            TreeNode unit = new TreeNode("Units", 3, 3);
            unit.Name = TNodeUnit;
            TreeNode naval = new TreeNode("Navals", 4, 4);
            naval.Name = TNodeNaval;
            List<TreeNode> navals = InitializeSideNode();
            List<TreeNode> units = InitializeSideNode();
            foreach (INIPair p in GlobalRules["VehicleTypes"])
            {
                if (GlobalRules[p.Value]["MovementZone"] == "Water")
                {
                    int side = GlobalRules.GuessSide(p.Value, CombatObjectType.Naval);
                    if (side >= 0) AddObjectToNode(navals[side], p.Value);
                    else AddObjectToNode(navals.Last(), p.Value);
                }
                else
                {
                    int side = GlobalRules.GuessSide(p.Value, CombatObjectType.Vehicle);
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
        private void AddObjectToNode(TreeNode dest, string regname, bool containsUiName = true)
        {
            dest.Nodes.Add(regname, GlobalRules.FormatTreeNodeName(regname, containsUiName));
        }
        private List<TreeNode> InitializeSideNode()
        {
            int num = GlobalRules.GetSideCount();
            List<TreeNode> nodes = new List<TreeNode>();
            for (int i = 0; i < num; i++)
            {
                nodes.Add(new TreeNode(Language.DICT[GlobalRules.GetSideName(i)]));
            }
            nodes.Add(new TreeNode(Language.DICT["NoSideMisc"]));
            return nodes;
        }

        private void trvObject_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (drew)
            {
                TreeNode node = e.Node;
                if (!string.IsNullOrEmpty(node.Name) && !ObjectRootNodes.Contains(node.Name))
                {
                    TreeNode root = GetRootNode(node);
                    combatRootName = root.Name;
                    initialized = false;
                    IEnumerable<TechnoPair> upgrades = GlobalRules.GetBuildingUpgradeList(node.Name);
                    LoadToObjectCollection(cbbUpg1, new TechnoPair("None", ""), upgrades);
                    LoadToObjectCollection(cbbUpg2, new TechnoPair("None", ""), upgrades);
                    LoadToObjectCollection(cbbUpg3, new TechnoPair("None", ""), upgrades);
                    if (upgrades == null)
                    {
                        cbbUpg1.SelectedIndex = 0;
                        cbbUpg2.SelectedIndex = 0;
                        cbbUpg3.SelectedIndex = 0;
                        mtxbUpgNum.Text = "0";
                    }
                    initialized = true;
                    currentCombatObjectRegName = node.Name;
                    ReloadCombatObject();
                }
                BrushObjectSelected?.Invoke(this, new EventArgs());
            }
        }
        private string currentCombatObjectRegName = "";
        private string combatRootName = "";
        private void ReloadCombatObject()
        {
            if (!string.IsNullOrEmpty(currentCombatObjectRegName))
            {
                switch (combatRootName)
                {
                    case TNodeAircraft:
                        CreateInnerAircraft(currentCombatObjectRegName);
                        brush.Reload(currentCombatObjectRegName, MapObjectType.Aircraft, InnerSource as ICombatObject);
                        break;
                    case TNodeUnit:
                    case TNodeNaval:
                        CreateInnerUnit(currentCombatObjectRegName);
                        brush.Reload(currentCombatObjectRegName, MapObjectType.Vehicle, InnerSource as ICombatObject);
                        break;
                    case TNodeInfantry:
                        CreateInnerInfantry(currentCombatObjectRegName);
                        brush.Reload(currentCombatObjectRegName, MapObjectType.Infantry, InnerSource as ICombatObject);
                        break;
                    case TNodeBuilding:
                        CreateInnerBuilding(currentCombatObjectRegName);
                        brush.Reload(currentCombatObjectRegName, MapObjectType.Building, InnerSource as ICombatObject);
                        break;
                }
            }
        }
        private void CreateInnerAircraft(string regname)
        {
            InnerSource = new AircraftItem(regname)
            {
                AutoNORecruitType = ckbRecruit.Checked,
                AutoYESRecruitType = ckbUnitRebuild.Checked
            };
            ApplyGeneralAttribute();
        }
        private void CreateInnerBuilding(string regname)
        {
            TechnoPair p1 = cbbUpg1.SelectedItem as TechnoPair;
            TechnoPair p2 = cbbUpg2.SelectedItem as TechnoPair;
            TechnoPair p3 = cbbUpg3.SelectedItem as TechnoPair;
            InnerSource = new StructureItem(regname)
            {
                AIRepairable = ckbRepair.Checked,
                AISellable = ckbSell.Checked,
                BuildingOnline = ckbPowered.Checked,
                SpotlightType = (BuildingSpotlightType)cbbSpotlight.SelectedIndex,
                UpgradeNum = TrimMaskedTextbox(mtxbUpgNum, 0, 3),
                Upgrade1 = p1 != null ? p1.Index : "None",
                Upgrade2 = p2 != null ? p2.Index : "None",
                Upgrade3 = p3 != null ? p3.Index : "None",
            };
            ApplyGeneralAttribute();
        }
        private void CreateInnerUnit(string regname)
        {
            InnerSource = new UnitItem(regname)
            {
                FollowsIndex = txbFollow.Text,
                IsAboveGround = ckbBridge.Checked,
                AutoYESRecruitType = ckbUnitRebuild.Checked,
                AutoNORecruitType = ckbRecruit.Checked
            };
            ApplyGeneralAttribute();
        }
        private void CreateInnerInfantry(string regname)
        {
            InnerSource = new InfantryItem(regname)
            {
                IsAboveGround = ckbBridge.Checked,
                AutoYESRecruitType = ckbUnitRebuild.Checked,
                AutoNORecruitType = ckbRecruit.Checked
            };
            ApplyGeneralAttribute();
        }
        private void ApplyGeneralAttribute()
        {
            if (InnerSource != null)
            {
                InnerSource.X = -1000;
                InnerSource.Y = -1000;
            }
            if ((InnerSource as ICombatObject) != null)
            {
                ICombatObject src = InnerSource as ICombatObject;
                HouseItem house = cbbOwner.SelectedItem as HouseItem;
                TagItem tag = cbbAttTag.SelectedItem as TagItem;
                src.OwnerHouse = house != null ? house.Name : CurrentMapDocument.Map.Houses.First().Name;
                src.HealthPoint = TrimMaskedTextbox(mtxbHp, 1, 256);
                src.VeterancyPercentage = TrimMaskedTextbox(mtxbVeteran, 0, 200);
                src.Rotation = TrimMaskedTextbox(mtxbFacing, 0, 256);
                src.TaggedTrigger = tag != null ? tag.ID : "None";
                src.Status = cbbStat.Text;
                src.Group = TryParseTextBox(txbGroup, -1);
            }
        }
        private bool mtxbUpgLocker = false;
        private void mtxbUpgNum_TextChanged(object sender, EventArgs e)
        {
            if (initialized && !mtxbUpgLocker)
            {
                mtxbUpgLocker = true;
                int num = TrimMaskedTextbox(mtxbUpgNum, 0, 3);
                mtxbUpgNum.Text = num.ToString();
                mtxbUpgLocker = false;
            }
        }
        private void ParamChangedOnRedraw(object sender, EventArgs e)
        {
            if (initialized)
            {
                ReloadCombatObject();
            }
        }

    }
}
