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
    public class JsonFile
    {
        public static List<CurrencyTest> CryptoCurrencies { get; set; }

        public static void InitializeCurrienciesListFromFile()
        {
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Crypto.Core\\jsonfiles\\waluty\\Prices.json");
            
            string jsonFile = File.ReadAllText(path);

            CryptoCurrencies = JsonConvert.DeserializeObject<List<CurrencyTest>>(jsonFile);
        }

        public static void SaveListToFile()
        {

        }
    }
}
