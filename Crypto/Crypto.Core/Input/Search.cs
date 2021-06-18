using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypto.Core.Models;

public class Search
{
    public static string SearchByName(string name)
    {
        return JsonFile.CryptoCurrencies.Any(x => x.Currency == name) ? JsonFile.CryptoCurrencies.First(x => x.Currency == name).Currency : "No match";
    }
}
