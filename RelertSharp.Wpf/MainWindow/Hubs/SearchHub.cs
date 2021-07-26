using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf
{
    internal static class SearchHub
    {
        public static event EventHandler SearchClearRequested;
        public static event EnumerableObjectHandler SearchResultPushed;
        public static event EnumerableObjectHandler SelectionPushed;
        public static event EventHandler SelectionClearRequested;


        #region Api
        public static void PushSearchResult(IEnumerable<object> src, bool clearAllPreviousResult = true)
        {
            if (clearAllPreviousResult) SearchClearRequested?.Invoke(null, null);
            SearchResultPushed?.Invoke(src);
        }
        public static void ClearResult()
        {
            SearchClearRequested?.Invoke(null, null);
        }
        public static void PushSelection(IEnumerable<IMapObject> src, bool clearAllSelection = true)
        {
            if (clearAllSelection) SelectionClearRequested?.Invoke(null, null);
            SelectionPushed?.Invoke(src);
        }
        #endregion
    }
}
