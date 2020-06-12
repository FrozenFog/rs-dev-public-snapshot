using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RelertSharp.Common
{
    public class RsLog
    {
        private FileStream _fs;
        private StreamWriter sw;
        private StringBuilder criticalMsg;
        private bool isStream = false;


        #region Ctor
        public RsLog()
        {
            if (!File.Exists("rs.log")) _fs = new FileStream("rs.log", FileMode.Create, FileAccess.Write);
            else _fs = new FileStream("rs.log", FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(_fs);
            criticalMsg = new StringBuilder();
        }
        #endregion


        #region Public Methods
        public void Critical(string msg)
        {
            HasCritical = true;
            sw.WriteLine(string.Format("{0}: {1}", DateTime.Now, msg));
            if (!isStream) sw.Flush();
            criticalMsg.AppendLine(msg);
        }
        public string ShowCritical()
        {
            string msg = criticalMsg.ToString();
            criticalMsg.Clear();
            HasCritical = false;
            return msg;
        }
        public void Write(string msg)
        {
            sw.WriteLine(string.Format("{0}: {1}", DateTime.Now, msg));
            if (!isStream) sw.Flush();
        }
        public void BeginWrite()
        {
            isStream = true;
        }
        public void EndWrite()
        {
            isStream = false;
            sw.Flush();
        }
        public void Dispose()
        {
            sw.Flush();
            _fs.Dispose();
        }
        #endregion


        #region Public Calls
        public bool HasCritical { get; set; }
        #endregion
    }
}
