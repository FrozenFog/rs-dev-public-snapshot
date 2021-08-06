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
        public static event Action SelectionChanged;
        #region Private call
        private static int MapWidth { get { return GlobalVar.GlobalMap.Info.Size.Width; } }
        private static Map Map { get { return GlobalVar.GlobalMap; } }
        #endregion
        private static Point selectDown, selectNew;
        private static int subCell;
        private static bool dragingSelectBox;
        private static bool isIsometric;
        private static int prevCount = -1;

        private static Canvas src;
        private static SolidColorBrush b = new SolidColorBrush(Colors.White);
        private static Rectangle rect;
        private static List<Line> isoLines = new List<Line>();

        // selecting
        private static List<IMapObject> selectedObjects = new List<IMapObject>();
        private static I3dLocateable beginCell, endCell;
        private static MapObjectType selectFlag = MapObjectType.AllSelectableObjects;

        // moving
        private static bool isMoving;
        private static List<I2dLocateable> orgPos = new List<I2dLocateable>();
        private static I2dLocateable posBeginMove;

        static Selector()
        {
            SearchHub.SelectionPushed += PushSelectionHandler;
            SearchHub.SelectionClearRequested += ClearSelectionHandler;
        }

        private static void ClearSelectionHandler(object sender, EventArgs e)
        {
            UnselectAll();
        }

        private static void PushSelectionHandler(IEnumerable<object> objects)
        {
            foreach (IMapObject obj in objects)
            {
                obj.Select();
                selectedObjects.Add(obj);
            }
            OnSelectionChanged();
        }

        private static void OnSelectionChanged()
        {
            if (prevCount != selectedObjects.Count)
            {
                SelectionChanged?.Invoke();
                prevCount = selectedObjects.Count;
            }
        }

        #region Api
        #region Box Object Selecting
        public static void SetIsometricSelecting(bool enable)
        {
            isIsometric = enable;
        }
        public static void BeginSelecting(Point downPos, Canvas graphic, I3dLocateable begin)
        {
            if (!dragingSelectBox)
            {
                selectDown = downPos;
                dragingSelectBox = true;
                src = graphic;
                beginCell = begin;
                endCell = begin;
                if (isIsometric)
                {
                    for (int i = 0; i< 4; i++)
                    {
                        Line l = new Line()
                        {
                            Stroke = b,
                            StrokeThickness = 1,
                            X1 = downPos.X,
                            X2 = downPos.X,
                            Y1 = downPos.Y,
                            Y2 = downPos.Y
                        };
                        isoLines.Add(l);
                        src.Children.Add(l);
                        Canvas.SetTop(l, 0);
                        Canvas.SetLeft(l, 0);
                    }
                }
                else
                {
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
        }
        public static void EndSelecting(bool reverseSelect)
        {
            if (dragingSelectBox)
            {
                dragingSelectBox = false;
                src.Children.Clear();
                if (beginCell.Coord == endCell.Coord)
                {
                    isoLines.Clear();
                    rect = null;
                    return;
                }
                IEnumerable<I2dLocateable> enumerator;
                if (isIsometric)
                {
                    MapPosition.RegularCellToIsometricSquare(beginCell, endCell, out I2dLocateable up, out I2dLocateable down);
                    enumerator = new Square2D(up, down);
                    isoLines.Clear();
                }
                else
                {
                    MapPosition.RegularCellToSceneSquare(beginCell, endCell, MapWidth, out I2dLocateable lt, out I2dLocateable rb);
                    enumerator = new SceneSquare2D(lt, rb, MapWidth);
                    rect = null;
                }

                foreach (I2dLocateable pos in enumerator)
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
                                else if (!reverseSelect && !selectedObjects.Contains(obj))
                                {
                                    obj.Select();
                                    selectedObjects.Add(obj);
                                }
                            }
                        }
                    }
                }
                OnSelectionChanged();
                PlaySelectedSound();
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
                        bool reverse = reverseSelect && selectedObjects.Contains(obj);
                        bool select = !(reverseSelect || selectedObjects.Contains(obj));
                        if (obj is InfantryItem inf)
                        {
                            if (inf.SubCell != subcell) continue;
                        }
                        if (reverse)
                        {
                            obj.CancelSelection();
                            selectedObjects.Remove(obj);
                        }
                        if (select)
                        {
                            obj.Select();
                            selectedObjects.Add(obj);
                        }
                        if (reverse || select) break;
                    }
                }
                OnSelectionChanged();
                PlaySelectedSound();
            }
        }
        public static void UpdateSelectingRectangle(Point newPos, I3dLocateable end)
        {
            if (dragingSelectBox)
            {
                selectNew = newPos;
                endCell = end;
                if (isIsometric)
                {
                    double xRight, yRight, xLeft, yLeft;
                    double x1 = selectDown.X, x2 = selectNew.X, y1 = selectDown.Y, y2 = selectNew.Y;
                    double invK = 2;
                    xRight = (invK * (y1 - y2) + x1 + x2) / 2;
                    yRight = (invK * (y1 + y2) + x1 - x2) / 2 / invK;
                    xLeft = (x1 + x2 - invK * (y1 - y2)) / 2;
                    yLeft = (x2 - x1 + invK * (y2 + y1)) / 2 / invK;
                    // up right line(persume)
                    isoLines[0].X2 = xRight;
                    isoLines[0].Y2 = yRight;

                    // down right line
                    isoLines[1].X1 = xRight;
                    isoLines[1].Y1 = yRight;
                    isoLines[1].X2 = x2;
                    isoLines[1].Y2 = y2;

                    // down left line
                    isoLines[2].X1 = x2;
                    isoLines[2].Y1 = y2;
                    isoLines[2].X2 = xLeft;
                    isoLines[2].Y2 = yLeft;

                    // up left line
                    isoLines[3].X2 = xLeft;
                    isoLines[3].Y2 = yLeft;

                    // canvas
                }
                else
                {
                    Canvas.SetTop(rect, Math.Min(selectNew.Y, selectDown.Y));
                    Canvas.SetLeft(rect, Math.Min(selectNew.X, selectDown.X));
                    rect.Width = Math.Abs(selectNew.X - selectDown.X);
                    rect.Height = Math.Abs(selectNew.Y - selectDown.Y);
                }
            }
        }
        public static void UnselectAll()
        {
            foreach (IMapObject obj in selectedObjects) obj.CancelSelection();
            selectedObjects.Clear();
            OnSelectionChanged();
        }
        public static void UnselectObject(IEnumerable<IMapObject> src)
        {
            IEnumerable<IMapObject> intersect = selectedObjects.Intersect(src);
            foreach (IMapObject obj in intersect)
            {
                obj.CancelSelection();
                selectedObjects.Remove(obj);
            }
            OnSelectionChanged();
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
        public static void SetSelectionFlag(MapObjectType type, bool isEnable)
        {
            if (isEnable) AddSelectionFlag(type);
            else RemoveSelectionFlag(type);
        }
        #endregion
        #region Moving and deleting
        public static void BeginSelectedObjectsMoving(I2dLocateable beginCell)
        {
            isMoving = true;
            orgPos.Clear();
            foreach (var obj in selectedObjects)
            {
                if (obj is IPosition inf) orgPos.Add(new PntPos(inf));
                else orgPos.Add(new PntPos(obj));
            }
            MapApi.BeginMove(selectedObjects);
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
                        if (obj is InfantryItem inf)
                        {
                            int destSubcell = selectedObjects.Count == 1 ? subcell : inf.SubCell;
                            MapApi.MoveObjectTo(obj, objDest, destSubcell, false);
                        }
                        else MapApi.MoveObjectTo(obj, objDest, subcell, false);
                    }
                }
            }
        }
        public static void EndSelectedObjectsMoving()
        {
            isMoving = false;
            MapApi.EndMove(selectedObjects);
            UndoRedoHub.PushCommand(selectedObjects, new List<I2dLocateable>(orgPos), new List<I2dLocateable>(selectedObjects));
        }
        public static bool IsPositionHasSelectedItem(I2dLocateable pos, int subcell = -1)
        {
            foreach (IMapObject obj in selectedObjects)
            {
                if (obj.Coord == pos.Coord)
                {
                    if (obj is InfantryItem inf)
                    {
                        if (inf.SubCell == subcell) return true;
                        else continue;
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
            if (selectedObjects.Count > 0) UndoRedoHub.PushCommand(selectedObjects);
            selectedObjects.Clear();
            OnSelectionChanged();
        }
        #endregion
        public static void CallUpdateSelection()
        {
            OnSelectionChanged();
        }
        #endregion



        #region Fun: play selected sound
        private static void PlaySelectedSound()
        {
            var rules = GlobalVar.GlobalRules;
            IEnumerable<IMapObject> playable = selectedObjects.Where(x => (x.ObjectType & (MapObjectType.CombatObject | MapObjectType.BaseNode)) != 0);
            HashSet<string> names = playable.Select(x => x.RegName).Distinct().ToHashSet();
            List<IniSystem.INIEntity> playableEnts = new List<IniSystem.INIEntity>();
            foreach (string name in names) playableEnts.Add(rules[name]);
            if (playableEnts.Count <= 0) return;
            int maxTech = playableEnts.Max(x => x.ParseInt("TechLevel", -1));
            IniSystem.INIEntity entMax = playableEnts.First(x => x.ParseInt("TechLevel", -1) == maxTech);
            string soundName = entMax["VoiceSelect"].ToString();
            NavigationHub.PlaySound(soundName, SoundType.SoundBankRnd);
        }
        #endregion


        public static IEnumerable<IMapObject> SelectedObjects { get { return selectedObjects; } }
        public static bool IsMoving { get { return isMoving; } }
        public static bool IsSelecting { get { return dragingSelectBox; } }
        public static bool IsIsometric { get { return isIsometric; } }
    }
}
