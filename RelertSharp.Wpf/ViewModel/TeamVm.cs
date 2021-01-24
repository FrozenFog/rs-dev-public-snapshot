using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using System.Windows.Data;

namespace RelertSharp.Wpf.ViewModel
{
    internal class TeamVm : BaseVm<TeamItem>
    {
        public TeamVm()
        {
            data = new TeamItem();
        }
        public TeamVm(object obj) : base(obj) { }
        public Dictionary<string, INIPair> Data
        {
            get { return data.Residue; }
            set
            {
                data.Residue = value;
                SetProperty();
            }
        }
        //public bool GetBool(string key)
        //{
        //    return data.Residue[key].ParseBool();
        //}
        //public void SetBool(string key, bool value)
        //{
        //    if (!data.Residue.ContainsKey(key)) data.Residue[key] = new INIPair(key);
        //    data.Residue[key].Value = value.YesNo();
        //    OnDesignatedPropertyChanged(key);
        //}
        //public string GetText(string key)
        //{
        //    return data.Residue[key].Value;
        //}
        //public void SetText(string key, string value)
        //{
        //    if (!data.Residue.ContainsKey(key)) data.Residue[key] = new INIPair(key);
        //    data.Residue[key].Value = value;
        //    OnDesignatedPropertyChanged(key);
        //}

        public string Name
        {
            get { return data.Name; }
            set
            {
                data.Name = value;
                SetProperty();
            }
        }
    }
}
