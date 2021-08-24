using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Terraformer
{
    internal class RampBase
    {
        public enum RampSlopeType
        {
            Ignore = -1,
            Flat = 0,
            Xz1 = 1, Xz2 = 2,
            Yz1 = 3, Yz2 = 4
        }
        public struct RampSideSection
        {
            public RampSlopeType SlopeType;
            public int Height;
            public RampSideSection(int height, int type)
            {
                SlopeType = (RampSlopeType)type;
                Height = height;
            }
            public RampSideSection Copy()
            {
                return FromIdentityCode(IdentityCode);
            }
            public bool IsIgnore { get { return SlopeType == RampSlopeType.Ignore; } }
            public bool IsNoBindFlat { get { return (SlopeType == RampSlopeType.Flat && Height == 0) || IsIgnore; } }
            public int IdentityCode
            {
                get
                {
                    if (SlopeType == RampSlopeType.Ignore) return 10;
                    if (SlopeType == RampSlopeType.Flat) return Height;
                    return ((int)SlopeType << 1) + (Height - 1);
                }
            }
            public static bool operator ==(RampSideSection a, RampSideSection b)
            {
                return a.SlopeType == b.SlopeType && a.Height == b.Height;
            }
            public static bool operator !=(RampSideSection a, RampSideSection b)
            {
                return a.SlopeType != b.SlopeType || a.Height != b.Height;
            }
            public static RampSideSection Flat
            {
                get { return new RampSideSection(0, 0); }
            }
            public static RampSideSection Ignore
            {
                get { return new RampSideSection(-1, -1); }
            }
            public static RampSideSection FromIdentityCode(int code)
            {
                if (code == 10) return Ignore;
                if (code == 0) return Flat;
                if (code == 1) return new RampSideSection(1, 0);
                int height = (code & 1) + 1;
                RampSlopeType type = (RampSlopeType)(code >> 1);
                RampSideSection result = new RampSideSection()
                {
                    Height = height,
                    SlopeType = type
                };
                return result;
            }
#if DEBUG
            public override string ToString()
            {
                if (IsNoBindFlat) return "no bind";
                return string.Format("{0}, {1}", Height, SlopeType.ToString());
            }
#endif
        }
        public struct RampData
        {
            public RampSideSection[] Sections { get; private set; }
            public int Offset { get; private set; }
            public int HeightFix { get; set; }
            public RampData(string param, int offset)
            {
                List<RampSideSection> results = new List<RampSideSection>();
                RampSideSection func(char height, char type)
                {
                    RampSideSection r = new RampSideSection(int.Parse(height.ToString()), int.Parse(type.ToString()));
                    return r;
                }
                for (int i = 0; i < param.Length;)
                {
                    RampSideSection data = func(param[i++], param[i++]);
                    results.Add(data);
                }
                Sections = results.ToArray();
                Offset = offset;
                HeightFix = 0;
            }
            public RampData Copy()
            {
                RampData result = new RampData()
                {
                    HeightFix = this.HeightFix,
                    Offset = this.Offset
                };
                List<RampSideSection> sections = new List<RampSideSection>();
                foreach (RampSideSection sec in Sections)
                {
                    RampSideSection side = new RampSideSection()
                    {
                        SlopeType = sec.SlopeType,
                        Height = sec.Height
                    };
                    sections.Add(side);
                }
                result.Sections = sections.ToArray();
                return result;
            }
#if DEBUG
            public override string ToString()
            {
                if (IsFlat) return "flat";
                if (IsIgnore) return "ignore";
                return Offset.ToString();
            }
#endif
            public bool IsFlat
            {
                get { return Offset == 0; }
            }
            public bool IsIgnore
            {
                get { return Offset == -1; }
            }
            public bool StretchHeight
            {
                get { return Sections.Any(x => x.Height == 2); }
            }
            public int HeightSum
            {
                get { return Sections.Sum(x => x.Height); }
            }
            public static RampData FlatRamp
            {
                get
                {
                    return new RampData("00000000", 0);
                }
            }
            public static RampData IgnoreRamp
            {
                get
                {
                    return new RampData("00000000", -1);
                }
            }
        }
    }
}
