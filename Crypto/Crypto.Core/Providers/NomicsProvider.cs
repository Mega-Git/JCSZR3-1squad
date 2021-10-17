using Crypto.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Providers
{
    public class NomicsProvider
    {

      public static List<CurrencyModel> GetData()
        {
            var client = new HttpClient();

            string key = "900135c7b342d5abfe1594e8a6275295376539e3";
            string URL = $"https://api.nomics.com/v1/currencies/sparkline?key={key}&start=2021-04-14T00%3A00%3A00Z";
            var response = client.GetAsync(URL).Result;
            var Crypto = JsonConvert.DeserializeObject<List<CurrencyModel>>(response.Content.ReadAsStringAsync().Result);

            return Crypto;

        }

    }
}
