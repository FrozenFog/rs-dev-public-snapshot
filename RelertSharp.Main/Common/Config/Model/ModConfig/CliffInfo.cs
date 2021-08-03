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

    public class CliffCell
    {
        [XmlIgnore]
        public CliffJointPosition In { get; set; }
        [XmlAttribute("in")]
        public int InValue
        {
            get { return (int)In; }
            set { In = (CliffJointPosition)value; }
        }
        [XmlIgnore]
        public CliffJointPosition Out;
        [XmlAttribute("out")]
        public int OutValue
        {
            get { return (int)Out; }
            set { Out = (CliffJointPosition)value; }
        }
        [XmlIgnore]
        public CliffJointPosition Bend;
        [XmlAttribute("bend")]
        public int BendValue
        {
            get { return (int)Bend; }
            set { Bend = (CliffJointPosition)value; }
        }
        [XmlIgnore]
        public CliffJointPosition Next;
        [XmlAttribute("next")]
        public int NextValue
        {
            get { return (int)Next; }
            set { Next = (CliffJointPosition)value; }
        }
        /// <summary>
        /// Counter clock wise
        /// Left is hi-ground
        /// </summary>
        [XmlAttribute("isRight")]
        public bool HiGroundRight;
    }

    public class CliffSection
    {
        [XmlArrayItem("cell")]
        public List<CliffCell> Cells = new List<CliffCell>();
        [XmlAttribute("len")]
        public int Length;
        [XmlIgnore]
        public CliffJointPosition In;
        [XmlAttribute("in")]
        public int InValue
        {
            get { return (int)In; }
            set { In = (CliffJointPosition)value; }
        }
        [XmlIgnore]
        public CliffJointPosition Out;
        [XmlAttribute("out")]
        public int OutValue
        {
            get { return (int)Out; }
            set { Out = (CliffJointPosition)value; }
        }
        [XmlAttribute("set")]
        public int SetIndex { get; set; }
        [XmlAttribute("sub")]
        public int SubIndex { get; set; }
    }

    public class TheaterCliffSet
    {
        [XmlArrayItem("section")]
        public List<CliffSection> Sections { get; set; } = new List<CliffSection>();
        [XmlAttribute("theater")]
        public string TheaterType { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
