using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;

namespace RelertSharp.FileSystem
{
    public class SoundBank
    {
        private Dictionary<string, IdxIndex> indexs = new Dictionary<string, IdxIndex>();


        #region Ctor - SoundBank
        public SoundBank(string[] bagnames)
        {
            foreach (string name in bagnames)
            {
                IdxBagFile bag = new IdxBagFile();
                bag.Load(GlobalVar.GlobalDir.GetRawByte(name + ".idx"));
                indexs[name] = bag.Index;
            }
        }
        #endregion


        #region Public Methods
        public WavFile GetSound(string name)
        {
            WavFile wav = new WavFile();
            if (GlobalVar.GlobalDir.HasFile(name))
            {
                if (name.EndsWith("wav"))
                {
                    wav = new WavFile(GlobalVar.GlobalDir.GetRawByte(name));
                    return wav;
                }
                if (name.EndsWith("aud"))
                {
                    AudFile aud = new AudFile(GlobalVar.GlobalDir.GetRawByte(name), name);
                    return aud.ToWav();
                }
            }
            foreach (string bagname in indexs.Keys)
            {
                if (indexs[bagname].HasFile(name))
                {
                    byte[] bag = GlobalVar.GlobalDir.GetRawByte(bagname + ".bag");
                    IdxBagFile f = new IdxBagFile(indexs[bagname], bag);
                    wav = f.ReadAudFile(name).ToWav();
                    f.Dispose();
                    GC.Collect();
                    return wav;
                }
            }
            return wav;

        }
        public void PlaySound(string name)
        {
            WavFile wav = GetSound(name);
            if (wav.IsEmpty) return;
            using (MemoryStream ms = new MemoryStream(wav.ByteArray))
            {
                SoundPlayer player = new SoundPlayer(ms);
                player.Load();
                player.Play();
            }

        }
        #endregion


        #region Private Methods
        #endregion


        #region Public Calls
        #endregion
    }
}
