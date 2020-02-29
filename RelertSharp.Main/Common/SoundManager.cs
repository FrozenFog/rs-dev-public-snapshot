using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;
using System.Diagnostics;
using relert_sharp.IniSystem;
using relert_sharp.FileSystem;

namespace relert_sharp.Common
{
    public class SoundManager
    {
        private SoundPlayer player = new SoundPlayer();
        private MemoryStream ms = new MemoryStream();
        private Stopwatch watch = new Stopwatch();


        #region Constructor SoundManager
        public SoundManager()
        {
            watch.Start();
        }
        #endregion


        #region Public Methods - SoundManager
        public void Stop()
        {
            player.Stop();
            player.Dispose();
            watch.Reset();
        }
        public void Play()
        {
            player.Load();
            watch.Restart();
            player.Play();
        }
        public void LoadWav(WavFile wav)
        {
            ms = new MemoryStream(wav.ByteArray);
            player = new SoundPlayer(ms);
            CurrentLength = wav.TimeMilSec;
            watch.Reset();
        }
        public string GetSoundName(TechnoPair pair, SoundType type)
        {
            INIEntity ent;
            switch (type)
            {
                case SoundType.Eva:
                    string tmp = GlobalVar.PlayerSide + "Eva";
                    string evakey = GlobalVar.GlobalConfig["SoundConfigs"][tmp];
                    ent = GlobalVar.GlobalSound.GetEva(pair.Index);
                    return ent[evakey] + ".wav";
                case SoundType.SoundBankRnd:
                    ent = GlobalVar.GlobalSound.GetSound(pair.Index);
                    string[] randoms = ent["Sounds"].Split(new char[] { ' ' });
                    Random r = new Random();
                    int rand = r.Next(0, randoms.Length - 1);
                    return randoms[rand];
                case SoundType.Theme:
                    ent = GlobalVar.GlobalSound.GetTheme(pair.Index);
                    return ent["Sound"] + ".wav";
                default:
                    return string.Empty;
            }
        }
        #endregion


        #region Private Methods - SoundManager

        #endregion


        #region Public Calls - SoundManager
        public int CurrentLength { get; private set; } = 0;
        public bool IsPlaying { get { return watch.ElapsedMilliseconds <= CurrentLength; } }
        #endregion
    }
}
