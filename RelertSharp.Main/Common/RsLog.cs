using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace RelertSharp.Common
{ 
    public class RsLog
    {
        private FileStream _fs;
        private StreamWriter sw;
        private StringBuilder criticalMsg;
        private bool isStream = false, logOverride = false;
        private LogLevel _logLvl = LogLevel.Asterisk;


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


        #region Private
        private void BaseWrite(string message, LogLevel logLevel)
        {
            LogLevel lvl = _logLvl;
            if (!logOverride && GlobalVar.GlobalConfig != null)
            {
                lvl = GlobalVar.GlobalConfig.UserConfig.LogLevel;
            }
            if ((int)logLevel >= (int)lvl)
            {
                string msg = string.Format("[{0}({3})]\t{1}: {2}", logLevel, DateTime.Now, message, (int)logLevel);
                sw.WriteLine(msg);
                if (!isStream) sw.Flush();
            }
        }
        #endregion


        #region Public Methods
        /// <summary>
        /// some error occured and CAUSE INFORMATION LOSS
        /// </summary>
        /// <param name="msg"></param>
        public void Critical(string msg)
        {
            HasCritical = true;
            BaseWrite(msg, LogLevel.Critical);
            criticalMsg.AppendLine(msg);
        }
        /// <summary>
        /// some error occured and CAUSE INFORMATION LOSS
        /// </summary>
        /// <param name="formatMsg"></param>
        /// <param name="param"></param>
        public void Critical(string formatMsg, params object[] param)
        {
            string msg = string.Format(formatMsg, param);
            Critical(msg);
        }
        /// <summary>
        /// log such as engine established
        /// </summary>
        /// <param name="format"></param>
        /// <param name="param"></param>
        public void Info(string format, params object[] param)
        {
            string msg = string.Format(format, param);
            BaseWrite(msg, LogLevel.Info);
        }
        /// <summary>
        /// log such as engine established
        /// </summary>
        /// <param name="msg"></param>
        public void Info(string msg)
        {
            BaseWrite(msg, LogLevel.Info);
        }
        /// <summary>
        /// some error occured and CAN still operate
        /// </summary>
        /// <param name="format"></param>
        /// <param name="param"></param>
        public void Warning(string format, params object[] param)
        {
            string msg = string.Format(format, param);
            BaseWrite(msg, LogLevel.Warning);
        }
        /// <summary>
        /// some error occured and CAN still operate
        /// </summary>
        /// <param name="message"></param>
        public void Warning(string message)
        {
            BaseWrite(message, LogLevel.Warning);
        }
        /// <summary>
        /// progress info that cannot be ignored
        /// </summary>
        /// <param name="message"></param>
        public void Asterisk(string message)
        {
            BaseWrite(message, LogLevel.Asterisk);
        }
        public string ShowCritical()
        {
            string msg = criticalMsg.ToString();
            criticalMsg.Clear();
            HasCritical = false;
            return msg;
        }
        public void Write(string msg, LogLevel logLevel = LogLevel.Anything)
        {
            BaseWrite(msg, logLevel);
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
        public bool SetLogLevel(LogLevel lvl)
        {
            _logLvl = lvl;
            logOverride = true;
            return true;
        }
        #endregion


        #region Public Calls
        public bool HasCritical { get; set; }
        #endregion
    }
}
