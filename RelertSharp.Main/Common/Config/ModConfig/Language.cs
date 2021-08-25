using Newtonsoft.Json;
using RelertSharp.Common.Config.Model;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public class Language
    {
        public Language(string fileName = "language.json")
        {
            TextReader reader = new StreamReader(fileName);
            Data = JsonConvert.DeserializeObject<LanguageControl>(reader.ReadToEnd());
        }


        public LanguageControl Data { get; set; }
    }
}
