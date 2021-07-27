using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.MapEngine;
using RelertSharp.Wpf.ToolBoxes;
using RelertSharp.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf
{
    public partial class MainWindow
    {
        #region Components
        [RsViewComponent(GuiViewType.AiTrigger, GuiViewSide.Right, "Ai Trigger Info")]
        private readonly AiTriggerView aiTrigger = new AiTriggerView();
        [RsViewComponent(GuiViewType.TeamList, GuiViewSide.Left, "Team List")]
        private readonly TeamListView teamList = new TeamListView();
        [RsViewComponent(GuiViewType.AiTriggerList, GuiViewSide.Left, "Ai Trigger List")]
        private readonly AiTriggerListView aiTriggerList = new AiTriggerListView();
        [RsViewComponent(GuiViewType.ScriptList, GuiViewSide.Left, "Script List")]
        private readonly ScriptListView scriptList = new ScriptListView();
        [RsViewComponent(GuiViewType.TaskforceList, GuiViewSide.Left, "Taskforce List")]
        private readonly TaskforceListView taskforceList = new TaskforceListView();
        [RsViewComponent(GuiViewType.Taskforce, GuiViewSide.Right, "Taskforce Info")]
        private readonly TaskforceView taskforce = new TaskforceView();
        [RsViewComponent(GuiViewType.Team, GuiViewSide.Right, "Team Info")]
        private readonly TeamView team = new TeamView();
        [RsViewComponent(GuiViewType.Script, GuiViewSide.Right, "Script Info")]
        private readonly ScriptView script = new ScriptView();
        [RsViewComponent(GuiViewType.MainPanel, GuiViewSide.Center, "Map")]
        private readonly MainPanel pnlMain = new MainPanel();
        [RsViewComponent(GuiViewType.Minimap, GuiViewSide.Top, "Minimap")]
        private readonly MinimapPanel minimap = new MinimapPanel();
        [RsViewComponent(GuiViewType.LightningPanel, GuiViewSide.Right, "Lightning")]
        private readonly LightningView lightning = new LightningView();
        [RsViewComponent(GuiViewType.AnimationPreview, GuiViewSide.Right, "Animation Preview")]
        private readonly AnimationPreview animationPreview = new AnimationPreview();
        [RsViewComponent(GuiViewType.TriggerList, GuiViewSide.Left, "Trigger List")]
        private readonly TriggerListView triggerList = new TriggerListView();
        [RsViewComponent(GuiViewType.Trigger, GuiViewSide.Top, "Trigger Info")]
        private readonly TriggerView trigger = new TriggerView();
        [RsViewComponent(GuiViewType.Event, GuiViewSide.Bottom, "Events")]
        private readonly TriggerLogicView events = new TriggerLogicView(true);
        [RsViewComponent(GuiViewType.Action, GuiViewSide.Bottom, "Actions")]
        private readonly TriggerLogicView actions = new TriggerLogicView(false);
        [RsViewComponent(GuiViewType.ObjctPanel, GuiViewSide.Left, "Object Brush")]
        private readonly MapObjectBrushView objectBrush = new MapObjectBrushView();
        [RsViewComponent(GuiViewType.HousePanel, GuiViewSide.Bottom, "House Info")]
        private readonly CountryHouseView housePanel = new CountryHouseView();
        [RsViewComponent(GuiViewType.TilePanel, GuiViewSide.Bottom, "Tiles")]
        private readonly TilePanelView tiles = new TilePanelView();
        [RsViewComponent(GuiViewType.LocalVarPanel, GuiViewSide.Right, "Local Var")]
        private readonly LocalVarView localVar = new LocalVarView();
        [RsViewComponent(GuiViewType.GlobalSearch, GuiViewSide.Center, "Global Search")]
        private readonly GlobalSearchView globalSearch = new GlobalSearchView();
        [RsViewComponent(GuiViewType.InspectorPanel, GuiViewSide.Right, "Inspector")]
        private readonly SelectedItemView inspector = new SelectedItemView();
        [RsViewComponent(GuiViewType.LayerControl, GuiViewSide.Right, "Layer Control")]
        private readonly LayerView layerControl = new LayerView();
        #endregion
    }
}
