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
        private readonly Stack<UndoRedoCommand> _UndoStack = new Stack<UndoRedoCommand>();
        private readonly Stack<UndoRedoCommand> _RedoStack = new Stack<UndoRedoCommand>();

        public UndoRedo() { }

        public void Undo()
        {

        }

        public void Redo()
        {

        }

        public void PushCommand(UndoRedoCommand cmd)
        {

        }
        public void PushCommand(UndoRedoCommandType type, IEnumerable<object> paramPrev, IEnumerable<object> paramNow)
        {
            PushCommand(new UndoRedoCommand(type, paramPrev, paramNow));
        }
    }

    internal class UndoRedoCommand
    {
        public UndoRedoCommand(UndoRedoCommandType type, IEnumerable<object> paramPrev, IEnumerable<object> paramNow)
        {

        }
    }
}
