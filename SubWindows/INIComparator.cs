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
        private Lang TRANS = new Lang(Constant.Trans);
        public INIComparator()
        {
            InitializeComponent();
            Set_Language();
        }

        private void btnSelectPath1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Title = TRANS.K("CMPSelectINITitle");
            o.Filter = TRANS.K("CMPSelectINIini") + "| *.ini|" + TRANS.K("CMPSelectINImap") + "|*.map;*.yrm;*.mpr";
            o.InitialDirectory = Application.StartupPath;
            if (DialogResult.OK == o.ShowDialog())
            {
                txbINIAPath.Text = o.FileName;
            }
        }

        private void btnSelectPath2_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Title = TRANS.K("CMPSelectINITitle");
            o.Filter = TRANS.K("CMPSelectINIini") + "| *.ini|" + TRANS.K("CMPSelectINImap") + "|*.map;*.yrm;*.mpr";
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
            catch (Exception)
            {
                MessageBox.Show(TRANS.K("CMOSelectInvalidPath"), TRANS.K("CMPTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DiffLog log = new DiffLog();
            foreach (INIEntity entF2 in f2.IniData)
            {
                // if it is just list, compare with list method then next
                if (Constant.EntName.DictionaryList.Contains(entF2.Name))
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
                    if (!(Constant.EntName.SystemEntity.Contains(entF2.Name) || Constant.EntName.DictionaryList.Contains(entF2.Name)))
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
                        if (!Constant.EntName.SystemEntity.Contains(entF2.Name))
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
            log.Translate(TRANS);
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = TRANS.K("CMPResultSaveTitle");
            dlg.FileName = "CompareResult.txt";
            dlg.Filter = TRANS.K("CMPResultSaveText") + "|*.txt|" + TRANS.K("CMPResultSaveCSV") + "|*.csv";
            dlg.AddExtension = true;
            dlg.InitialDirectory = Application.StartupPath;
            string result = ";Report Generated by Relert-Sharp INI Comparator.\n";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Utils.File f = new Utils.File(dlg.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                result += TRANS.K("RTatic") + "\n" +
                    TRANS.K(ckbIgnoreRemoved.Checked ? "RIgnore" : "RInclude") + TRANS.K("RRemoved") + "\n" +
                    TRANS.K(ckbIgnoreNew.Checked ? "RIgnore" : "RInclude") + TRANS.K("RNew") + "\n" +
                    TRANS.K(ckbCalculateR.Checked ? "RDo" : "RDont") + TRANS.K("RCalc") + "\n";
                result += TRANS.K("ROrg") + f1.FullName + "\n" +
                    TRANS.K("RKt") + f2.FullName + "\n\n";
                foreach (string entName in log.DictEntry.Keys)
                {
                    result += TRANS.K(entName) + ":\n";
                    foreach (DiffLog.LogEntry ent in log.DictEntry[entName])
                    {
                        result += "\t" + ent.Result + "\n";
                    }
                    result += "\n";
                }
                f.Write(result);
                f.Close();
                MessageBox.Show(TRANS.K("CMPResultSaveSuccessText"), TRANS.K("CMPTitle"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        private void Set_Language()
        {
            foreach(Control c in Controls)
            {
                if(c.GetType() != typeof(TextBox))
                {
                    c.Text = TRANS.K(c.Text);
                }
            }
            Text = TRANS.K(Text);
        }
        public class DiffLog
        {
            private List<string> statedNames = new List<string>();
            private Dictionary<string, List<LogEntry>> logs = new Dictionary<string, List<LogEntry>>();
            public DiffLog()
            {

            }
            public void AddLog(string entName, INIPair pOlder, INIPair pNewer, bool CalculateRelative = false)
            {
                if (!statedNames.Contains(entName))
                {
                    statedNames.Add(entName);
                    logs[entName] = new List<LogEntry>();
                }
                string pName = Misc.GetNonNull(pOlder.Name, pNewer.Name);
                logs[entName].Add(new LogEntry(pName, pOlder, pNewer, CalculateRelative));
            }
            public void AddLog(INIEntity entOlder, INIEntity entNewer)
            {
                //unfinished
                //entOlder, entNewer is item list and has differences
            }
            public void Translate(Lang TRANS)
            {
                Dictionary<string, List<LogEntry>> buffer = new Dictionary<string, List<LogEntry>>();
                foreach (string key in logs.Keys)
                {
                    string newkey = TRANS.K(key);
                    foreach (LogEntry ent in logs[key])
                    {
                        foreach (string s in ent.Description)
                        {
                            if (Constant.CurrentLanguage == Constant.Language.Chinese)
                            {
                                ent.Result += TRANS.K(s);
                            }
                            else
                            {
                                ent.Result += TRANS.K(s) + " ";
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
            public class LogEntry : Interpreter
            {
                private string itemName;
                private List<string> describ;
                private string result = "";
                private bool isEmpty = false;
                private bool calcPercent;
                private Constant.Interpreter.ChangeStatus changeStatus;
                public LogEntry(string name, INIPair polder, INIPair pnewer, bool needCalc = false)
                {
                    itemName = name;
                    calcPercent = needCalc;
                    object older = polder.Value;
                    object newer = pnewer.Value;
                    if (Misc.GetNonNull(older, newer) == null)
                    {
                        isEmpty = true;
                        return;
                    }
                    if (older == null || older.ToString() == "") changeStatus = Constant.Interpreter.ChangeStatus.New;
                    else if (newer == null || newer.ToString() == "") changeStatus = Constant.Interpreter.ChangeStatus.Removed;
                    else changeStatus = Constant.Interpreter.ChangeStatus.Changed;
                    Constant.INIKeyType keytype = Misc.GetNonNull(polder.KeyType, pnewer.KeyType);
                    switch (keytype)
                    {
                        case Constant.INIKeyType.SightLike:
                            describ = SightLike(older, newer, itemName);
                            if (calcPercent)
                            {
                                double percent;
                                if (newer.GetType() == typeof(int))
                                {
                                    percent = (int)newer / (double)(int)older * 100;
                                }
                                else
                                {
                                    percent = (float)newer / (float)older * 100;
                                }
                                describ.Add("(" + percent.ToString("0.00") + "%)");
                            }
                            break;
                        case Constant.INIKeyType.ActiveLike:
                            describ = ActiveBoolLike(older, newer, itemName);
                            break;
                        case Constant.INIKeyType.PassiveLike:
                            describ = PassiveBoolLike(older, newer, itemName);
                            break;
                        case Constant.INIKeyType.AcquireLike:
                            describ = AcquireBoolLike(older, newer, itemName);
                            break;
                        case Constant.INIKeyType.NameLike:
                        case Constant.INIKeyType.Armor:
                            describ = NameLike(older, newer, itemName);
                            break;
                        case Constant.INIKeyType.NameListLike:
                            describ = NameListLike(older, newer, itemName);
                            break;
                        case Constant.INIKeyType.NumListLike:
                            describ = NameLike(older, newer, itemName);
                            break;
                        case Constant.INIKeyType.VersesListLike:
                            describ = VersesListLike(older, newer, itemName);
                            break;
                        case Constant.INIKeyType.VersusLike:
                            describ = VersusLike(older.ToString(), newer.ToString(), itemName);
                            break;
                        case Constant.INIKeyType.MultiplierLike:
                            describ = SightLike(older, newer, itemName);
                            break;
                        default:
                            describ = DefaultStringLike(older, newer, itemName);
                            describ.Insert(0, "----");
                            break;
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
                public bool IsEmpty
                {
                    get { return isEmpty; }
                }
                public Constant.Interpreter.ChangeStatus ChangeStatus
                {
                    get { return changeStatus; }
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
