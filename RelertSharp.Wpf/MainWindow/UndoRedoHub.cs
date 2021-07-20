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
        private static Stack<UndoRedoCommand> undoCommands = new Stack<UndoRedoCommand>();
        private static Stack<UndoRedoCommand> redoCommands = new Stack<UndoRedoCommand>();
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
                return true;
            }
            return false;
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
            UndoRedoCommand command = new UndoRedoCommand(CommandType.AddObject, cmds.JoinBy(), obj);
            PushCommand(command);
        }
        /// <summary>
        /// Remove object from map
        /// </summary>
        /// <param name="obj"></param>
        public static void PushCommand(IEnumerable<IMapObject> src)
        {
            List<string> cmds = new List<string>();
            cmds.Add(src.Count().ToString());
            foreach (IMapObject obj in src)
            {
                string[] arr = obj.ExtractParameter();
                cmds.Add(arr.Length.ToString());
                cmds.AddRange(arr);
            }
            UndoRedoCommand cmd = new UndoRedoCommand(CommandType.RemoveObject, cmds.JoinBy(), new List<IMapObject>(src));
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
            UndoRedoCommand cmd = new UndoRedoCommand(CommandType.MoveObject, cmds.JoinBy(), referance);
            PushCommand(cmd);
        }
        #endregion

        private static void PushCommand(UndoRedoCommand cmd)
        {
            redoCommands.Clear();
            undoCommands.Push(cmd);
        }



        private class UndoRedoCommand
        {
            private object refer;
            public UndoRedoCommand(CommandType type, string parameter, IMapObject referance = null)
            {
                Type = type;
                Command = parameter;
                refer = referance;
            }
            public UndoRedoCommand(CommandType type, string parameter, IEnumerable<IMapObject> referances)
            {
                Type = type;
                Command = parameter;
                refer = referances;
            }


            public void Execute()
            {
                switch (Type)
                {
                    case CommandType.AddObject:
                        ExecCommand.AddObject(Command, refer as IMapObject);
                        break;
                    case CommandType.AddTile:
                        ExecCommand.AddTile(Command);
                        break;
                    case CommandType.RemoveObject:
                        foreach (IMapObject obj in refer as IEnumerable<IMapObject>) ExecCommand.RemoveObject(obj);
                        break;
                    case CommandType.MoveObject:
                        ExecCommand.MoveObject(Command, refer as IEnumerable<IMapObject>);
                        break;
                }
            }
            public void ReverseExecute()
            {
                switch (Type)
                {
                    case CommandType.AddObject:
                        ExecCommand.RemoveObject(refer as IMapObject);
                        break;
                    case CommandType.AddTile:
                        ExecCommand.RedoTile(Command);
                        break;
                    case CommandType.RemoveObject:
                        ExecCommand.AddObject(Command, refer as IEnumerable<IMapObject>);
                        break;
                    case CommandType.MoveObject:
                        ExecCommand.MoveObjectBack(Command, refer as IEnumerable<IMapObject>);
                        break;
                }
            }



            #region Call
            public CommandType Type { get; set; }
            public string Command { get; set; }
            #endregion
        }


        private static class ExecCommand
        {
            public static IMapObject AddObject(string parameter, IMapObject referance)
            {
                ParameterReader reader = new ParameterReader(parameter);
                int len = reader.ReadInt();
                string[] arr = reader.Take(len);
                //IMapObject add = referance.ConstructFromParameter(arr);
                MapApi.AddObject(referance);
                EngineApi.DrawObject(referance);
                return referance;
            }
            public static IEnumerable<IMapObject> AddObject(string parameter, IEnumerable<IMapObject> referances)
            {
                List<IMapObject> result = new List<IMapObject>();
                ParameterReader reader = new ParameterReader(parameter);
                int count = reader.ReadInt();
                IMapObject[] refers = referances.ToArray();
                for (int i = 0; i < count; i++)
                {
                    int len = reader.ReadInt();
                    string[] arr = reader.Take(len);
                    IMapObject add = refers[i]/*.ConstructFromParameter(arr)*/;
                    MapApi.AddObject(add);
                    EngineApi.DrawObject(add);
                    result.Add(add);
                }
                return result;
            }
            public static void RemoveObject(IMapObject referance)
            {
                MapApi.RemoveObject(referance);
            }
            public static void AddTile(string parameter)
            {

            }
            public static void RedoTile(string parameter)
            {

            }
            public static void MoveObject(string command, IEnumerable<IMapObject> targets)
            {
                ParameterReader reader = new ParameterReader(command);
                int count = reader.ReadInt();
                IMapObject[] objs = targets.ToArray();
                MapApi.BeginMove(targets);
                for (int i = 0; i< count; i++)
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
            public static void MoveObjectBack(string command, IEnumerable<IMapObject> targets)
            {
                ParameterReader reader = new ParameterReader(command);
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
    }
}
