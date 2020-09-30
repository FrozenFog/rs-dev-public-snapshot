using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelertSharp.Common
{
    public class ListViewComparer : IComparer
    {
        private CaseInsensitiveComparer comparer;


        public ListViewComparer()
        {
            Order = SortOrder.Ascending;
            TargetCol = 0;
            comparer = new CaseInsensitiveComparer();
        }



        public int Compare(object x, object y)
        {
            int result;
            ListViewItem lvi1 = x as ListViewItem;
            ListViewItem lvi2 = y as ListViewItem;

            result = comparer.Compare(lvi1.SubItems[TargetCol].Text, lvi2.SubItems[TargetCol].Text);
            if (Order == SortOrder.Ascending) return result;
            else if (Order == SortOrder.Descending) return -result;
            return 0;
        }



        public int TargetCol { get; set; }
        public SortOrder Order { get; set; }
    }
}
