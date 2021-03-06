using RelertSharp.Common;
using RelertSharp.Engine.Api;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf
{
    internal static class UndoRedoHub
    {
        public static event Action CommandExecuted, CommandPushed;
        private static Stack<IUndoRedoCommand> undoCommands = new Stack<IUndoRedoCommand>();
        private static Stack<IUndoRedoCommand> redoCommands = new Stack<IUndoRedoCommand>();
        private enum CommandType
        {
            AddObject,
            RemoveObject,
            AddTile,
            MoveObject
        }


        #region Api
        public static bool Undo()
        {
            if (undoCommands.Count > 0)
            {
                var cmd = undoCommands.Pop();
                cmd.ReverseExecute();
                redoCommands.Push(cmd);
                CommandExecuted?.Invoke();
                return true;
            }
            return false;
        }
        public static bool Redo()
        {
            if (redoCommands.Count > 0)
            {
                var cmd = redoCommands.Pop();
                cmd.Execute();
                undoCommands.Push(cmd);
                CommandExecuted?.Invoke();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Replace object in map
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="before"></param>
        public static void PushCommand(IEnumerable<IMapObject> dest, IEnumerable<IMapObject> before)
        {
            List<string> cmdNow = new List<string>();
            cmdNow.Add(dest.Count().ToString());
            foreach (IMapObject d in dest)
            {
                string[] arr = d.ExtractParameter();
                cmdNow.Add(arr.Length.ToString());
                cmdNow.AddRange(arr);
            }
            List<string> cmdBefore = new List<string>();
            cmdBefore.Add(before.Count().ToString());
            foreach (IMapObject b in before)
            {
                string[] arr = b.ExtractParameter();
                cmdBefore.Add(arr.Length.ToString());
                cmdBefore.AddRange(arr);
            }
            AddObjectCommand remove = new AddObjectCommand()
            {
                CommandLine = cmdBefore.JoinBy(),
                IsMultipleObject = true,
                IsRemove = true
            };
            remove.SetReferance(new List<IMapObject>(before));
            AddObjectCommand add = new AddObjectCommand()
            {
                CommandLine = cmdNow.JoinBy(),
                IsMultipleObject = true
            };
            add.SetReferance(new List<IMapObject>(dest));
            CompositeCommand cmd = new CompositeCommand();
            cmd.Commands.Add(remove);
            cmd.Commands.Add(add);
            PushCommand(cmd);
        } 
        /// <summary>
        /// Add object to map
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pos"></param>
        public static void PushCommand(IMapObject obj)
        {
            List<string> cmds = new List<string>();
            string[] arrs = obj.ExtractParameter();
            cmds.Add(arrs.Length.ToString());
            cmds.AddRange(arrs);
            AddObjectCommand cmd = new AddObjectCommand()
            {
                CommandLine = cmds.JoinBy()
            };
            cmd.SetReferance(obj);
            PushCommand(cmd);
        }
        /// <summary>
        /// Remove/add object from map
        /// </summary>
        /// <param name="obj"></param>
        public static void PushCommand(IEnumerable<IMapObject> src, bool isRemove)
        {
            List<string> cmds = new List<string>();
            cmds.Add(src.Count().ToString());
            foreach (IMapObject obj in src)
            {
                string[] arr = obj.ExtractParameter();
                cmds.Add(arr.Length.ToString());
                cmds.AddRange(arr);
            }
            AddObjectCommand cmd = new AddObjectCommand()
            {
                CommandLine = cmds.JoinBy(),
                IsMultipleObject = true,
                IsRemove = isRemove
            };
            cmd.SetReferance(new List<IMapObject>(src));
            PushCommand(cmd);
        }
        /// <summary>
        /// Move object to destination
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        public static void PushCommand(List<IMapObject> src, List<I2dLocateable> orgPos, List<I2dLocateable> destPos)
        {
            List<string> cmds = new List<string>();
            List<IMapObject> referance = new List<IMapObject>(src);
            cmds.Add(referance.Count.ToString());
            for (int i = 0; i < referance.Count; i++)
            {
                IMapObject obj = src[i];
                string[] line;
                if (obj.ObjectType == MapObjectType.Infantry)
                {
                    IPosition org = orgPos[i] as IPosition;
                    IPosition dest = destPos[i] as IPosition;
                    line = new string[]
                    {
                        ((int)obj.ObjectType).ToString(),
                        org.SubCell.ToString(),
                        org.X.ToString(),
                        org.Y.ToString(),
                        dest.SubCell.ToString(),
                        dest.X.ToString(),
                        dest.Y.ToString(),
                    };
                }
                else
                {
                    I2dLocateable org = orgPos[i];
                    I2dLocateable dest = destPos[i];
                    line = new string[]
                    {
                        ((int)obj.ObjectType).ToString(),
                        org.X.ToString(),
                        org.Y.ToString(),
                        dest.X.ToString(),
                        dest.Y.ToString(),
                    };
                }
                cmds.AddRange(line);
            }
            MoveObjectCommand cmd = new MoveObjectCommand()
            {
                CommandLine = cmds.JoinBy()
            };
            cmd.SetReferance(referance);
            PushCommand(cmd);
        }
        private static List<Tile> tileCommandBuffer = new List<Tile>();
        public static void BeginCommand(IEnumerable<Tile> before)
        {
            tileCommandBuffer.Clear();
            foreach (Tile t in before)
            {
                Tile buff = new Tile(t);
                tileCommandBuffer.Add(buff);
            }
        }
        public static void EndCommand(IEnumerable<Tile> target)
        {
            PushCommand(target.ToList(), tileCommandBuffer);
        }
        public static void BeginCommand(Tile before)
        {
            tileCommandBuffer.Clear();
            tileCommandBuffer.Add(new Tile(before));
        }
        public static void EndCommand(Tile now)
        {
            PushCommand(new List<Tile>() { now }, tileCommandBuffer);
        }
        public static void PushCommand(List<Tile> target, List<Tile> before)
        {
            List<string> cmds = new List<string>();
            cmds.Add(target.Count.ToString());
            for (int i = 0; i< target.Count; i++)
            {
                Tile org = before[i];
                Tile dest = target[i];
                string[] line = new string[]
                {
                    org.TileIndex.ToString(),
                    org.SubIndex.ToString(),
                    org.RealHeight.ToString(),
                    dest.TileIndex.ToString(),
                    dest.SubIndex.ToString(),
                    dest.RealHeight.ToString()
                };
                cmds.AddRange(line);
            }
            TileModifyCommand cmd = new TileModifyCommand()
            {
                CommandLine = cmds.JoinBy()
            };
            cmd.SetReferance(new List<Tile>(before));
            PushCommand(cmd);
        }
        #endregion

        private static void PushCommand(IUndoRedoCommand cmd)
        {
            redoCommands.Clear();
            undoCommands.Push(cmd);
            CommandPushed?.Invoke();
        }



        #region Commands
        private class TileModifyCommand : BaseCommand
        {
            public override void Execute()
            {
                ParameterReader reader = new ParameterReader(CommandLine);
                int count = reader.ReadInt();
                Tile[] targets = (referance as IEnumerable<Tile>).ToArray();
                for (int i = 0; i < count; i++)
                {
                    reader.Skip(3);
                    int index = reader.ReadInt();
                    byte subindex = reader.ReadByte();
                    int height = reader.ReadInt();
                    MapApi.SetTile(index, subindex, targets[i], height);
                }
            }

            public override void ReverseExecute()
            {
                ParameterReader reader = new ParameterReader(CommandLine);
                int count = reader.ReadInt();
                Tile[] targets = (referance as IEnumerable<Tile>).ToArray();
                for (int i = 0; i < count; i++)
                {
                    int index = reader.ReadInt();
                    byte subindex = reader.ReadByte();
                    int height = reader.ReadInt();
                    reader.Skip(3);
                    MapApi.SetTile(index, subindex, targets[i], height);
                }
            }
        }
        private class MoveObjectCommand : BaseCommand
        {
            public override void Execute()
            {
                IEnumerable<IMapObject> targets = referance as IEnumerable<IMapObject>;
                ParameterReader reader = new ParameterReader(CommandLine);
                int count = reader.ReadInt();
                IMapObject[] objs = targets.ToArray();
                MapApi.BeginMove(targets);
                for (int i = 0; i < count; i++)
                {
                    MapObjectType type = (MapObjectType)reader.ReadInt();
                    IMapObject obj = objs[i];
                    if (type == MapObjectType.Infantry)
                    {
                        reader.Skip(3);
                        int subcell = reader.ReadInt();
                        int x = reader.ReadInt(); int y = reader.ReadInt();
                        MapApi.MoveObjectTo(obj, new Pnt(x, y), subcell, false);
                    }
                    else
                    {
                        reader.Skip(2);
                        int x = reader.ReadInt(); int y = reader.ReadInt();
                        MapApi.MoveObjectTo(obj, new Pnt(x, y), -1, false);
                    }
                }
                MapApi.EndMove(targets);
            }

            public override void ReverseExecute()
            {
                IEnumerable<IMapObject> targets = referance as IEnumerable<IMapObject>;
                ParameterReader reader = new ParameterReader(CommandLine);
                int count = reader.ReadInt();
                IMapObject[] objs = targets.ToArray();
                MapApi.BeginMove(targets);
                for (int i = 0; i < count; i++)
                {
                    MapObjectType type = (MapObjectType)reader.ReadInt();
                    IMapObject obj = objs[i];
                    if (type == MapObjectType.Infantry)
                    {
                        int subcell = reader.ReadInt();
                        int x = reader.ReadInt(); int y = reader.ReadInt();
                        reader.Skip(3);
                        MapApi.MoveObjectTo(obj, new Pnt(x, y), subcell, false);
                    }
                    else
                    {
                        int x = reader.ReadInt(); int y = reader.ReadInt();
                        reader.Skip(2);
                        MapApi.MoveObjectTo(obj, new Pnt(x, y), -1, false);
                    }
                }
                MapApi.EndMove(targets);
            }
        }
        private class AddObjectCommand : BaseCommand
        {
            public bool IsMultipleObject { get; set; }
            public bool IsRemove { get; set; }
            private bool exec;
            public override void Execute()
            {
                if (IsRemove && !exec)
                {
                    exec = true;
                    ReverseExecute();
                    return;
                }
                if (IsMultipleObject)
                {
                    IEnumerable<IMapObject> objs = referance as IEnumerable<IMapObject>;
                    ParameterReader reader = new ParameterReader(CommandLine);
                    int count = reader.ReadInt();
                    IMapObject[] refers = objs.ToArray();
                    for (int i = 0; i < count; i++)
                    {
                        int len = reader.ReadInt();
                        string[] arr = reader.Take(len);
                        IMapObject add = refers[i]/*.ConstructFromParameter(arr)*/;
                        MapApi.AddObject(add);
                        EngineApi.DrawObject(add);
                        EngineApi.ApplyLightningToObject(add);
                    }
                }
                else
                {
                    IMapObject obj = referance as IMapObject;
                    ParameterReader reader = new ParameterReader(CommandLine);
                    int len = reader.ReadInt();
                    string[] arr = reader.Take(len);
                    //IMapObject add = referance.ConstructFromParameter(arr);
                    MapApi.AddObject(obj);
                    EngineApi.DrawObject(obj);
                    EngineApi.ApplyLightningToObject(obj);
                }
                exec = false;
            }

            public override void ReverseExecute()
            {
                if (IsRemove && !exec)
                {
                    exec = true;
                    Execute();
                    return;
                }
                if (IsMultipleObject)
                {
                    foreach (IMapObject obj in referance as IEnumerable<IMapObject>) MapApi.RemoveObject(obj);
                }
                else MapApi.RemoveObject(referance as IMapObject);
                exec = false;
            }
        }
        private class CompositeCommand : BaseCommand
        {
            public List<IUndoRedoCommand> Commands = new List<IUndoRedoCommand>();
            public CompositeCommand()
            {

            }
            public override void Execute()
            {
                foreach (IUndoRedoCommand cmd in Commands) cmd.Execute();
            }

            public override void ReverseExecute()
            {
                foreach (IUndoRedoCommand cmd in Commands.Reverse<IUndoRedoCommand>()) cmd.ReverseExecute();
            }
        }
        #endregion



        #region Interface
        private interface IUndoRedoCommand
        {
            void Execute();
            void ReverseExecute();
            string CommandLine { get; }
        }
        private abstract class BaseCommand : IUndoRedoCommand
        {
            protected object referance;
            public string CommandLine { get; set; }
            public abstract void Execute();
            public abstract void ReverseExecute();
            public void SetReferance(IMapObject src)
            {
                referance = src;
            }
            public void SetReferance(IEnumerable<IMapObject> src)
            {
                referance = src;
            }
            public void SetReferance(Tile src)
            {
                referance = src;
            }
            public void SetReferance(IEnumerable<Tile> src)
            {
                referance = src;
            }
        }
        #endregion



        #region Calls
        public static int UndoCount
        {
            get { return undoCommands.Count; }
        }
        public static int RedoCount
        {
            get { return redoCommands.Count; }
        }
        #endregion
    }
}
