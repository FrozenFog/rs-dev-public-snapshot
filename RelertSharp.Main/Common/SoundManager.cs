using RelertSharp.Encoding;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using System;
using System.Diagnostics;
using System.IO;
using System.Media;

namespace RelertSharp.Common
{
    public class SoundManager
    {
        private SoundPlayer player = new SoundPlayer();
        private MemoryStream ms = new MemoryStream();
        private Stopwatch watch = new Stopwatch();
        private bool isValid = false;


        #region ConstruCtor SoundManager
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
            ms.Dispose();
            watch.Reset();
            CurrentLength = -1;
            GC.Collect();
        }
        public void Play()
        {
            if (!isValid) return;
            try
            {
                player.Load();
                watch.Restart();
                player.Play();
            }
            catch { Stop(); }
        }
        public void LoadWav(WavFile wav)
        {
            if (wav.ByteArray == null)
            {
                isValid = false;
                return;
            }
            if (wav.WavTypeFlag == 0x11)
            {
                if (wav.Chanel == 1) wav = new WavFile(AudEncoding.DecodeAcmWav(wav.SampleBytes, wav.BlockAlign, true), wav.SamplesPerSec, wav.Chanel);
                else wav = new WavFile(AudEncoding.DecodeAcmWav(wav.SampleBytes, wav.BlockAlign, false), wav.SamplesPerSec, wav.Chanel);
            }
            ms = new MemoryStream(wav.ByteArray);
            player.Dispose();
            player = new SoundPlayer(ms);
            CurrentLength = wav.TimeMilSec;
            watch.Reset();
            isValid = true;
        }
        public string GetSoundName(string regname, SoundType type)
        {
            INIEntity ent;
            switch (type)
            {
                case SoundType.Eva:
                    string evakey = GlobalVar.GlobalConfig.GetSideInfo(x => x.SideId == GlobalVar.PlayerSide).EvaPrefix;
                    ent = GlobalVar.GlobalSound.GetEva(regname);
                    return ent[evakey] + ".wav";
                case SoundType.SoundBankRnd:
                    ent = GlobalVar.GlobalSound.GetSound(regname);
                    string[] randoms = ent["Sounds"].Split(new char[] { ' ' });
                    Random r = new Random();
                    int rand = r.Next(0, randoms.Length - 1);
                    string result = randoms[rand];
                    if (result.StartsWith("$")) result = result.Substring(1);
                    return result;
                case SoundType.Theme:
                    ent = GlobalVar.GlobalSound.GetTheme(regname);
                    return ent["Sound"] + ".wav";
                default:
                    return string.Empty;
            }
        }
        //public string GetSoundName(TechnoPair pair, SoundType type, bool isZero = false)
        //{
        //    INIEntity ent;
        //    int index = -1;
        //    try
        //    {
        //        if (isZero) index = int.Parse(pair.Index);
        //    }
        //    catch
        //    {
        //        return string.Empty;
        //    }
        //    switch (type)
        //    {
        //        case SoundType.Eva:
        //        case SoundType.Eva0:
        //            string tmp = GlobalVar.PlayerSide + "Eva";
        //            string evakey = GlobalVar.GlobalConfig["SoundConfigs"][tmp];
        //            if (isZero) ent = GlobalVar.GlobalSound.GetEva(int.Parse(pair.Index));
        //            else ent = GlobalVar.GlobalSound.GetEva(pair.Index);
        //            return ent[evakey] + ".wav";
        //        case SoundType.SoundBankRnd:
        //        case SoundType.SoundBankRnd0:
        //            if (isZero) ent = GlobalVar.GlobalSound.GetSound(int.Parse(pair.Index));
        //            else ent = GlobalVar.GlobalSound.GetSound(pair.Index);
        //            string[] randoms = ent["Sounds"].Split(new char[] { ' ' });
        //            Random r = new Random();
        //            int rand = r.Next(0, randoms.Length - 1);
        //            string result = randoms[rand];
        //            if (result.StartsWith("$")) result = result.Substring(1);
        //            return result;
        //        case SoundType.Theme:
        //        case SoundType.Theme0:
        //            if (isZero) ent = GlobalVar.GlobalSound.GetTheme(int.Parse(pair.Index));
        //            else ent = GlobalVar.GlobalSound.GetTheme(pair.Index);
        //            return ent["Sound"] + ".wav";
        //        default:
        //            return string.Empty;
        //    }
        //}
        #endregion


        #region Private Methods - SoundManager

        #endregion


        #region Public Calls - SoundManager
        public int CurrentLength { get; private set; } = -1;
        public bool IsPlaying { get { return watch.ElapsedMilliseconds <= CurrentLength; } }
        #endregion
    }
}
