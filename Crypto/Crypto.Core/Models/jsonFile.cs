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
        public static List<CurrencyModel> CryptoCurrencies { get; set; }

        public static void InitializeCurrienciesListFromFile()
        {
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Crypto.Core\\jsonfiles\\waluty\\Writes.json");
            
            var jsonFile = File.ReadAllText(path);

            CryptoCurrencies = JsonConvert.DeserializeObject<List<CurrencyModel>>(jsonFile);
        }
    }
}
