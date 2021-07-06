using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common.Config.Model
{
    public class LanguageControl
    {
        [JsonProperty("Triggers")]
        internal TriggerLanguageCollection Triggers { get; set; }
    }

    internal class TriggerLanguageCollection
    {
        public Dictionary<int, TriggerLanguageItem> Event { get; set; }
        public Dictionary<int, TriggerLanguageItem> Action { get; set; }
        public Dictionary<int, TriggerLanguageItem> Script { get; set; }

        public static void Serielize(TriggerLanguageCollection src, string filename)
        {
            string json = JsonConvert.SerializeObject(src);
            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.TextWriter w = new System.IO.StreamWriter(fs);
            w.Write(json);
            w.Flush();
            fs.Dispose();
        }
    }

    internal class TriggerLanguageItem
    {
        [JsonProperty("abs")]
        public string Abstract { get; set; }
        [JsonProperty("fmt")]
        public string FormatString { get; set; }
        [JsonProperty("des")]
        public string Description { get; set; }
    }
}
