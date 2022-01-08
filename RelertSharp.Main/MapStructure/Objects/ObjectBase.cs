using RelertSharp.Common;
using RelertSharp.IniSystem.Serialization;
using RelertSharp.IniSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.MapStructure.Objects
{
    public abstract class ObjectBase<T> : IIniEntitySerializable, IEnumerable<T> where T : ObjectItemBase
    {
        private Dictionary<string, T> data = new Dictionary<string, T>();
        protected abstract T InvokeCtor();
        public ObjectBase() { }

        public virtual void ReadFromIni(INIEntity src)
        {
            foreach (var p in src)
            {
                var item = InvokeCtor();
                item.ReadFromIni(p);
                this[p.Name] = item;
            }
        }

        public virtual INIEntity SaveAsIni()
        {
            INIEntity ent = this.GetNamedEnt();
            foreach (T item in this)
            {
                ent.AddPair(item.SaveAsIni());
            }
            return ent;
        }
        #region Public Methods - ObjectBase
        public virtual void RemoveByID(string id)
        {
            if (data.Keys.Contains(id)) data.Remove(id);
        }
        /// <summary>
        /// Only use this to generate id
        /// </summary>
        /// <param name="item"></param>
        public virtual void AddItem(T item, bool forceRenewId = false)
        {
            if (item.Id != Constant.ITEM_NONE && !forceRenewId) data[item.Id] = item;
            else
            {
                for (int i = 0; i < 10000; i++)
                {
                    if (!data.Keys.Contains(i.ToString()))
                    {
                        item.Id = i.ToString();
                        data[item.Id] = item;
                        return;
                    }
                }
            }
        }
        internal virtual void Clear()
        {
            data.Clear();
        }
        #endregion


        #region Protected
        protected Dictionary<string, T> GetDictionary()
        {
            return data;
        }
        protected void RemoveItem(T item)
        {
            data.Remove(item.Id);
        }
        #endregion


        #region Public Calls - ObjectBase
        public T this[string _id]
        {
            get
            {
                if (data.Keys.Contains(_id)) return data[_id];
                return null;
            }
            set
            {
                data[_id] = value;
            }
        }
        #region Enumerator
        public IEnumerator<T> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }
        #endregion
        #endregion
    }


    public abstract class ObjectItemBase : BaseVisibleObject<ISceneObject>, IRegistable, ITaggableObject, IIniPairSerializable
    {


        #region Ctor
        protected ObjectItemBase(ObjectItemBase src)
        {
            Id = src.Id;
            Owner = src.Owner;
            RegName = src.RegName;
            HealthPoint = src.HealthPoint;
            X = src.X;
            Y = src.Y;
        }
        protected ObjectItemBase() { }


        protected void ReadFromIni(ParameterReader reader, string id)
        {
            Id = id;
            Owner = reader.ReadString();
            RegName = reader.ReadString();
            HealthPoint = reader.ReadInt();
            X = reader.ReadInt();
            Y = reader.ReadInt();
        }
        protected void SaveToWriter(ParameterWriter writer)
        {
            writer.Write(Id);
            writer.Write(Owner);
            writer.Write(RegName);
            writer.Write(HealthPoint);
            writer.Write(X);
            writer.Write(Y);
        }
        public abstract void ReadFromIni(INIPair src);

        public abstract INIPair SaveAsIni();
        #endregion


        #region Public Methods - ObjectItemBase
        public virtual void ApplyConfig(IMapObjectBrushConfig config, IObjectBrushFilter filter, bool applyPosAndName = false)
        {
            if (applyPosAndName)
            {
                RegName = config.RegName;
                X = config.Pos.X;
                Y = config.Pos.Y;
            }
            if (filter.Owner) Owner = config.Owner;
            if (filter.HealthPoint) HealthPoint = config.HealthPoint;
            if (filter.Group) Group = config.Group;
            if (filter.MissionStatus) Status = config.MissionStatus;
            if (filter.Tag) TagId = config.AttatchedTag;
            if (filter.Veteran) VeterancyPercentage = config.VeterancyPercentage;
            if (filter.Facing) Rotation = config.FacingRotation;
            if (filter.RecruitYes) AutoYESRecruitType = config.AutoRecruitYes;
            if (filter.RecruitNo) AutoNORecruitType = config.AutoRecruitNo;
            if (filter.AboveGround) IsAboveGround = config.AboveGround;
        }
        public override string ToString()
        {
            return string.Format("{0} at {1},{2}", RegName, X, Y);
        }

        public override void ChangeDisplay(IndexableDisplayType type)
        {
            // do nothing
        }
        #endregion


        #region Public Calls - ObjectItemBase
        [IniHeader]
        public override string Id { get; set; } = Constant.ITEM_NONE;
        [IniPairItem(1)]
        public string RegName { get; set; } = "(NOTHING)";
        [IniPairItem(0)]
        public string Owner { get; set; } = Constant.ITEM_NONE;
        /// <summary>
        /// Default: 256
        /// </summary>
        [IniPairItem(2)]
        public int HealthPoint { get; set; } = 256;
        [IniPairItem(6)]
        public virtual string Status { get; set; } = Constant.ITEM_NONE;
        [IniPairItem(7)]
        public virtual string TagId { get; set; } = Constant.VALUE_NONE;
        [IniPairItem(3)]
        public override int X { get; set; } = 0;
        [IniPairItem(4)]
        public override int Y { get; set; } = 0;
        [IniPairItem(5)]
        public virtual int Rotation { get; set; } = 0;
        /// <summary>
        /// Default: 100
        /// </summary>
        [IniPairItem(8)]
        public virtual int VeterancyPercentage { get; set; } = 100;
        /// <summary>
        /// Default: -1
        /// </summary>
        [IniPairItem(9)]
        public virtual string Group { get; set; } = Constant.ID_INVALID;
        public virtual bool IsAboveGround { get; set; } = false;
        public virtual bool AutoNORecruitType { get; set; } = false;
        /// <summary>
        /// Default: true
        /// </summary>
        public virtual bool AutoYESRecruitType { get; set; } = true;
        public override string Name
        {
            get
            {
                if (GlobalVar.GlobalRules != null) return GlobalVar.GlobalRules[RegName].GetString(Constant.KEY_NAME);
                else return string.Empty;
            }
            set { }
        }

        public override string Value { get { return RegName; } }
        #endregion
    }

    public sealed class VirtualMapObject : ObjectItemBase, IMapObject
    {
        public IMapObject ConstructFromParameter(string[] commands)
        {
            return new VirtualMapObject();
        }

        public string[] ExtractParameter()
        {
            return new string[0];
        }
        public int GetChecksum()
        {
            return 0;
        }

        /// <summary>
        /// do nothing
        /// </summary>
        /// <param name="src"></param>
        public override void ReadFromIni(INIPair src)
        {
            
        }

        /// <summary>
        /// return null
        /// </summary>
        /// <returns></returns>
        public override INIPair SaveAsIni()
        {
            return null;
        }
    }
}
