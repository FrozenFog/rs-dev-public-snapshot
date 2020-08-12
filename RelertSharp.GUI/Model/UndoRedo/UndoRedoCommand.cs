using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.GUI.Model
{
    internal class UndoRedoCommand
    {
        public UndoRedoCommand(UndoRedoCommandType type, IEnumerable<object> paramPrev, IEnumerable<object> paramNow)
        {
            CommandType = type;
            PrevParam = paramPrev;
            NowParam = paramNow;
        }


        #region Public Calls - UndoRedoCommand
        public UndoRedoCommandType CommandType { get; private set; }
        public IEnumerable<object> PrevParam { get; private set; }
        public IEnumerable<object> NowParam { get; private set; }
        #endregion
    }
}
