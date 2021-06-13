using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Crypto.Core.Models;

public class Search
{
	public static string SearchByName(string name)
	{
        if (JsonFile.CryptoCurrencies.Any(x => x.Currency == name))
        {
            return JsonFile.CryptoCurrencies.First(x => x.Currency == name).Currency;
        }
        else
        {
            return "No match";
        }
	}
}
