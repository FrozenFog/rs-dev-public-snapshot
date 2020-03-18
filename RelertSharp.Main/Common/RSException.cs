using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public class RSException : ApplicationException
    {
        public class INIEntityNotFoundException :ApplicationException
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
        }
        public class OverlayOutOfIndexException : ApplicationException
        {
            private string x, y;
            public OverlayOutOfIndexException(string _x, string _y) { x = _x; y = _y; }
            public string X { get { return x; } }
            public string Y { get { return y; } }
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
