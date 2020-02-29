using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using relert_sharp.Common;

namespace relert_sharp.FileSystem
{
    public class IdxBagFile
    {
        private BinaryReader bagReader;

        #region Constructor - IdxBagFile
        public IdxBagFile() { }
        public IdxBagFile(IdxIndex index, byte[] bagfile)
        {
            Index = index;
            bagReader = new BinaryReader(new MemoryStream(bagfile));
        }
        #endregion


        #region Private Methods - IdxBagFile
        private void ReadIndex(byte[] _data)
        {
            BinaryReader br = new BinaryReader(new MemoryStream(_data));
            if (br.ReadInt32() != 0x41424147) throw new RSException.InvalidFileException.InvalidIdx();
            br.ReadInt32();//2
            int num = br.ReadInt32();
            for (; num > 0; num--)
            {
                IdxIndex.IdxItem item = new IdxIndex.IdxItem();
                string name = System.Text.Encoding.ASCII.GetString(br.ReadBytes(16));
                int nameEnd = name.IndexOf('\0');
                item.Name = name.Substring(0, nameEnd);
                item.Offset = br.ReadUInt32();
                item.Length = br.ReadUInt32();
                item.SampleRate = br.ReadUInt32();
                item.Flag = br.ReadUInt32();
                item.ChunkSize = br.ReadUInt32();
                Index.Items[item.Name] = item;
            }
        }
        #endregion


        #region Public Methods - IdxBagFile
        public void Load(byte[] _idxData)
        {
            ReadIndex(_idxData);
        }
        public void LoadBag(byte[] _bagData)
        {
            MemoryStream ms = new MemoryStream(_bagData);
            bagReader = new BinaryReader(ms);
        }
        public AudFile ReadAudFile(string _filename)
        {
            if (!Index.Keys.Contains(_filename)) return new AudFile(_filename, 0);
            IdxIndex.IdxItem target = Index[_filename];
            bagReader.BaseStream.Seek(target.Offset, SeekOrigin.Begin);
            return new AudFile(bagReader.ReadBytes((int)target.Length), _filename + ".aud", target);
        }
        #endregion


        #region Public Calls - IdxBagFile
        public IdxIndex Index { get; set; } = new IdxIndex();
        #endregion
    }


    public class IdxIndex
    {
        private Dictionary<string, IdxItem> items;
        #region Constructor - IdxIndex
        public IdxIndex()
        {
            items = new Dictionary<string, IdxItem>();
        }
        #endregion


        #region Private Methods - IdxIndex
        #endregion


        #region Protected - IdxIndex
        #endregion


        #region Public Methods - IdxIndex
        public bool HasFile(string name)
        {
            return items.Keys.Contains(name);
        }
        #endregion


        #region Public Calls - IdxIndex
        public Dictionary<string, IdxItem> Items { get { return items; } set { items = value; } }
        public IdxItem this[string _filename] { get { return items[_filename]; } }
        public Dictionary<string, IdxItem>.KeyCollection Keys { get { return items.Keys; } }
        #endregion


        public struct IdxItem
        {
            public string Name;
            public uint Offset;
            public uint Length;
            public uint SampleRate;
            public uint Flag;
            public uint ChunkSize;
        }
    }
}
