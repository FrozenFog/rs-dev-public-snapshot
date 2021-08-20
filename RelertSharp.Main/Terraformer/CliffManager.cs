using RelertSharp.Common;
using RelertSharp.Common.Config.Model;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Terraformer
{
    public static class CliffManager
    {
        private static CliffSection ApplySectionOffset(CliffSection src, Pnt offset)
        {
            return new CliffSection()
            {
                Offset = offset,
                SetIndex = src.SetIndex,
                SubIndex = src.SubIndex,
                BeginCell = src.BeginCell,
                EndCell = src.EndCell
            };
        }

        #region Api
        public static List<CliffSection> AlignCliffBetween(I2dLocateable beginCell, I2dLocateable endCell, string cliffKey, out I2dLocateable actualEnd)
        {
            Pnt current = new Pnt(beginCell);
            Pnt end = new Pnt(endCell);
            actualEnd = current;
            List<CliffSection> result = new List<CliffSection>();
            string theater = GlobalVar.GlobalMap.Info.TheaterName;
            var cfg = GlobalVar.GlobalConfig.ModConfig.CliffSets.Find(x => x.TheaterType == theater && x.Key == cliffKey);
            if (cfg != null)
            {
                var limitations = cfg.Successor;
                CliffSection prevSec = null;
                SectionLimitation filter = null;
                Pnt prevVec = default;
                Pnt destVec = end - current;
                bool exit = false;
                if (destVec == Pnt.Zero) return result;
                while (destVec.Magnitude() > 1 && !exit)
                {
                    CliffSection sec = null;
                    Pnt succOffset = new Pnt();
                    destVec = end - current;
                    IEnumerable<CliffSection> candidates;
                    if (prevSec == null)
                    {
                        candidates = cfg.Sections
                        .OrderBy(x => RsMath.I2dAngle(x.FullVector, destVec))
                        .ThenBy(x => (x.FullVector - destVec).Magnitude());
                    }
                    else
                    {
                        filter = limitations.Find(x => x.Type == prevSec.SuccessorType);
                        candidates = cfg.Sections.Where(x => filter.Allow.Any(filt => CliffSection.SectionEqual(x, filt)))
                            .OrderBy(x => RsMath.I2dAngle(x.FullVector, destVec))
                            .ThenBy(x => (x.FullVector - destVec).Magnitude());
                    }
                    foreach (var item in candidates)
                    {
                        if (!item.IsCorner)
                        {
                            prevVec = item.FullVector;
                            current += item.FullVector;
                            sec = item;
                            break;
                        }
                    }
                    if (sec != null)
                    {
                        if (filter != null) succOffset = filter.Allow.Find(x => CliffSection.SectionEqual(x, sec)).Offset;
                        result.Add(ApplySectionOffset(sec, succOffset));
                        if (sec.FollowBy != null)
                        {
                            result.Add(sec.FollowBy);
                        }
                        exit = destVec.Magnitude() < sec.FullVector.Magnitude();
                    }
                    else break;
                    actualEnd = current;
                    prevSec = sec;
                }
            }
            return result;
        }
        #endregion
    }
}
