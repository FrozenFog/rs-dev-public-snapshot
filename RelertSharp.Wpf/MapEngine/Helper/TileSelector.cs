using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Algorithm;
using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.Engine.Api;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using RelertSharp.Terraformer;
using RelertSharp.Engine;

namespace RelertSharp.Wpf.MapEngine.Helper
{
    internal static class TileSelector
    {
        public static event Action SelectedTileChanged;
        private static DdaLineDrawing dda = new DdaLineDrawing();
        private static LinkedList<I2dLocateable> controlNodes = new LinkedList<I2dLocateable>();
        private static I2dLocateable prevCell;
        private static HashSet<Tile> selected = new HashSet<Tile>();
        private static bool filtSet, filtHeight;
        private static bool isIsometric = false;
        private static TileLayer Tiles { get { return GlobalVar.GlobalMap.TilesData; } }

        static TileSelector()
        {
            MouseState.MouseStateChanged += HandleStateChanged;
        }

        private static void HandleStateChanged()
        {
            if (MouseState.PrevState == PanelMouseState.TileLineSelecting) controlNodes.Clear();
        }


        #region Api
        #region Selecting
        #region Line
        public static void AddLineControlNode(I2dLocateable cell, bool select = true)
        {
            var tiles = GlobalVar.GlobalMap.TilesData;
            controlNodes.AddLast(cell);
            if (select)
            {
                dda.SetControlNode(prevCell == null ? cell : prevCell, cell);
                EngineApi.InvokeLock();
                foreach (I2dLocateable pos in dda.GetLineCells())
                {
                    if (tiles[pos] is Tile t)
                    {
                        t.Select();
                        selected.Add(t);
                    }
                }
                EngineApi.InvokeUnlock();
                OnSelectionChanged();
            }
            prevCell = cell;
        }
        #endregion
        #region Bucket
        public static void BucketAt(I2dLocateable cell)
        {
            var tiles = GlobalVar.GlobalMap.TilesData;
            var dict = GlobalVar.TileDictionary;
            if (tiles[cell] is Tile center)
            {
                int set = dict.GetTileSetIndexFromTile(center);
                List<Predicate<Tile>> predicates = new List<Predicate<Tile>>()
                {
                    x => !x.IsSelected
                };
                if (filtSet) predicates.Add(x => dict.GetTileSetIndexFromTile(x) == set);
                if (filtHeight) predicates.Add(x => x.RealHeight == center.RealHeight);
                Bfs2D bfs = new Bfs2D(tiles, center, predicates);
                EngineApi.InvokeLock();
                foreach (Tile t in bfs)
                {
                    t.Select();
                    selected.Add(t);
                }
                EngineApi.InvokeUnlock();
                OnSelectionChanged();
            }
        }
        public static void BucketTilesetFilter(bool enable)
        {
            filtSet = enable;
        }
        public static void BucketHeightFilter(bool enable)
        {
            filtHeight = enable;
        }
        #endregion
        #region Box and Iso
        private static Point selectDown, selectNew;
        private static bool dragingSelectBox;
        private static Canvas src;
        private static I3dLocateable beginCell, endCell;
        private static SolidColorBrush b = new SolidColorBrush(Colors.White);
        private static List<Line> isoLines = new List<Line>();
        private static Rectangle rect;
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
                    for (int i = 0; i < 4; i++)
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
                var Map = GlobalVar.GlobalMap;
                var mapInfo = Map?.Info;
                dragingSelectBox = false;
                src.Children.Clear();
                if (beginCell.Coord == endCell.Coord)
                {
                    isoLines.Clear();
                    rect = null;
                    OnSelectionChanged();
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
                    MapPosition.RegularCellToSceneSquare(beginCell, endCell, mapInfo.Size.Width, out I2dLocateable lt, out I2dLocateable rb);
                    enumerator = new SceneSquare2D(lt, rb, mapInfo.Size.Width);
                    rect = null;
                }

