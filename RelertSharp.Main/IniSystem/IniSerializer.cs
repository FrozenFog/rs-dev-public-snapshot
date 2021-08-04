using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace RelertSharp.IniSystem.Serialization
{
    using RelertSharp.IniSystem.Serialization.Helper;
    using System.Runtime.CompilerServices;

    public class IniEntitySerializer
    {
        private Type targetType;
        private IniAttributeInfo info;
        public IniEntitySerializer(Type type)
        {
            targetType = type;
            info = IniSerializerHelper.DumpTypePropertyInfo(targetType);
        }

        #region Main
        public INIEntity Serialize(object obj)
        {
            string header = targetType.Name;
            if (info.HasHeaderOnClass)
            {
                header = info.ClassHeader.HeaderName ?? targetType.Name;
            }
            else if (info.HeaderProp != null)
            {
                header = info.HeaderProp.Info.GetValue(obj).ToString();
            }
            INIEntity result = new INIEntity(header);
            foreach (var prop in targetType.GetProperties())
            {
                INIPair p = IniSerializerHelper.DumpAsPair(prop, obj);
                if (p != null) result.AddPair(p);
            }
            return result;
        }
        public void Deserialize(INIEntity src, object dest, bool removeRead = true)
        {
            List<INIPair> read = new List<INIPair>();
            bool readHead = false;
            foreach (INIPair p in src)
            {
                var propInfo = info.PairProp.Find(x => x.Attribute.PairName == p.Name);
                if (!readHead)
                {
                    info.HeaderProp.Info.SetValue(dest, src.Name);
                    readHead = true;
                }
                if (propInfo == null) continue;
                object value;
                if (propInfo.Info.PropertyType == typeof(bool))
                {
                    value = IniSerializerHelper.ParseBool(propInfo.Attribute.BoolCast, p.Value);
                }
                else if (propInfo.Info.PropertyType == typeof(string))
                {
                    value = p.Value;
                }
                else value = Convert.ChangeType(p.Value, propInfo.Info.PropertyType);
                propInfo.Info.SetValue(dest, value);
                read.Add(p);
            }
            if (removeRead)
            {
                read.ForEach(x => src.PopPair(x.Name));
            }
        }
        #endregion
    }
    public class IniPairSerializer
    {
        private Type targetType;
        private IniAttributeInfo info;
        public IniPairSerializer(Type type)
        {
            targetType = type;
            info = IniSerializerHelper.DumpTypePropertyInfo(targetType);
        }


        #region Main
        public INIPair Serialize(object obj)
        {
            string header = targetType.Name;
            if (info.HasHeaderOnClass)
            {
                header = info.ClassHeader.HeaderName ?? targetType.Name;
            }
            else if (info.HeaderProp != null)
            {
                header = info.HeaderProp.Info.GetValue(obj).ToString();
            }
            INIPair result = new INIPair(header);
            List<IniPairArrayItem> items = new List<IniPairArrayItem>();
            foreach (var prop in targetType.GetProperties())
            {
                IniPairArrayItem item = IniSerializerHelper.DumpAsString(prop, obj);
                if (item != null) items.Add(item);
            }
            IEnumerable<string> value = items.OrderBy(x => x.Position).Select(x => x.Value);
            result.Value = value.JoinBy();
            return result;
        }
        public void Deserialize(INIPair src, object dest)
        {
            char separator = info.ClassHeader.Separator;
            string[] arr = src.Value.Split(separator);
            for (int i = 0; i < arr.Length; i++)
            {
                var target = info.ArrayItemProp.Find(x => x.Attribute.Position == i);
                object value;
                if (target.Info.PropertyType == typeof(bool))
                {
                    value = IniSerializerHelper.ParseBool(target.Attribute.BoolCast, arr[i]);
                }
                else if (target.Info.PropertyType == typeof(string))
                {
                    value = arr[i];
                }
                else value = Convert.ChangeType(arr[i], target.Info.PropertyType);
                target.Info.SetValue(dest, value);
            }
        }
        #endregion
    }


    #region Attributes
    public class IniPairAttribute : Attribute
    {
        public string PairName { get; set; }
        public IniBoolCastType BoolCast { get; set; }
        public IniPairAttribute([CallerMemberName]string name = null, IniBoolCastType cast = IniBoolCastType.TrueFalse)
        {
            PairName = name;
            BoolCast = cast;
        }
    }
    public class IniHeaderAttribute : Attribute
    {
        public string HeaderName { get; set; }
        public char Separator { get; set; }
        public IniHeaderAttribute(string header = null, char separator = ',')
        {
            HeaderName = header;
            Separator = separator;
        }
    }
    public class IniArrayAttribute : Attribute
    {
        public string PairName { get; set; }
        public string SeparatorString { get; set; }
        public IniArrayAttribute([CallerMemberName]string name = null, string separator = ",")
        {
            PairName = name;
            SeparatorString = separator;
        }
    }
    public class IniPairItemAttribute : Attribute
    {
        public int Position { get; set; }
        public IniBoolCastType BoolCast { get; set; }
        public IniPairItemAttribute(int position = 0, IniBoolCastType cast = IniBoolCastType.TrueFalse)
        {
            Position = position;
            BoolCast = cast;
        }
    }
    public enum IniBoolCastType
    {
        None = 0,
        TrueFalse,
        YesNo,
        ZeroOne
    }
    #endregion
}


