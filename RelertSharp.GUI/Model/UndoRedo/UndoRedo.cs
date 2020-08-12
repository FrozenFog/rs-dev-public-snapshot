using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.GUI.Model
{
    internal enum UndoRedoCommandType
    {
        DrawTile,
        BucketTile,
        SelectTile,
        DrawObject,
        SelectObject,
    }
    internal class UndoRedo
    {
        private readonly LinkedList<UndoRedoCommand> _UndoList = new LinkedList<UndoRedoCommand>();
        private readonly LinkedList<UndoRedoCommand> _RedoList = new LinkedList<UndoRedoCommand>();

        public UndoRedo() { }

        public void Undo()
        {
            if (_UndoList.Count > 0)
            {
                UndoRedoCommand cmd = _UndoList.Last();_UndoList.RemoveLast();
                _RedoList.AddLast(cmd);
                ExecuteCommand(cmd.CommandType, cmd.PrevParam);
            }
        }

        public void Redo()
        {
            if (_RedoList.Count > 0)
            {
                UndoRedoCommand cmd = _RedoList.Last(); _RedoList.RemoveLast();
                _UndoList.AddLast(cmd);
                ExecuteCommand(cmd.CommandType, cmd.NowParam);
            }
        }

        public void PushCommand(UndoRedoCommand cmd)
        {
            if (_RedoList.Count > 0) _RedoList.Clear();
            if (_UndoList.Count == 50) _UndoList.RemoveFirst();
            _UndoList.AddLast(cmd);
        }
        public void PushCommand(UndoRedoCommandType type, IEnumerable<object> paramPrev, IEnumerable<object> paramNow)
        {
            PushCommand(new UndoRedoCommand(type, paramPrev, paramNow));
        }


        private void ExecuteCommand(UndoRedoCommandType type, IEnumerable<object> param)
        {

        }
    }
}
