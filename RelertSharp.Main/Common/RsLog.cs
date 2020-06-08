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
        #region Ctor
        public RsLog()
        {
            FileStream fs;
            if (!File.Exists("rs.log")) fs = new FileStream("rs.log", FileMode.Create, FileAccess.Write);
        }
        #endregion


        #region Public Methods

        #endregion
    }
}
