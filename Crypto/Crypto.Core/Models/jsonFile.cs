using Crypto.Core.CryptoList;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Models
{
   public static class jsonFile
    {

        public static List<CurrencyTest> jsonRead(string source)
        {

            var path =source;
            string jsonFile = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<List<CurrencyTest>>(jsonFile);

            


        }
    }
}
