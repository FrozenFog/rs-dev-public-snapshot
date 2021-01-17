using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.MapStructure.Logic
{
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
