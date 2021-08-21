using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RelertSharp.Common.Config.Model
{
    public enum CliffJointPosition
    {
        None = 0,
        Top = 1,
        TopRight = 2,
        Right = 3,
        DownRight = 4,
        Down = 5,
        DownLeft = 6,
        Left = 7,
        TopLelt = 8
    }

    public class CliffSection
    {
        [XmlAttribute("begin")]
        public string BeginPositionString { get; set; }
        [XmlAttribute("end")]
        public string EndPositionString { get; set; }
        [XmlAttribute("offset")]
        public string OffsetString { get; set; }
        [XmlAttribute("bend")]
        public int BendTypeValue { get; set; }
        [XmlAttribute("set")]
        public int SetIndex { get; set; }
        [XmlAttribute("sub")]
        public int SubIndex { get; set; }
        [XmlElement("Follow")]
        public CliffSection FollowBy { get; set; }
        [XmlAttribute("succ")]
        public int SuccessorType { get; set; }
        //[XmlArrayItem("sec")]
        //public List<CliffSection> Allow { get; set; }


        [XmlIgnore]
        public bool IsCorner { get { return BendTypeValue != 0; } }
        [XmlIgnore]
        public Pnt Offset
        {
            get
            {
                if (!bOff)
                {
                    if (OffsetString.IsNullOrEmpty()) offset = new Pnt();
                    else offset = new Pnt(OffsetString);
                    bOff = true;
                }
                return offset;
            }
            set { offset = value; bOff = true; }
        }
        private bool bOff;
        private Pnt offset;
        [XmlIgnore]
        public Pnt BeginCell
        {
            get
            {
                if (!bInit)
                {
                    if (BeginPositionString.IsNullOrEmpty()) beginCell = new Pnt();
                    else beginCell = new Pnt(BeginPositionString);
                    bInit = true;
                }
                return beginCell;
            }
            set { beginCell = value; bInit = true; }
        }
        private Pnt beginCell;
        private bool bInit;
        [XmlIgnore]
        public Pnt EndCell
        {
            get
            {
                if (!bEnd)
                {
                    if (EndPositionString.IsNullOrEmpty()) endCell = new Pnt();
                    else endCell = new Pnt(EndPositionString);
                    bEnd = true;
                }
                return endCell;
            }
            set { endCell = value; bEnd = true; }
        }
        private bool bEnd;
        private Pnt endCell;
        [XmlIgnore]
        public WallDirection CornerType
        {
            get { return (WallDirection)Math.Abs(BendTypeValue); }
        }
        [XmlIgnore]
        public bool IsReverseCorner { get { return BendTypeValue < 0; } }
        [XmlIgnore]
        public Pnt Vector { get { return EndCell - BeginCell; } }
        [XmlIgnore]
        public Pnt FullVector
        {
            get
            {
                if (FollowBy != null) return Vector + FollowBy.Vector;
                else return Vector;
            }
        }


        public static bool SectionEqual(CliffSection a, CliffSection b)
        {
            return a.SetIndex == b.SetIndex && a.SubIndex == b.SubIndex;
        }
    }

    public class SectionLimitation
    {
        [XmlArrayItem("sec")]
        public List<CliffSection> Allow { get; set; } = new List<CliffSection>();
        [XmlAttribute("type")]
        public int Type { get; set; }
        [XmlAttribute("vec")]
        public string VectorString { get; set; }
        [XmlIgnore]
        public Pnt Vector
        {
            get
            {
                if (!bVec)
                {
                    vector = new Pnt(VectorString);
                    bVec = true;
                }
                return vector;
            }
            set { vector = value; bVec = true; }
        }
        private Pnt vector;
        private bool bVec;
    }

    public class TheaterCliffSet
    {
        [XmlArrayItem("sec")]
        public List<CliffSection> Sections { get; set; } = new List<CliffSection>();
        [XmlArrayItem("suc")]
        public List<SectionLimitation> Successor { get; set; } = new List<SectionLimitation>();
        [XmlAttribute("theater")]
        public string TheaterType { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("key")]
        public string Key { get; set; }
    }
}
