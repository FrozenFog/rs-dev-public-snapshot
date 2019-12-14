using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relert_sharp.Utils
{
    public class RSException : ApplicationException
    {
        public class EntityNotFoundException :ApplicationException
        {
            private string entityname, parent;
            public EntityNotFoundException(string entityName, string host)
            {
                entityname = entityName;
                parent = host;
            }
            public string EntityName
            {
                get { return entityname; }
            }
            public string Host
            {
                get { return parent; }
            }
        }
        public class MixEntityNotFoundException : ApplicationException
        {
            private string mixName, fileName;
            public MixEntityNotFoundException(string mixname, string filename)
            {
                mixName = mixname;
                fileName = filename;
            }
            public string MixName
            {
                get { return mixName; }
            }
            public string FileName
            {
                get { return fileName; }
            }
        }
        private string error;
        private Exception innerException;

        public RSException() { }
        public RSException(string msg) :base(msg)
        {
            error = msg;
        }
        public RSException(string msg, Exception innerException):base(msg,innerException)
        {
            this.innerException = innerException;
            error = msg;
        }
        public string GetError()
        {
            return error;
        }
    }
}
