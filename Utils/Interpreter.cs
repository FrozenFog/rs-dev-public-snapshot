using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relert_sharp.Utils
{
    public class Interpreter
    {
        protected List<string> SightLike(object older, object newer, string itemName)
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
                switch (Constant.CurrentLanguage)
                {
                    case Constant.Language.EnglishUS:
                        return new List<string>() { itemName, modify, "From", older.ToString(), "To", newer.ToString() };
                    case Constant.Language.Chinese:
                        return new List<string>() { itemName, "From", older.ToString(), modify, "To", newer.ToString() };
                    default:
                        return null;
                }
            }

        }
        protected List<string> ActiveBoolLike(object older, object newer, string itemName)
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
                switch (Constant.CurrentLanguage)
                {
                    case Constant.Language.EnglishUS:
                        return new List<string>() { "Wont", "Define", "Whether", "Can", itemName, "OrNot", "(SetDefault)" };
                    case Constant.Language.Chinese:
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
        protected List<string> PassiveBoolLike(object older, object newer, string itemName)
        {
            string modify = "Cant";
            if (older == null || older.ToString() == "")
            {
                if ((bool)newer)
                {
                    modify = "Can";
                }
                return new List<string>() { "Now", modify, itemName, "(NewDefine)" };
            }
            else if (newer == null || newer.ToString() == "")
            {
                switch (Constant.CurrentLanguage)
                {
                    case Constant.Language.EnglishUS:
                        return new List<string>() { "Wont", "Define", "Whether", itemName, "OrNot", "(SetDefault)" };
                    case Constant.Language.Chinese:
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
        protected List<string> AcquireBoolLike(object older, object newer, string itemName)
        {
            string modify = "Lost";
            if (older == null || older.ToString() == "")
            {
                if ((bool)newer)
                {
                    modify = "Gain";
                }
                switch (Constant.CurrentLanguage)
                {
                    case Constant.Language.EnglishUS:
                        return new List<string>() { "Now", modify, "Abil", "Of", itemName, "(NewDefine)" };
                    case Constant.Language.Chinese:
                        return new List<string>() { "Now", modify, itemName, "Of", "Abil", "(NewDefine)" };
                    default:
                        return null;
                }
            }
            else if (newer == null || newer.ToString() == "")
            {
                switch (Constant.CurrentLanguage)
                {
                    case Constant.Language.EnglishUS:
                        return new List<string>() { "Now", "Wont", "Define", "Whether", "Have", "Abil", "Of", itemName, "OrNot", "(SetDefault)" };
                    case Constant.Language.Chinese:
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
                switch (Constant.CurrentLanguage)
                {
                    case Constant.Language.EnglishUS:
                        return new List<string>() { "Now", modify, "Abil", "Of", itemName };
                    case Constant.Language.Chinese:
                        return new List<string>() { "Now", modify, itemName, "Of", "Abil" };
                    default:
                        return null;
                }
            }
        }
        protected List<string> NameLike(object older, object newer, string itemName)
        {
            if (older == null || older.ToString() == "")
            {
                return new List<string>() { "New", itemName, ":", "\"" + newer.ToString() + "\"" };
            }
            else if (newer == null || newer.ToString() == "")
            {
                return new List<string>() { itemName, "Removed" };
            }
            else
            {
                switch (Constant.CurrentLanguage)
                {
                    case Constant.Language.EnglishUS:
                        return new List<string>() { itemName, "Changed", "From", "\"" + older.ToString() + "\"", "To", "\"" + newer.ToString() + "\"" };
                    case Constant.Language.Chinese:
                        return new List<string>() { itemName, "From", "\"" + older.ToString() + "\"", "Changed", "To", "\"" + newer.ToString() + "\"" };
                    default:
                        return null;
                }
            }
        }
        protected List<string> NameListLike(object older, object newer, string itemName)
        {
            List<string> sl1 = Misc.Trim(older.ToString().Split(new char[] { ',' }));
            List<string> sl2 = Misc.Trim(newer.ToString().Split(new char[] { ',' }));
            List<string> newitems = new List<string>();
            List<string> removeditems = new List<string>();
            foreach (string s in sl2)
            {
                if (s == "") break;
                //if both
                if (sl1.Contains(s))
                {
                    sl1.Remove(s);
                    continue;
                }
                //if not, item is new
                else
                {
                    newitems.Add(s);
                    continue;
                }
            }
            removeditems = sl1;
            string resultRemoved = Misc.Join(removeditems, "\",\"");
            string resultNew = Misc.Join(newitems, "\",\"");
            List<string> result = new List<string>() { itemName, ":\r\n\t\t\t-" };
            if (resultRemoved != "")
            {
                result = result.Concat(new List<string>() { "ItemsRemoved", ":\"" + resultRemoved + "\"" }).ToList();
                if (resultNew != "") result.Add("\r\n\t\t\t-");
            }
            if (resultNew != "")
            {
                result = result.Concat(new List<string>() { "NewItems", ":\"" + resultNew + "\"" }).ToList();
            }
            return result;
        }
        protected List<string> VersesListLike(object older, object newer, string itemName)
        {
            List<string> sl1 = Misc.Trim(older.ToString().Split(new char[] { ',' }));
            List<string> sl2 = Misc.Trim(newer.ToString().Split(new char[] { ',' }));
            if (older.ToString() == "") sl1 = new List<string>() { "", "", "", "", "", "", "", "", "", "", "" };
            if (newer.ToString() == "") sl2 = new List<string>() { "", "", "", "", "", "", "", "", "", "", "" };
            List<string> result = new List<string>() { itemName, ":" };
            for (int i = 0; i < 10; i++)
            {
                if (sl1[i] != sl2[i])
                {
                    result.Add("\r\n\t\t\t-");
                    result = result.Concat(VersusLike(sl1[i], sl2[i], Constant.GenericArmorType[i])).ToList();
                }
            }
            return result;
        }
        protected List<string> VersusLike(string older, string newer, string itemName)
        {
            if (itemName.Contains(".")) itemName = itemName.Split(new char[] { '.' })[1];
            if (older == "")
            {
                switch (Constant.CurrentLanguage)
                {
                    case Constant.Language.EnglishUS:
                        return new List<string>() { "DamageMulti", "Against", itemName, "SetAs", newer };
                    case Constant.Language.Chinese:
                        return new List<string>() { "Against", itemName, "Of", "DamageMulti", "SetAs", newer };
                    default:
                        return null;
                }
            }
            if (newer == "")
            {
                switch (Constant.CurrentLanguage)
                {
                    case Constant.Language.EnglishUS:
                        return new List<string>() { "DamageMulti", "Against", itemName, "SetAs", "DefaultValue" };
                    case Constant.Language.Chinese:
                        return new List<string>() { "Against", itemName, "Of", "DamageMulti", "SetAs", "DefaultValue" };
                }
            }
            int versusOld = int.Parse(older.Replace("%", string.Empty));
            int versusNew = int.Parse(newer.Replace("%", string.Empty));
            string modify = "Increased";
            if (versusNew < versusOld) modify = "Decreased";
            switch (Constant.CurrentLanguage)
            {
                case Constant.Language.EnglishUS:
                    return new List<string>() { "DamageMulti", "Against", itemName, modify, "From", older, "To", newer };
                case Constant.Language.Chinese:
                    return new List<string>() { "Against", itemName, "Of", "DamageMulti", modify, "From", older, "To", newer };
                default:
                    return null;
            }
        }
        protected List<string> DefaultStringLike(object older, object newer, string itemName)
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
                switch (Constant.CurrentLanguage)
                {
                    case Constant.Language.EnglishUS:
                        return new List<string>() { "Attribute", itemName, "Changed", "From", older.ToString(), "To", newer.ToString() };
                    case Constant.Language.Chinese:
                        return new List<string>() { "Attribute", itemName, "From", older.ToString(), "Changed", "To", newer.ToString() };
                    default:
                        return null;
                }
            }
        }
    }
}
