using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RelertSharp.Wpf.Common
{
    internal class DragDropHelper<TObject, TVm> where TObject : class
    {
        private Point prevMouseDown;
        private bool isDraging = false;
        private bool triggeredDrag = false;
        private TObject dragItem;
        private readonly DependencyObject src;
        private DelayedAction delayDrag;
        public DragDropHelper(DependencyObject sourceControl)
        {
            src = sourceControl;
            delayDrag = new DelayedAction(null, () => isDraging = true, 500);
        }



        #region Api
        public void BeginDrag(Point downPoint)
        {
            prevMouseDown = downPoint;
            isDraging = true;
            delayDrag.Restart();
        }
        public void SetDragItem(TObject obj)
        {
            dragItem = obj;
        }
        public void SetReferanceVm(TVm vm)
        {
            ReferanceVm = vm;
        }
        public void EndDrag()
        {
            delayDrag.Stop();
            isDraging = false;
            triggeredDrag = false;
        }
        public void MouseMoveDrag(Point moving)
        {
            if (isDraging && !triggeredDrag)
            {
                if (RsMath.ChebyshevDistance(moving, prevMouseDown) > MouseMoveThreshold)
                {
                    if (dragItem != null)
                    {
                        triggeredDrag = true;
                        DataObject obj = new DataObject(dragItem);
                        DragDropEffects effect = DragDrop.DoDragDrop(src, obj, DragDropEffects.Move);
                    }
                }
            }
        }
        public bool GetDragObject(DragEventArgs e, out TObject obj)
        {
            e.Effects = DragDropEffects.None;
            e.Handled = true;
            obj = null;
            if (e.Data.GetData(typeof(TObject)) is TObject data)
            {
                obj = data;
                return true;
            }
            return false;
        }
        #endregion


        #region Call
        public int MouseMoveThreshold { get; set; } = 10;
        public bool IsDraging { get { return isDraging; } }
        public TObject DragItem { get { return dragItem; } }
        public TVm ReferanceVm { get; private set; }
        #endregion
    }
}
