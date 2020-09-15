using System.Collections.Generic;

namespace RelertSharp.FileSystem
{
    public class HvaFile : BaseFile
    {
        private List<string> sectionNames = new List<string>();
        private List<HvaFrame> frames = new List<HvaFrame>();


        #region Ctor - HvaFile
        public HvaFile(byte[] _data, string _fullName) : base(_data, _fullName)
        {
            Read();
        }
        public HvaFile(string _path) : base(_path, System.IO.FileMode.Open, System.IO.FileAccess.Read, false)
        {
            Read();
        }
        #endregion


        #region Private Methods - HvaFile
        private void Read()
        {
            //header
            ReadBytes(16);//useless path
            FrameNum = ReadUInt32();
            SectionNum = ReadUInt32();
            for (int i = 0; i < SectionNum; i++)
            {
                sectionNames.Add(ReadString(16));
            }
            //frame
            for (int j = 0; j < FrameNum; j++)
            {
                HvaFrame f = new HvaFrame();
                for (int i = 0; i < SectionNum; i++)
                {
                    float[] mat = ReadFloats(12);
                    f.AddFrameMatrix(mat);
                }
                frames.Add(f);
            }
        }
        #endregion


        #region Public Calls - HvaFile
        public uint FrameNum { get; private set; }
        public uint SectionNum { get; private set; }
        #endregion
    }


    public class HvaFrame
    {
        private List<float[]> matrixs = new List<float[]>();


        #region Ctor - HvaFrame
        public HvaFrame() { }
        #endregion


        #region Internal - HvaFrame
        internal void AddFrameMatrix(float[] mat)
        {
            matrixs.Add(mat);
        }
        #endregion


        #region Public Calls - HvaFrame
        public float[] this[int _index] { get { return matrixs[_index]; } }
        #endregion
    }
}
