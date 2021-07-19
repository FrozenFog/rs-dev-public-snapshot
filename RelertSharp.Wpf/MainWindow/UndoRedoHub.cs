using RelertSharp.Common;
using RelertSharp.Engine.Api;
using RelertSharp.MapStructure;
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
            cmds.Add(obj.ObjectType.ToString());
            string[] arrs = obj.ExtractParameter();
            cmds.Add(arrs.Length.ToString());
            cmds.AddRange(arrs);
            UndoRedoCommand command = new UndoRedoCommand(CommandType.AddObject, cmds.JoinBy(), obj);
            PushCommand(command);
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
            public UndoRedoCommand(CommandType type, string parameter, object referance = null)
            {
                Type = type;
                Command = parameter;
                refer = referance;
            }


            public void Execute()
            {
                switch (Type)
                {
                    case CommandType.AddObject:
                        refer = ExecCommand.AddObject(Command, refer as IMapObject);
                        break;
                    case CommandType.AddTile:
                        ExecCommand.AddTile(Command);
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
                string[] param = parameter.Split(',');
                int len = param[1].ParseInt();
                string[] arr = param.Skip(2).Take(len).ToArray();
                IMapObject add = referance.ConstructFromParameter(arr);
                MapApi.AddObject(add);
                EngineApi.DrawObject(add);
                return add;
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
        }
    }
}
