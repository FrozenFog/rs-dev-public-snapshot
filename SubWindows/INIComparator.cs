using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using relert_sharp.Utils;
using relert_sharp.FileSystem;

namespace relert_sharp.SubWindows
{
    public partial class INIComparator : Form
    {
        private Cons.Language language;
        private Lang Trans = null;
        public INIComparator(Cons.Language lang)
        {
            InitializeComponent();
            Lang l = new Lang(lang);
            language = lang;
            Trans = l;
            Set_Language();
        }

        private void btnSelectPath1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Title = Trans.Ds("CMPSelectINITitle");
            o.Filter = Trans.Ds("CMPSelectINIini") + "| *.ini|" + Trans.Ds("CMPSelectINImap") + " | *.map,*.yrm,*.mpr";
            o.InitialDirectory = Application.StartupPath;
            if (DialogResult.OK == o.ShowDialog())
            {
                txbINIAPath.Text = o.FileName;
            }
        }

        private void btnSelectPath2_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Title = Trans.Ds("CMPSelectINITitle");
            o.Filter = Trans.Ds("CMPSelectINIini") + "| *.ini|" + Trans.Ds("CMPSelectINImap") + " | *.map,*.yrm,*.mpr";
            o.InitialDirectory = Application.StartupPath;
            if (DialogResult.OK == o.ShowDialog())
            {
                txbINIBPath.Text = o.FileName;
            }
        }

        private void btnRunCompare_Click(object sender, EventArgs e)
        {
            INIFile f1 = new INIFile(txbINIAPath.Text);
            INIFile f2 = new INIFile(txbINIBPath.Text);
            DiffLog log = new DiffLog(language);
            foreach (INIEntity entF2 in f2.IniData)
            {
                // if is just list, compare with list method then next
                if (Cons.EntName.DictionaryList.Contains(entF2.Name))
                {
                    log.AddLog(f1.GetEnt(entF2.Name), entF2);
                    f1.RemoveEnt(entF2);
                    continue;
                }
                // if both have same entity
                // compare both, remove both
                if (f1.HasIniEnt(entF2))
                {
                    INIEntity entF1 = f1.GetEnt(entF2.Name);
                    f1.RemoveEnt(entF2);
                    // if not system entity
                    // parse int, float, etc
                    if (!(Cons.EntName.SystemEntity.Contains(entF2.Name) || Cons.EntName.DictionaryList.Contains(entF2.Name)))
                    {
                        entF1.ConvPairs();
                        entF2.ConvPairs();
                    }
                    foreach (INIPair p2 in entF2.DataList)
                    {
                        // if ent1 have same pair with ent2
                        // compare both, remove both
                        if (entF1.HasPair(p2))
                        {
                            INIPair p1 = entF1.GetPair(p2.Name);
                            if (p1.Value != p2.Value)
                            {
                                log.AddLog(entF1.Name, p1, p2);
                            }
                            entF1.RemovePair(p1);
                        }
                        // ent1 dont have p2, this item was newly added
                        else
                        {
                            log.AddLog(entF1.Name, INIPair.NullPair, p2);
                        }
                    }
                    // ent2 is empty now, what remains in ent1 is removed in current version
                    foreach (INIPair p1 in entF1.DataList)
                    {
                        log.AddLog(entF1.Name, p1, INIPair.NullPair);
                    }
                }
                // f1 dont have ent2, this entity was newly added
                else
                {
                    entF2.ConvPairs();
                    foreach (INIPair p2 in entF2.DataList)
                    {
                        log.AddLog(entF2.Name, INIPair.NullPair, p2);
                    }
                }
            }
            // ini2 is empty now, what remains in ini1 is removed in current version
            foreach (INIEntity ent in f1.IniData)
            {
                ent.ConvPairs();
                foreach (INIPair p1 in ent.DataList)
                {
                    log.AddLog(ent.Name, p1, INIPair.NullPair);
                }
            }
            log.Translate(Trans);
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = Trans.Ds("ResultSaveTitle");
            dlg.FileName = "CompareResult.txt";
            dlg.Filter = Trans.Ds("CMPResultSaveText") + "|*.txt|" + Trans.Ds("CMPResultSaveCSV") + "|*.csv";
            dlg.AddExtension = true;
            dlg.InitialDirectory = Application.StartupPath;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Utils.File f = new Utils.File(dlg.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                ////////
            }
        }
        private void Set_Language()
        {
            foreach(Control c in Controls)
            {
                if(c.GetType() != typeof(TextBox))
                {
                    c.Text = Trans.Ds(c.Text);
                }
            }
        }
        public class DiffLog
        {
            private List<string> statedNames = new List<string>();
            private Dictionary<string, List<LogEntry>> logs = new Dictionary<string, List<LogEntry>>();
            private Cons.Language language;
            public DiffLog(Cons.Language l)
            {
                language = l;
            }
            public void AddLog(string entName, INIPair pOlder, INIPair pNewer)
            {
                if (!statedNames.Contains(entName))
                {
                    statedNames.Add(entName);
                    logs[entName] = new List<LogEntry>();
                }
                logs[entName].Add(new LogEntry(pOlder.Name, pOlder.Value, pNewer.Value, language));
            }
            public void AddLog(INIEntity entOlder, INIEntity entNewer)
            {
                if (!statedNames.Contains(entOlder.Name))
                {
                    statedNames.Add(entOlder.Name);
                }

            }
            public void Translate(Lang Trans)
            {
                Dictionary<string, List<LogEntry>> buffer = new Dictionary<string, List<LogEntry>>();
                foreach (string key in logs.Keys)
                {
                    string newkey = Trans.Ds(key);
                    foreach (LogEntry ent in logs[key])
                    {
                        foreach (string s in ent.Description)
                        {
                            ent.Result += Trans.Ds(s);
                        }
                    }
                    buffer[newkey] = logs[key];
                }
                logs = buffer;
            }
            private class LogEntry
            {
                private string itemName;
                private List<string> describ;
                private string result = "";
                public LogEntry(string name, object older, object newer, Cons.Language language)
                {
                    itemName = name;
                    if (Cons.Interpreter.SightLike.Contains(itemName))
                    {
                        string modify = "Increased";
                        if (older.GetType() == typeof(int))
                        {
                            if ((int)older > (int)newer)
                            {
                                modify = "Decreased";
                            }
                        }
                        else
                        {
                            if ((float)older > (float)newer)
                            {
                                modify = "Decreased";
                            }
                        }
                        switch (language)
                        {
                            case Cons.Language.EnglishUS:
                                describ = new List<string>() { itemName, modify, "From", older.ToString(), "Into", newer.ToString() };
                                break;
                            case Cons.Language.Chinese:
                                describ = new List<string>() { itemName, "From", older.ToString(), modify, "Into", newer.ToString() };
                                break;
                        }
                    }
                    else if (Cons.Interpreter.ActiveBoolLike.Contains(itemName))
                    {
                        string modify = "Unable";
                        if ((bool)newer)
                        {
                            modify = "Able";
                        }
                        describ = new List<string>() { "Is_Now", modify, itemName };
                    }
                    else if (Cons.Interpreter.PassiveBoolLike.Contains(itemName))
                    {
                        string modify = "Cant";
                        if ((bool)newer)
                        {
                            modify = "Can";
                        }
                        describ = new List<string>() { "Now", modify, itemName };
                    }
                    else if (Cons.Interpreter.AcquireBoolLike.Contains(itemName))
                    {
                        string modify = "Lost";
                        if ((bool)newer)
                        {
                            modify = "Gain";
                        }
                        describ = new List<string>() { "Now", modify, itemName };
                    }
                }
                #region Public Calls
                public string ItemName
                {
                    get { return itemName; }
                }
                public List<string> Description
                {
                    get { return describ; }
                }
                public string Result
                {
                    get { return result; }
                    set { result = value; }
                }
                #endregion
            }
        }
    }
}
