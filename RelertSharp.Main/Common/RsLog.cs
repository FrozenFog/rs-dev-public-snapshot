using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

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
            string path = Application.StartupPath + "\\Log";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string name = string.Format("{6}\\{0}-{1}-{2} {3:D2}-{4:D2}-{5:D2}.log", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, path);
            _fs = new FileStream(name, FileMode.Create, FileAccess.Write);
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
        public void Critical(string formatMsg, params object[] param)
        {
            string msg = string.Format(formatMsg, param);
            Critical(msg);
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
        public void Write(string format, params object[] args)
        {
            Write(string.Format(format, args));
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
