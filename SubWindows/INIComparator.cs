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
            o.Filter = Trans.Ds("CMPSelectINIini") + "| *.ini|" + Trans.Ds("CMPSelectINImap") + "|*.map;*.yrm;*.mpr";
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
            o.Filter = Trans.Ds("CMPSelectINIini") + "| *.ini|" + Trans.Ds("CMPSelectINImap") + "|*.map;*.yrm;*.mpr";
            o.InitialDirectory = Application.StartupPath;
            if (DialogResult.OK == o.ShowDialog())
            {
                txbINIBPath.Text = o.FileName;
            }
        }

        private void btnRunCompare_Click(object sender, EventArgs e)
        {
            INIFile f1 = null, f2 = null;
            try
            {
                f1 = new INIFile(txbINIAPath.Text);
                f2 = new INIFile(txbINIBPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Trans.Ds("CMOSelectInvalidPath"), Trans.Ds("CMPTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
                                log.AddLog(entF1.Name, p1, p2, ckbCalculateR.Checked);
                            }
                            entF1.RemovePair(p1);
                        }
                        // ent1 dont have p2, this item was newly added
                        else
                        {
                            if (!ckbIgnoreNew.Checked)
                            {
                                log.AddLog(entF1.Name, INIPair.NullPair, p2);
                            }
                        }
                    }
                    // ent2 is empty now, what remains in ent1 is removed in current version
                    if (!ckbIgnoreRemoved.Checked)
                    {
                        foreach (INIPair p1 in entF1.DataList)
                        {
                            log.AddLog(entF1.Name, p1, INIPair.NullPair);
                        }
                    }
                }
                // f1 dont have ent2, this entity was newly added
                else
                {
                    if (!ckbIgnoreNew.Checked)
                    {
                        if (!Cons.EntName.SystemEntity.Contains(entF2.Name))
                        {
                            entF2.ConvPairs();
                        }
                        foreach (INIPair p2 in entF2.DataList)
                        {
                            log.AddLog(entF2.Name, INIPair.NullPair, p2);
                        }
                    }
                }
            }
            // ini2 is empty now, what remains in ini1 is removed in current version
            if (!ckbIgnoreRemoved.Checked)
            {
                foreach (INIEntity ent in f1.IniData)
                {
                    ent.ConvPairs();
                    foreach (INIPair p1 in ent.DataList)
                    {
                        log.AddLog(ent.Name, p1, INIPair.NullPair);
                    }
                }
            }
            log.RemoveEmpty();
            log.Translate(Trans);
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = Trans.Ds("CMPResultSaveTitle");
            dlg.FileName = "CompareResult.txt";
            dlg.Filter = Trans.Ds("CMPResultSaveText") + "|*.txt|" + Trans.Ds("CMPResultSaveCSV") + "|*.csv";
            dlg.AddExtension = true;
            dlg.InitialDirectory = Application.StartupPath;
            string result = ";Report Generated by Relert-Sharp INI Comparator.\n";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Utils.File f = new Utils.File(dlg.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                result += Trans.Ds("RTatic") + "\n" +
                    Trans.Ds(ckbIgnoreRemoved.Checked ? "RIgnore" : "RInclude") + Trans.Ds("RRemoved") + "\n" +
                    Trans.Ds(ckbIgnoreNew.Checked ? "RIgnore" : "RInclude") + Trans.Ds("RNew") + "\n" +
                    Trans.Ds(ckbCalculateR.Checked ? "RDo" : "RDont") + Trans.Ds("RCalc") + "\n";
                result += Trans.Ds("ROrg") + f1.FullName + "\n" +
                    Trans.Ds("RDst") + f2.FullName + "\n\n";
                foreach (string entName in log.DictEntry.Keys)
                {
                    result += Trans.Ds(entName) + ":\n";
                    foreach (DiffLog.LogEntry ent in log.DictEntry[entName])
                    {
                        result += "\t" + ent.Result + "\n";
                    }
                    result += "\n";
                }
                f.Write(result);
                f.Close();
                MessageBox.Show(Trans.Ds("CMPResultSaveSuccessText"), Trans.Ds("CMPTitle"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            Text = Trans.Ds(Text);
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
            public void AddLog(string entName, INIPair pOlder, INIPair pNewer, bool CalculateRelative = false)
            {
                if (!statedNames.Contains(entName))
                {
                    statedNames.Add(entName);
                    logs[entName] = new List<LogEntry>();
                }
                string pName = pOlder.Name;
                if (pName == "" || pName == null) pName = pNewer.Name;
                logs[entName].Add(new LogEntry(pName, pOlder.Value, pNewer.Value, language, CalculateRelative));
            }
            public void AddLog(INIEntity entOlder, INIEntity entNewer)
            {
                //unfinished
                //entOlder, entNewer is item list and has differences
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
                            if (Trans.Lg == Cons.Language.Chinese)
                            {
                                ent.Result += Trans.Ds(s);
                            }
                            else
                            {
                                ent.Result += Trans.Ds(s) + " ";
                            }
                        }
                    }
                    buffer[newkey] = logs[key];
                }
                logs = buffer;
            }
            public void RemoveEmpty()
            {
                Dictionary<string, List<LogEntry>> buffer = new Dictionary<string, List<LogEntry>>();
                List<string> namesBuffer = new List<string>();
                foreach (string name in statedNames)
                {
                    List<LogEntry> entryBuffer = new List<LogEntry>();
                    foreach (LogEntry entry in logs[name])
                    {
                        if (!entry.IsEmpty)
                        {
                            entryBuffer.Add(entry);
                        }
                    }
                    if (entryBuffer.Count != 0)
                    {
                        buffer[name] = entryBuffer;
                        namesBuffer.Add(name);
                    }
                }
                statedNames = namesBuffer;
                logs = buffer;
            }
            public class LogEntry
            {
                private string itemName;
                private List<string> describ;
                private string result = "";
                private bool isEmpty = false;
                private bool calcPercent;
                public LogEntry(string name, object older, object newer, Cons.Language language, bool needCalc = false)
                {
                    itemName = name;
                    calcPercent = needCalc;
                    if ((older == null || older.ToString() == "") &&
                        (newer == null || newer.ToString() == ""))
                    {
                        isEmpty = true;
                        return;
                    }
                    if (Cons.Interpreter.SightLike.Contains(itemName))
                    {
                        describ = SightLike(older, newer, language);
                        if (calcPercent)
                        {
                            double percent;
                            if (newer.GetType() == typeof(int))
                            {
                                percent = (double)((int)newer) / (double)(int)older * 100;
                            }
                            else
                            {
                                percent = (float)newer / (float)older * 100;
                            }
                            describ.Add("(" + percent.ToString("0.00") + "%)");
                        }
                    }
                    else if (Cons.Interpreter.ActiveBoolLike.Contains(itemName))
                    {
                        describ = ActiveBoolLike(older, newer, language);
                    }
                    else if (Cons.Interpreter.PassiveBoolLike.Contains(itemName))
                    {
                        describ = PassiveBoolLike(older, newer, language);
                    }
                    else if (Cons.Interpreter.AcquireBoolLike.Contains(itemName))
                    {
                        describ = AcquireBoolLike(older, newer, language);
                    }
                    else if (Cons.Interpreter.NameLike.Contains(itemName))
                    {
                        describ = NameLike(older, newer, language);
                    }
                    else
                    {
                        describ = DefaultStringLike(older, newer, language);
                        describ.Insert(0, "----");
                    }

                }
                public bool IsEmpty
                {
                    get { return isEmpty; }
                }
                private List<string> SightLike(object older, object newer, Cons.Language language)
                {
                    if (older == null || older.ToString() == "")
                    {
                        return new List<string>() { itemName, "SetAs", newer.ToString() };
                    }
                    else if (newer == null || newer.ToString() == "")
                    {
                        return new List<string>() { itemName, "SetAs", "DefaultValue" };
                    }
                    else
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
                                return new List<string>() { itemName, modify, "From", older.ToString(), "To", newer.ToString() };
                            case Cons.Language.Chinese:
                                return new List<string>() { itemName, "From", older.ToString(), modify, "To", newer.ToString() };
                            default:
                                return null;
                        }
                    }

                }
                private List<string> ActiveBoolLike(object older, object newer, Cons.Language language)
                {
                    if (older == null || older.ToString() == "")
                    {
                        string modify = "Wont";
                        if ((bool)newer)
                        {
                            modify = "Will";
                        }
                        return new List<string>() { "Now", modify, itemName, "(NewDefine)" };
                    }
                    else if (newer == null || newer.ToString() == "")
                    {
                        switch (language)
                        {
                            case Cons.Language.EnglishUS:
                                return new List<string>() { "Wont", "Define", "Whether", "Can", itemName, "OrNot", "(SetDefault)" };
                            case Cons.Language.Chinese:
                                return new List<string>() { "Wont", "Define", "Whether", "Can", itemName, "(SetDefault)" };
                            default:
                                return null;
                        }
                    }
                    else
                    {
                        string modify = "Unable";
                        if ((bool)newer)
                        {
                            modify = "Able";
                        }
                        return new List<string>() { "Now", modify, "To", itemName };
                    }
                }
                private List<string> PassiveBoolLike(object older, object newer, Cons.Language language)
                {
                    string modify = "Cant";
                    if (older == null || older.ToString() == "")
                    {
                        if ((bool)newer)
                        {
                            modify = "Can";
                        }
                        return new List<string>() { "Now", modify, ItemName, "(NewDefine)" };
                    }
                    else if (newer == null || newer.ToString() == "")
                    {
                        switch (language)
                        {
                            case Cons.Language.EnglishUS:
                                return new List<string>() { "Wont", "Define", "Whether", itemName, "OrNot", "(SetDefault)" };
                            case Cons.Language.Chinese:
                                return new List<string>() { "Wont", "Define", "Whether", itemName, "(SetDefault)" };
                            default:
                                return null;
                        }
                    }
                    else
                    {
                        if ((bool)newer)
                        {
                            modify = "Can";
                        }
                        return new List<string>() { "Now", modify, itemName };
                    }
                }
                private List<string> AcquireBoolLike(object older, object newer, Cons.Language language)
                {
                    string modify = "Lost";
                    if (older == null || older.ToString() == "")
                    {
                        if ((bool)newer)
                        {
                            modify = "Gain";
                        }
                        switch (language)
                        {
                            case Cons.Language.EnglishUS:
                                return new List<string>() { "Now", modify, "Abil", "Of", ItemName, "(NewDefine)" };
                            case Cons.Language.Chinese:
                                return new List<string>() { "Now", modify, ItemName, "Of", "Abil", "(NewDefine)" };
                            default:
                                return null;
                        }
                    }
                    else if (newer == null || newer.ToString() == "")
                    {
                        switch (language)
                        {
                            case Cons.Language.EnglishUS:
                                return new List<string>() { "Now", "Wont", "Define", "Whether", "Have", "Abil", "Of", itemName, "OrNot", "(SetDefault)" };
                            case Cons.Language.Chinese:
                                return new List<string>() { "Now", "Wont", "Define", "Whether", "Have", itemName, "Of", "Abil", "(SetDefault)" };
                            default:
                                return null;
                        }
                    }
                    else
                    {
                        if ((bool)newer)
                        {
                            modify = "Gain";
                        }
                        switch (language)
                        {
                            case Cons.Language.EnglishUS:
                                return new List<string>() { "Now", modify, "Abil", "Of", ItemName };
                            case Cons.Language.Chinese:
                                return new List<string>() { "Now", modify, ItemName, "Of", "Abil" };
                            default:
                                return null;
                        }
                    }
                }
                private List<string> NameLike(object older, object newer, Cons.Language language)
                {
                    if (older == null || older.ToString() == "")
                    {
                        return new List<string>() { "New", itemName, ":", "\""+newer.ToString()+"\"" };
                    }
                    else if (newer == null || newer.ToString() == "")
                    {
                        return new List<string>() { itemName, "Removed" };
                    }
                    else
                    {
                        switch (language)
                        {
                            case Cons.Language.EnglishUS:
                                return new List<string>() { itemName, "Changed", "From", "\""+older.ToString()+ "\"", "To", "\""+newer.ToString()+ "\"" };
                            case Cons.Language.Chinese:
                                return new List<string>() { itemName, "From", "\""+older.ToString()+ "\"", "Changed", "To", "\""+newer.ToString()+ "\"" };
                            default:
                                return null;
                        }
                    }
                }
                private List<string> DefaultStringLike(object older, object newer, Cons.Language language)
                {
                    if (older == null || older.ToString() == "")
                    {
                        return new List<string>() { "Attribute", itemName, "Now", "Define", "As", newer.ToString() };
                    }
                    else if (newer == null || newer.ToString() == "")
                    {
                        return new List<string>() { "Attribute", itemName, "Removed", "(SetDefault)" };
                    }
                    else
                    {
                        switch (language)
                        {
                            case Cons.Language.EnglishUS:
                                return new List<string>() { "Attribute", itemName, "Changed", "From", older.ToString(), "To", newer.ToString() };
                            case Cons.Language.Chinese:
                                return new List<string>() { "Attribute", itemName, "From", older.ToString(), "Changed", "To", newer.ToString() };
                            default:
                                return null;
                        }
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
            #region Public Calls
            public Dictionary<string, List<LogEntry>> DictEntry
            {
                get { return logs; }
            }
            public List<string> EntryNames
            {
                get { return statedNames; }
            }
            #endregion
        }
    }
}
