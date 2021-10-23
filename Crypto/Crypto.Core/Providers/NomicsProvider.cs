using Crypto.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Providers
{
    public class NomicsProvider
    {
        public static string GetIds()
        {
            var client = new HttpClient();
            string key = "900135c7b342d5abfe1594e8a6275295376539e3";
            string URL = $"https://api.nomics.com/v1/currencies/ticker?key={key}&per-page=100&page=1&sort=rank";
            var response = client.GetAsync(URL).Result;

            var IdsName = JsonConvert.DeserializeObject<List<CurrencyModel>>(response.Content.ReadAsStringAsync().Result);

           var test = IdsName.Select(x => x.Currency).ToList().ToString();

            return test;
        }

        public static List<CurrencyModel> GetData()
        {
            var client = new HttpClient();

            var ids =  GetIds();
            string key = "900135c7b342d5abfe1594e8a6275295376539e3";
            string URL = $"https://api.nomics.com/v1/currencies/sparkline?key={key}&ids={ids}&start=2021-04-14T00%3A00%3A00Z";
            var response = client.GetAsync(URL).Result;
            while (response.StatusCode != HttpStatusCode.OK)
            {
                System.Threading.Thread.Sleep(200);
                response = client.GetAsync(URL).Result;
            }
            var Crypto = JsonConvert.DeserializeObject<List<CurrencyModel>>(response.Content.ReadAsStringAsync().Result);

            //Serialize data from API to file in folder
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Crypto.Core\\jsonfiles\\waluty\\Writes.json");

            string json = JsonConvert.SerializeObject(Crypto.ToArray());

            File.WriteAllText(path, json);


            return Crypto;

        }

    }
}
