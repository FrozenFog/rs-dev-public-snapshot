using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.MapStructure.Logic
{
    public abstract class IndexableItemCollection<TItem> : IGlobalIdContainer, IEnumerable<TItem> where TItem : IndexableItem
    {
        protected Dictionary<string, TItem> data = new Dictionary<string, TItem>();

        public IEnumerable<string> AllId { get { return data.Keys; } }

        public int Length { get { return data.Count; } }

        public virtual void AscendingSort()
        {
            data = data.OrderBy(x => x.Value.ToString()).ToDictionary(x => x.Key, x => x.Value);
        }

        public void ChangeDisplay(IndexableDisplayType type)
        {
            foreach (TItem item in data.Values)
            {
                item.ChangeDisplay(type);
            }
        }

        public void DescendingSort()
        {
            data = data.OrderByDescending(x => x.Value.ToString()).ToDictionary(x => x.Key, x => x.Value);
        }

        public bool HasId(string id)
        {
            return data.ContainsKey(id);
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        public TItem this[string _id]
        {
            get
            {
                if (!string.IsNullOrEmpty(_id) && HasId(_id)) return data[_id];
                return null;
            }
            set
            {
                data[_id] = value;
            }
        }
    }




    public abstract class IndexableItem : IIndexableItem
    {
        private IndexableDisplayType displayType = IndexableDisplayType.IdAndName;
        private const string format = "{0} - {1}";

        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public override string ToString()
        {
            if (displayType == IndexableDisplayType.IdAndName) return string.Format(format, Id, Name);
            else return Name;
        }
        public void ChangeDisplay(IndexableDisplayType type)
        {
            displayType = type;
        }
    }
}
