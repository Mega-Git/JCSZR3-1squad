using System;
using System.Collections;
using System.Collections.Generic;

public class Search
{
	public static string SearchValueByName(string name)
	{
        List<String> CurrencyList = new();
        if (CurrencyList.Contains(name))
        {
            var x = CurrencyList.Find(z => z.Contains(name));
            return x;
        }
        else
        {
            return "No match";
        }
	}
}
