using RelertSharp.Algorithm;
using RelertSharp.Common;
using RelertSharp.Engine.Api;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RelertSharp.Wpf.MapEngine.Helper
{
    internal static class Selector
    {
        #region Private call
        private static int MapWidth { get { return GlobalVar.CurrentMapDocument.Map.Info.Size.Width; } }
        private static Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }
        #endregion
        private static Point selectDown, selectNew;
        private static int subCell;
        private static bool dragingSelectBox;
        private static bool isIsometric;

        private static Canvas src;
        private static SolidColorBrush b = new SolidColorBrush(Colors.White);
        private static Rectangle rect;

        // selecting
        private static List<IMapObject> selectedObjects = new List<IMapObject>();
        private static I3dLocateable beginCell, endCell;
        private static MapObjectType selectFlag = MapObjectType.AllSelectableObjects;

        // moving
        private static bool isMoving;
        private static List<I2dLocateable> orgPos = new List<I2dLocateable>();
        private static I2dLocateable posBeginMove;

        #region Api
        #region Box Object Selecting
        public static void BeginSelecting(Point downPos, bool isometric, Canvas graphic, I3dLocateable begin)
        {
            if (!dragingSelectBox)
            {
                selectDown = downPos;
                dragingSelectBox = true;
                isIsometric = isometric;
                src = graphic;
                beginCell = begin;
                endCell = begin;
                rect = new Rectangle()
                {
                    Stroke = b,
                    StrokeThickness = 1
                };
                src.Children.Add(rect);
                Canvas.SetTop(rect, downPos.Y);
                Canvas.SetLeft(rect, downPos.X);
            }
        }
        public static void EndSelecting(bool reverseSelect)
        {
            if (dragingSelectBox)
            {
                dragingSelectBox = false;
                src.Children.Clear();
                MapPosition.RegularCellToSceneSquare(beginCell, endCell, MapWidth, out I2dLocateable lt, out I2dLocateable rb);
                foreach (I2dLocateable pos in new SceneSquare2D(lt, rb, MapWidth))
                {
                    if (Map.TilesData[pos] is Tile t)
                    {
                        foreach (IMapObject obj in t.GetObjects())
                        {
                            if ((obj.ObjectType & selectFlag) != 0)
                            {
                                if (reverseSelect && selectedObjects.Contains(obj))
                                {
                                    obj.CancelSelection();
                                    selectedObjects.Remove(obj);
                                }
                                else
                                {
                                    obj.Select();
                                    selectedObjects.Add(obj);
                                }
                            }
                        }
                    }
                }
                rect = null;
            }
        }
        public static void SelectAt(I2dLocateable cell, int subcell, bool reverseSelect)
        {
            if (Map.TilesData[cell] is Tile t)
            {
                foreach (IMapObject obj in t.GetObjects())
                {
                    if ((obj.ObjectType & selectFlag) != 0)
                    {
                        if (obj is InfantryItem inf && inf.SubCell != subcell) continue;
                        if (reverseSelect && selectedObjects.Contains(obj))
                        {
                            obj.CancelSelection();
                            selectedObjects.Remove(obj);
                        }
                        else
                        {
                            obj.Select();
                            selectedObjects.Add(obj);
                        }
                    }
                }
            }
        }
        public static void UpdateSelectingRectangle(Point newPos, I3dLocateable end)
        {
            if (dragingSelectBox)
            {
                selectNew = newPos;
                endCell = end;
                Canvas.SetTop(rect, Math.Min(selectNew.Y, selectDown.Y));
                Canvas.SetLeft(rect, Math.Min(selectNew.X, selectDown.X));
                rect.Width = Math.Abs(selectNew.X - selectDown.X);
                rect.Height = Math.Abs(selectNew.Y - selectDown.Y);
            }
        }
        public static void UnselectAll()
        {
            foreach (IMapObject obj in selectedObjects) obj.CancelSelection();
            selectedObjects.Clear();
        }
        public static void AddSelectionFlag(MapObjectType typeAdd)
        {
            selectFlag |= typeAdd;
        }
        public static void RemoveSelectionFlag(MapObjectType typeRemove)
        {
            /// A B Out
            /// 0 0 0
            /// 0 1 0
            /// 1 0 1
            /// 1 1 0
            selectFlag &= ~typeRemove;
        }
        #endregion
        #region Moving and deleting
        public static void BeginSelectedObjectsMoving(I2dLocateable beginCell)
        {
            isMoving = true;
            orgPos.Clear();
            foreach (var obj in selectedObjects) orgPos.Add(new Pnt(obj));
            posBeginMove = beginCell;
        }
        public static void MoveSelectedObjectsTo(I2dLocateable destCell, int subcell = -1)
        {
            if (isMoving)
            {
                I2dLocateable delta = RsMath.I2dSubi(destCell, posBeginMove);
                for (int i = 0; i < selectedObjects.Count; i++)
                {
                    IMapObject obj = selectedObjects[i];
                    I2dLocateable objDest = RsMath.I2dAddi(orgPos[i], delta);
                    if (Map.TilesData[objDest] is Tile)
                    {
                        if (obj is InfantryItem inf) MapApi.MoveObjectTo(obj, objDest, inf.SubCell);
                        else MapApi.MoveObjectTo(obj, objDest, subcell);
                    }
                }
            }
        }
        public static void EndSelectedObjectsMoving()
        {
            isMoving = false;
        }
        public static bool IsPositionHasSelectedItem(I2dLocateable pos, int subcell = -1)
        {
            foreach (IMapObject obj in selectedObjects)
            {
                if (obj.Coord == pos.Coord)
                {
                    if (subcell != -1)
                    {
                        if (obj is InfantryItem inf) return inf.SubCell == subcell;
                        else return true;
                    }
                    else return true;
                }
            }
            return false;
        }
        public static void DeleteSelectedObjects()
        {
            foreach (IMapObject obj in selectedObjects)
            {
                MapApi.RemoveObject(obj);
            }
            selectedObjects.Clear();
        }
        #endregion
        #endregion


        public static IEnumerable<IMapObject> SelectedObjects { get { return selectedObjects; } }
        public static bool IsMoving { get { return isMoving; } }
        public static bool IsSelecting { get { return dragingSelectBox; } }
    }
}
