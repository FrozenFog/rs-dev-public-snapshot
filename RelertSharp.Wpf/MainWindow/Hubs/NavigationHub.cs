using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Wpf.Common;
using RelertSharp.Common;
using RelertSharp.Engine;
using RelertSharp.Wpf.Views;
using RelertSharp.Common.Config.Model;
using System.Windows;

namespace RelertSharp.Wpf
{
    internal static class NavigationHub
    {
        #region Main Distribution
        public static void AutoTrace(object src)
        {
            if (src is IMapObject obj)
            {
                GoToPosition(obj, obj.GetHeight());
            }
            else if (src is ILogicItem logic)
            {
                IIndexableItem item = logic as IIndexableItem;
                switch (logic.ItemType)
                {
                    case LogicType.Trigger:
                        GoToTrigger(item);
                        break;
                    case LogicType.Team:
                        GoToTeam(item);
                        break;
                    case LogicType.Script:
                        GoToScript(item);
                        break;
                    case LogicType.Taskforce:
                        GoToTaskforce(item);
                        break;
                }
            }
        }
        public static void HandleTrace(TriggerInfoTraceType type, string value, IIndexableItem target)
        {
            switch (type)
            {
                case TriggerInfoTraceType.TriggerRegTrace:
                    GoToTrigger(target);
                    break;
                case TriggerInfoTraceType.TeamRegTrace:
                    GoToTeam(target);
                    break;
                case TriggerInfoTraceType.I2dWaypointTrace:
                    IMapObject wp = GlobalVar.GlobalMap.Waypoints.FindByID(value);
                    if (wp != null)
                    {
                        GoToPosition(wp, wp.GetHeight());
                    }
                    break;
                case TriggerInfoTraceType.AnimIdxTrace:
                    PlayAnimation(target.Name);
                    break;
                case TriggerInfoTraceType.EvaIdxTrace:
                    PlaySound(target.Name, SoundType.Eva);
                    break;
                case TriggerInfoTraceType.EvaRegTrace:
                    PlaySound(target.Id, SoundType.Eva);
                    break;
                case TriggerInfoTraceType.ThemeIdxTrace:
                    PlaySound(target.Name, SoundType.Theme);
                    break;
                case TriggerInfoTraceType.ThemeRegTrace:
                    PlaySound(target.Id, SoundType.Theme);
                    break;
                case TriggerInfoTraceType.SoundIdxTrace:
                    PlaySound(target.Name, SoundType.SoundBankRnd);
                    break;
                case TriggerInfoTraceType.SoundRegTrace:
                    PlaySound(target.Id, SoundType.SoundBankRnd);
                    break;
                case TriggerInfoTraceType.I2dBase128Trace:
                    string[] tmp = value.Split(',');
                    if (tmp.Length == 2)
                    {
                        int.TryParse(tmp[0], out int x);
                        int.TryParse(tmp[1], out int y);
                        I2dLocateable pos = new Pnt(x, y);
                        if (pos.X != 0 && pos.Y != 0)
                        {
                            GoToPosition(pos, GlobalVar.GlobalMap.GetHeightFromTile(pos));
                        }
                    }
                    break;
            }
        }
        #endregion
        #region Logic navigation
        public static event IndexableHandler GoToTriggerRequest;
        private static IListContainer trgList;
        public static void GoToTrigger(IIndexableItem trigger)
        {
            AddHistory(trgList);
            LayoutManagerHub.SwitchToActivateContent(trgList);
            GoToTriggerRequest?.Invoke(trigger);
        }
        public static void BindTriggerList(IListContainer target)
        {
            trgList = target;
        }

        public static event IndexableHandler GoToTeamRequest;
        private static IListContainer teamList;
        public static void GoToTeam(IIndexableItem team)
        {
            AddHistory(teamList);
            LayoutManagerHub.SwitchToActivateContent(teamList);
            GoToTeamRequest?.Invoke(team);
        }
        public static void BindTeamList(IListContainer target)
        {
            teamList = target;
        }

        public static event IndexableHandler GoToScriptRequest;
        private static IListContainer scriptList;
        public static void GoToScript(IIndexableItem script)
        {
            AddHistory(scriptList);
            LayoutManagerHub.SwitchToActivateContent(scriptList);
            GoToScriptRequest?.Invoke(script);
        }
        public static void BindScriptList(IListContainer target)
        {
            scriptList = target;
        }

        public static event IndexableHandler GoToTaskforceRequest;
        private static IListContainer tfList;
        public static void GoToTaskforce(IIndexableItem taskforce)
        {
            AddHistory(tfList);
            LayoutManagerHub.SwitchToActivateContent(tfList);
            GoToTaskforceRequest?.Invoke(taskforce);
        }
        public static void BindTaskforceList(IListContainer target)
        {
            tfList = target;
        }

        public static event I3dLocateableHandler GoToPositionRequest;
        public static void GoToPosition(I3dLocateable pos)
        {
            GoToPositionRequest?.Invoke(pos);
        }
        public static void GoToPosition(I2dLocateable pos, int height)
        {
            I3dLocateable dest = new Pnt3(pos, height);
            GoToPositionRequest?.Invoke(dest);
        }
        #endregion


        #region External function
        public static event RegnameHandler PlayAnimationRequest;
        public static void PlayAnimation(string regname)
        {
            PlayAnimationRequest?.Invoke(regname);
        }
        public static event SoundPlayingHandler PlaySoundRequest;
        public static void PlaySound(string soundReg, SoundType type)
        {
            PlaySoundRequest?.Invoke(soundReg, type);
        }
        #endregion


        #region BackTrace
        private static readonly LinkedList<NavigateNode> navigateHistory = new LinkedList<NavigateNode>();
        private static readonly Stack<NavigateNode> navigateRedo = new Stack<NavigateNode>();
        private static void AddHistory(IListContainer sender)
        {
            IIndexableItem former = sender.GetSelectedItem();
            if (former != null)
            {
                navigateRedo.Clear();
                if (navigateHistory.Count > 50) navigateHistory.RemoveFirst();
                NavigateNode node = new NavigateNode()
                {
                    Invoker = sender,
                    Value = former
                };
                navigateHistory.AddLast(node);
            }
        }
        public static void BackTrace()
        {
            if (navigateHistory.Count > 0)
            {
                NavigateNode node = navigateHistory.Last.Value;
                navigateHistory.RemoveLast();
                node.Invoker.SelectItem(node.Value);
                navigateRedo.Push(node);
            }
        }
        public static void RedoTrace()
        {
            if (navigateRedo.Count > 0)
            {
                NavigateNode node = navigateRedo.Pop();
                node.Invoker.SelectItem(node.Value);
                navigateHistory.AddLast(node);
            }
        }

        private class NavigateNode
        {
            public NavigateNode()
            {

            }
            public IIndexableItem Value { get; set; }
            public IListContainer Invoker { get; set; }
        }
        #endregion
    }


    internal class TraceHelper
    {
        public TraceHelper()
        {

        }

        public FrameworkElement SourceControl { get; set; }
        public LogicInfoParameter Param { get; set; }
    }
}
