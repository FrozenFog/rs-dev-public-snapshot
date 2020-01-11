﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relert_sharp.FileSystem
{
    public class VxlFile : BaseFile
    {
        private byte[] pal;
        private List<VoxelSection> data = new List<VoxelSection>();


        #region Constructor - VxlFile
        public VxlFile(byte[] _data, string _fullName) : base(_data, _fullName)
        {
            FileExt = Common.FileExtension.VXL;
            Read();
            Dispose();
        }
        public VxlFile(string _path) : base(_path, System.IO.FileMode.Open, System.IO.FileAccess.Read, false)
        {
            FileExt = Common.FileExtension.VXL;
            Read();
            Dispose();
        }
        #endregion


        #region Private Methods - VxlFile
        private void Read()
        {
            //header
            ReadBytes(20);//"Voxel Animation ";int 1
            SectionCount = ReadInt32();
            ReadBytes(4);//same as above
            SectionBodySize = ReadUInt32();
            ReadBytes(2);//unknown
            pal = ReadBytes(768);

            for(int i = 0; i < SectionCount; i++)
            {
                //header
                VoxelSection sec = new VoxelSection(ReadString(16), ReadInt32());
                ReadBytes(8);//unknown, uint 1, uint 0
                data.Add(sec);
            }
            int __pos = GetPos();
            //go to tailer
            ReadSeek(SectionBodySize, System.IO.SeekOrigin.Current);
            foreach (VoxelSection sec in data)
            {
                ReadTailer(sec);
            }
            ReadSeek(__pos, System.IO.SeekOrigin.Begin);
            //and back
            //body
            foreach (VoxelSection sec in data)
            {
                ReadSection(sec);
            }
        }
        private void ReadSection(VoxelSection sec)
        {
            int _num = sec.Width * sec.Depth;
            sec.SpanStart = ReadInt32s(_num);
            sec.SpanEnd = ReadInt32s(_num);
            int _index = 0;
            for (int posY = 0; posY < sec.Depth; posY++)
            {
                for (int posX = 0; posX < sec.Width; posX++)
                {
                    if (sec.SpanStart[_index] == -1 || sec.SpanEnd[_index] == -1)
                    {
                        _index++;
                        continue;
                    }
                    ReadSpan(posX, posY, sec);
                    _index++;
                }
            }
        }
        private void ReadSpan(int _posx, int _posy, VoxelSection sec)
        {
            VoxelSpanSeg seg = new VoxelSpanSeg(_posx, _posy);
            for (int _posZ = 0; _posZ < sec.Height;)
            {
                byte _skipNum = ReadByte();
                byte _voxelNum = ReadByte();
                _posZ += _skipNum;
                for (int i = 0; i < _voxelNum; i++)
                {
                    VoxelUnit voxel = new VoxelUnit(ReadByte(), ReadByte());
                    seg.AddVoxel(voxel, _posZ);
                    _posZ++;
                }
                ReadByte();//repeat
            }
            sec.AddSpanSeg(seg);
        }
        private void ReadTailer(VoxelSection _sec)
        {
            _sec.OffsetSpanStart = ReadUInt32();
            _sec.OffsetSpanEnd = ReadUInt32();
            _sec.OffsetSpanData = ReadUInt32();
            _sec.Scale = ReadFloat();
            _sec.TransMatrix = ReadFloats(12);
            _sec.MinBounds = ReadFloats(3);
            _sec.MaxBounds = ReadFloats(3);
            _sec.Width = ReadByte();
            _sec.Depth = ReadByte();
            _sec.Height = ReadByte();
            _sec.NormalType = ReadByte();
        }
        #endregion


        #region Public Calls - VxlFile
        public int SectionCount { get; private set; }
        public uint SectionBodySize { get; private set; }
        #endregion
    }


    public class VoxelSection
    {
        private List<VoxelSpanSeg> data = new List<VoxelSpanSeg>();


        #region Constructor - VoxelSection
        public VoxelSection(string _name, int _index)
        {
            SectionName = _name;
            SectionIndex = _index;
        }
        #endregion


        #region Internal - VoxelSection
        internal void AddSpanSeg(VoxelSpanSeg seg)
        {
            data.Add(seg);
        }
        #endregion


        #region Public Calls - VoxelSection
        public string SectionName { get; private set; }
        public int SectionIndex { get; private set; }
        public uint OffsetSpanStart { get; internal set; }
        public uint OffsetSpanEnd { get; internal set; }
        public uint OffsetSpanData { get; internal set; }
        public float Scale { get; internal set; }
        public float[] TransMatrix { get; internal set; }
        public float[] MinBounds { get; internal set; }
        public float[] MaxBounds { get; internal set; }
        public byte Width { get; internal set; }
        public byte Depth { get; internal set; }
        public byte Height { get; internal set; }
        public byte NormalType { get; internal set; }
        public int[] SpanStart { get; internal set; }
        public int[] SpanEnd { get; internal set; }
        #endregion
    }


    public class VoxelSpanSeg
    {
        private List<VoxelUnit> data = new List<VoxelUnit>();
        #region Constructor - VoxelSpanSeg
        public VoxelSpanSeg(int _x, int _y)
        {
            SpanX = _x;
            SpanY = _y;
        }
        #endregion


        #region Internal - VoxelSpanSeg
        internal void AddVoxel(VoxelUnit vox, int _z)
        {
            vox.VoxX = SpanX;
            vox.VoxY = SpanY;
            vox.VoxZ = _z;
            data.Add(vox);
        }
        #endregion


        #region Public Call - VoxelSpanSeg
        public int SpanX { get; private set; }
        public int SpanY { get; private set; }
        #endregion
    }


    public class VoxelUnit
    {
        #region Constructor - VoxelUnit
        public VoxelUnit(byte _colorIndex, byte _normalVector)
        {
            ColorIndex = _colorIndex;
            NormalVector = _normalVector;
        }
        #endregion


        #region Public Calls - VoxelUnit
        public byte ColorIndex { get; set; }
        public byte NormalVector { get; set; }
        public int VoxX { get; internal set; }
        public int VoxY { get; internal set; }
        public int VoxZ { get; internal set; }
        #endregion
    }
}
