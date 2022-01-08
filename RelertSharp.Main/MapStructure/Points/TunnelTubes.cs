using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.MapStructure.Points
{
    [IniEntitySerialize(Constant.MapStructure.ENT_TUBE)]
    public class MapTunnels : IIniEntitySerializable
    {
        public MapTunnels()
        {

        }
        public void ReadFromIni(INIEntity ent)
        {
            foreach (INIPair p in ent)
            {
                TubeSection sec = new TubeSection();
                sec.ReadFromIni(p);
                Sections.Add(sec);
            }
        }
        public INIEntity SaveAsIni()
        {
            INIEntity ent = this.GetNamedEnt();
            int i = 0;
            foreach (TubeSection section in Sections)
            {
                INIPair p = section.SaveAsIni();
                p.Name = i++.ToString();
                ent.AddPair(p);
            }
            return ent;
        }
        public List<TubeSection> Sections { get; set; } = new List<TubeSection>();
    }

    public class TubeSection : PointItemBase, IIniPairSerializable
    {
        internal TubeSection()
        {
            Directions.SetValueAll(TubeDirection._END);
        }
        public override void ReadFromIni(INIPair src)
        {
            Id = src.Name;
            ParameterReader reader = new ParameterReader(src.ParseStringList());
            StartPos.X = reader.ReadInt();
            StartPos.Y = reader.ReadInt();
            StartFacing = (TubeDirection)reader.ReadInt();
            EndPos.X = reader.ReadInt();
            EndPos.Y = reader.ReadInt();
            int i = 0;
            while (reader.CanRead)
            {
                Directions[i++] = (TubeDirection)reader.ReadInt(-1);
            }
            if (reader.ReadError) GlobalVar.Monitor.LogCritical(Id, Name, ObjectType, this);
        }
        public override INIPair SaveAsIni()
        {
            INIPair p = new INIPair(Id);
            ParameterWriter writer = new ParameterWriter();
            writer.Write(StartPos.FormatXYIni());
            writer.Write((int)StartFacing);
            writer.Write(EndPos.FormatXYIni());
            foreach (TubeDirection value in Directions)
            {
                if (value != TubeDirection._END) writer.Write((int)value);
                else
                {
                    writer.Write((int)TubeDirection._END);
                    break;
                }
            }
            p.Value = writer.ToString();
            return p;
        }
        public I2dLocateable StartPos { get; set; } = new Pnt();
        public I2dLocateable EndPos { get; set; } = new Pnt();
        public TubeDirection StartFacing { get; set; } = TubeDirection.NE;
        public override int X { get => StartPos.X; set => StartPos.X = value; }
        public override int Y { get => StartPos.Y; set => StartPos.Y = value; }
        public TubeDirection[] Directions { get; } = new TubeDirection[100];
        public override string Name { get { return Id; } set { } }
        public override string RegName { get { return Constant.VALUE_NONE; } set { } }
        public override MapObjectType ObjectType { get => MapObjectType.TubeTunnel; }
    }
}