namespace RelertSharp.IniSystem.Serialization.Helper
{
    using RelertSharp.IniSystem.Serialization;
    internal static class IniSerializerHelper
    {
        public static string CastBool(IniBoolCastType type, object value)
        {
            if (value.GetType() == typeof(bool))
            {
                bool b = (bool)value;
                switch (type)
                {
                    case IniBoolCastType.TrueFalse:
                        return b.ToString();
                    case IniBoolCastType.YesNo:
                        return b.YesNo();
                    case IniBoolCastType.ZeroOne:
                        return b.ZeroOne();
                    case IniBoolCastType.None:
                        return value.ToString();
                }
                return null;
            }
            else return value.ToString();
        }
        public static bool ParseBool(IniBoolCastType type, string value)
        {
            return Utils.Misc.IniParseBool(value);
        }
        public static PropertyInfo GetProperty<TAttribute>(Type targetType) where TAttribute : Attribute
        {
            foreach (var prop in targetType.GetProperties())
            {
                if (prop.GetCustomAttribute<TAttribute>() is TAttribute) return prop;
            }
            return null;
        }
        public static IniAttributeInfo DumpTypePropertyInfo(Type targetType)
        {
            IniAttributeInfo info = new IniAttributeInfo();
            if (targetType.GetCustomAttribute<IniHeaderAttribute>() is IniHeaderAttribute head)
            {
                info.HasHeaderOnClass = true;
                info.ClassHeader = head;
            }
            foreach (var prop in targetType.GetProperties())
            {
                if (prop.GetCustomAttribute<IniHeaderAttribute>() is IniHeaderAttribute attrHead)
                {
                    info.HeaderProp = new AttributePropertyInfo<IniHeaderAttribute>()
                    {
                        Attribute = attrHead,
                        Info = prop
                    };
                }
                else if (prop.GetCustomAttribute<IniPairItemAttribute>() is IniPairItemAttribute attr)
                {
                    info.ArrayItemProp.Add(new AttributePropertyInfo<IniPairItemAttribute>()
                    {
                        Attribute = attr,
                        Info = prop
                    });
                }
                else if (prop.GetCustomAttribute<IniPairAttribute>() is IniPairAttribute aPair)
                {
                    info.PairProp.Add(new AttributePropertyInfo<IniPairAttribute>()
                    {
                        Attribute = aPair,
                        Info = prop
                    });
                }
            }
            return info;
        }

        public static INIPair DumpAsPair(PropertyInfo prop, object src)
        {
            if (prop.GetCustomAttribute<IniPairAttribute>() is IniPairAttribute attrPair)
            {
                string name = attrPair.PairName ?? prop.Name;
                INIPair p = new INIPair(name);
                object value = prop.GetValue(src);
                p.Value = CastBool(attrPair.BoolCast, value);
                return p;
            }
            else if (prop.GetCustomAttribute<IniArrayAttribute>() is IniArrayAttribute attrArray)
            {
                string arrayName = attrArray.PairName ?? prop.Name;
                if (prop.GetValue(src) is IEnumerable<object> enumerator)
                {
                    INIPair p = new INIPair(arrayName);
                    p.Value = enumerator.JoinBy(attrArray.SeparatorString);
                    return p;
                }
                return null;
            }
            return null;
        }
        public static IniPairArrayItem DumpAsString(PropertyInfo prop, object src)
        {
            if (prop.GetCustomAttribute<IniPairItemAttribute>() is IniPairItemAttribute attr)
            {
                string value = CastBool(attr.BoolCast, prop.GetValue(src));
                return new IniPairArrayItem()
                {
                    Position = attr.Position,
                    Value = value
                };
            }
            return null;
        }
    }




    #region Components
    internal class AttributePropertyInfo<TAttribute> where TAttribute : Attribute
    {
        public TAttribute Attribute { get; set; }
        public PropertyInfo Info { get; set; }
    }
    internal class IniPairArray
    {
        public List<IniPairArrayItem> Items { get; private set; } = new List<IniPairArrayItem>();
        public string Name { get; private set; }

        public IniPairArray(string name)
        {
            Name = name;
        }
    }
    internal class IniPairArrayItem
    {
        public int Position { get; set; }
        public string Value { get; set; }
    }
    internal class IniAttributeInfo
    {
        public AttributePropertyInfo<IniHeaderAttribute> HeaderProp { get; set; }
        public bool HasHeaderOnClass { get; set; }
        public IniHeaderAttribute ClassHeader { get; set; }
        public List<AttributePropertyInfo<IniPairItemAttribute>> ArrayItemProp { get; private set; } = new List<AttributePropertyInfo<IniPairItemAttribute>>();
        public List<AttributePropertyInfo<IniPairAttribute>> PairProp { get; private set; } = new List<AttributePropertyInfo<IniPairAttribute>>();
    }
    #endregion
}