                foreach (I2dLocateable pos in enumerator)
                {
                    if (Map.TilesData[pos] is Tile t)
                    {
                        if (reverseSelect && selected.Contains(t))
                        {
                            t.CancelSelection();
                            selected.Remove(t);
                        }
                        else if (!reverseSelect && !selected.Contains(t))
                        {
                            t.Select();
                            selected.Add(t);
                        }
                    }
                }
                OnSelectionChanged();
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
        #endregion
        #region SingleSelect
        private static bool isSingleSelect, isReverseSingleSelect;
        public static void BeginSingleSelect(bool reverseSelect)
        {
            isReverseSingleSelect = reverseSelect;
            isSingleSelect = true;
        }
        public static void SingleSelectAt(I2dLocateable cell)
        {
            var Map = GlobalVar.GlobalMap;
            if (Map.TilesData[cell] is Tile t)
            {
                bool reverse = isReverseSingleSelect && selected.Contains(t);
                bool select = !(isReverseSingleSelect || selected.Contains(t));
                if (reverse)
                {
                    t.CancelSelection();
                    selected.Remove(t);
                }
                if (select)
                {
                    t.Select();
                    selected.Add(t);
                }
                OnSelectionChanged();
            }
        }
        public static void EndSingleSelect()
        {
            isSingleSelect = false;
            isReverseSingleSelect = false;
        }
        #endregion
        public static void UnselectAll()
        {
            selected.Foreach(x => x.CancelSelection());
            selected.Clear();
            OnSelectionChanged();
        }
        #endregion
        #region Editing
        public static void RiseAllSelectedTile()
        {
            using (var _ = new EngineRegion())
            {
                UndoRedoHub.BeginCommand(selected);
                foreach (Tile t in selected)
                {
                    t.Rise();
                }
                UndoRedoHub.EndCommand(selected);
            }
        }
        public static void RiseTile(I2dLocateable cell)
        {
            using (var _ = new EngineRegion())
            {
                if (Tiles[cell] is Tile t)
                {
                    UndoRedoHub.BeginCommand(t);
                    t.Rise();
                    UndoRedoHub.EndCommand(t);
                }
            }
        }
        public static void SinkAllSelectedTile()
        {
            using (var _ = new EngineRegion())
            {
                UndoRedoHub.BeginCommand(selected);
                foreach (Tile t in selected)
                {
                    t.Sink();
                }
                UndoRedoHub.EndCommand(selected);
            }
        }
        public static void SinkTile(I2dLocateable cell)
        {
            using (var _ = new EngineRegion())
            {
                if (Tiles[cell] is Tile t)
                {
                    UndoRedoHub.BeginCommand(t);
                    t.Sink();
                    UndoRedoHub.EndCommand(t);
                }
            }
        }
        public static void AllSetHeightTo(int height)
        {
            using (var _ = new EngineRegion())
            {
                UndoRedoHub.BeginCommand(selected);
                foreach (Tile t in selected)
                {
                    t.SetHeightTo(height);
                }
                UndoRedoHub.EndCommand(selected);
            }
        }
        public static void RoughRampInSelectedTile()
        {
            using (var _ = new EngineRegion())
            {
                UndoRedoHub.BeginCommand(SelectedTile);
                HillGeneratorConfig cfg = new HillGeneratorConfig()
                {
                    RampBorderUnaffect = true,
                    RiseUnsolvedTile = false,
                    RampFixBorderTreatAsFlat = false
                };
                HillGenerator.RoughRampIn(SelectedTile, cfg);
                UndoRedoHub.EndCommand(SelectedTile);
            }
        }
        public static void FixRampInSelectedTile()
        {
            using (var _ = new EngineRegion())
            {
                UndoRedoHub.BeginCommand(SelectedTile);
                HillGeneratorConfig cfg = new HillGeneratorConfig()
                {
                    RampBorderUnaffect = true,
                    RiseUnsolvedTile = false,
                    RampFixBorderTreatAsFlat = false
                };
                HillGenerator.SmoothRampIn(SelectedTile, cfg);
                HillGenerator.SmoothRampIn(SelectedTile, cfg);
                UndoRedoHub.EndCommand(SelectedTile);
            }
        }
        public static void ClearAllTileAsZero(bool alsoSetHeightToZero)
        {
            using (var _ = new EngineRegion())
            {
                UndoRedoHub.BeginCommand(SelectedTile);
                foreach (Tile t in SelectedTile)
                {
                    if (alsoSetHeightToZero) MapApi.SetTile(0, 0, t, 0);
                    else MapApi.SetTile(0, 0, t);
                }
                UndoRedoHub.EndCommand(SelectedTile);
            }
        }
        #endregion
        #endregion


        #region Private
        private static void OnSelectionChanged()
        {
            SelectedTileChanged?.Invoke();
        }
        #endregion


        #region Calls
        public static IEnumerable<Tile> SelectedTile { get { return selected; } }
        public static bool IsTileSetFilter { get { return filtSet; } }
        public static bool IsHeightFilter { get { return filtHeight; } }
        public static bool IsIsoSeleect { get { return isIsometric; } }
        public static bool IsSelecting { get { return dragingSelectBox; } }
        public static bool IsSingleSelecting { get { return isSingleSelect; } }
        #endregion
    }
}
