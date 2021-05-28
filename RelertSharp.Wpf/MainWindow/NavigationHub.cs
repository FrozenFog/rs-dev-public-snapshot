using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Wpf.Common;
using RelertSharp.Common;
using RelertSharp.Engine;
using RelertSharp.Wpf.Views;

namespace RelertSharp.Wpf
{
    internal static class NavigationHub
    {
        public static event IndexableHandler GoToTriggerRequest;
        private static IListContainer trgList;
        public static void GoToTrigger(IIndexableItem trigger)
        {
            AddHistory(trgList);
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
}
