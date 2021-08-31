using System;

namespace RelertSharp.Common
{
    public class RSException : ApplicationException
    {
        public class IndexableException : ApplicationException
        {
            private IIndexableItem indexableItem;
            private string message;
            public IndexableException(IIndexableItem src, string message)
            {
                this.message = message;
                indexableItem = src;
            }
            public override string Message
            {
                get { return string.Format("{0} Id: {1}, Name: {2}", message, indexableItem.Id, indexableItem.Name); }
            }
        }
        public class INIEntityNotFoundException : ApplicationException
        {
            private string entityname, parent;
            public INIEntityNotFoundException(string entityName, string host) { entityname = entityName; parent = host; }
            public string EntityName { get { return entityname; } }
            public string Host { get { return parent; } }
        }
        public class MixEntityNotFoundException : ApplicationException
        {
            private string mixName, fileName;
            public MixEntityNotFoundException(string mixname, string filename) { mixName = mixname; fileName = filename; }
            public string MixName { get { return mixName; } }
            public string FileName { get { return fileName; } }
            public override string Message
            {
                get { return string.Format("Cannot find file {0} in any mix directory!", FileName); }
            }
        }
        public class OverlayOutOfIndexException : ApplicationException
        {
            private string x, y;
            public OverlayOutOfIndexException(string _x, string _y) { x = _x; y = _y; }
            public string X { get { return x; } }
            public string Y { get { return y; } }
            public override string Message
            {
                get { return string.Format("Overlay is out of map! X: {0}, Y: {1}", X, Y); }
            }
        }
        public class InvalidWaypointException : ApplicationException
        {
            public InvalidWaypointException(int waypoint) { Waypoint = waypoint; }
            public int Waypoint { get; set; }
        }
        public class InvalidFileException : ApplicationException
        {
            public InvalidFileException(string filename) { FileName = filename; }
            public string FileName { get; set; }


            public class InvalidIdx : ApplicationException
            {
                public InvalidIdx() { }
            }
        }
        public class InvalidLogicException : ApplicationException
        {
            public InvalidLogicException(string id, int idx, LogicType type)
            {
                Id = id;
                Idx = idx;
                Type = type;
            }
            public string Id { get; private  set; }
            public int Idx { get; private set; }
            public LogicType Type { get; private set; }
            public override string Message
            {
                get
                {
                    return string.Format("Invalid {0} info, id {1}, invalid index: {2}", Type, Id, Idx);
                }
            }
        }
        //private string error;
        //private Exception innerException;

        //public RSException() { }
        //public RSException(string msg) :base(msg)
        //{
        //    error = msg;
        //}
        //public RSException(string msg, Exception innerException):base(msg,innerException)
        //{
        //    this.innerException = innerException;
        //    error = msg;
        //}
        //public string GetError()
        //{
        //    return error;
        //}
    }
}
