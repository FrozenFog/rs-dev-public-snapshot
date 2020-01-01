using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relert_sharp.Common
{
    public class CSFString
    {



        #region Constructor - CSFString
        public CSFString(string _uiTag)
        {
            UIName = _uiTag;
        }
        #endregion


        #region Public Calls - CSFString
        public string UIName { get; set; }
        public string ContentString { get; set; }
        #endregion
    }
}
