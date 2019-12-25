using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relert_sharp.Common
{
    public class CSFString
    {
        public CSFString(string _uiTag)
        {
            UIName = _uiTag;
        }


        #region Public Calls - CSFString
        public string UIName { get; set; }
        public string ContentString { get; set; }
        #endregion
    }
}
