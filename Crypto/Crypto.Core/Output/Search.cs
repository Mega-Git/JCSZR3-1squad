using System;
using System.Collections;
using System.Collections.Generic;

public class Search
{
    static public List<String> CurrencyList = new List<string>();
	public static string SearchByName(string name)
	{
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
