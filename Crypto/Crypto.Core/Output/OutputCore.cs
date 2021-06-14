using Crypto.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class OutputCore
{
	public static string DisplayValueFromName(string name)
    {
		var newName = Search.SearchByName(name);
        if (newName == "No match")
        {
            return newName;
        }
        else
        {
		    var value = JsonFile.CryptoCurrencies.Find(x => x.Currency.Contains(newName)).Prices[0];
		    return $"{newName} {value}";
        }
    }
	public static void DisplayAllCurrencies()
    {
        JsonFile.CryptoCurrencies.ForEach(x => Console.WriteLine($"{x.Currency} {x.Prices[0]}"));
    }
	public static void DisplayValueFromNameInTimePeriod(string name)
    {
        var newName = Search.SearchByName(name);
        if (newName == "No match")
        {
            Console.WriteLine(newName);
        }
        else
        {
            DateTime startingDate = DateTime.Parse(GetStartingDate()).AddHours(2);
            DateTime endingDate = DateTime.Parse(GetEndingDate()).AddHours(2);
            var userDates = new List<DateTime>();
            for (var date = startingDate; date <= endingDate; date = date.AddDays(1))
            {
                userDates.Add(date);
            }
            var currencyDates = JsonFile.CryptoCurrencies.Where(x => x.Currency.Contains(newName)).SelectMany(x => x.Timestamps).ToList();
            List<DateTime> currencyDatesFix = currencyDates.Select(x => DateTime.Parse(x)).ToList();
            var overlappingDates = currencyDatesFix.Select(x => x).Intersect(userDates.Select(y => y)).ToList();
            if (overlappingDates.Count == 0)
            {
                Console.WriteLine("No matches");
            }
            else
            {
                var datesWithValues = JsonFile.CryptoCurrencies.Where(x => x.Currency.Contains(newName)).Select(x => new {x.Prices, x.Timestamps}).ToList();
                var datesWithValuesTimestamps = datesWithValues.SelectMany(x => x.Timestamps).ToList();
                var datesWithValuesTimestampsFix = datesWithValuesTimestamps.Select(x => DateTime.Parse(x)).ToList();
                for (int i = 0; i < overlappingDates.Count; i++)
                {
                    for (int o = 0; o < datesWithValuesTimestamps.Count; o++)
                    {
                        var dateFind = datesWithValuesTimestampsFix[o].Equals(overlappingDates[i]);
                        if (dateFind)
                        {
                            Console.WriteLine($"Time: {datesWithValues[0].Timestamps[o]} Price: {datesWithValues[0].Prices[o]} ");
                            break;
                        }
                    }
                }
            }
        }
    }
    public static string GetStartingDate()
    {
        Console.WriteLine("Input starting date: ");
        Console.Write("Day: ");
        string startingDay = Console.ReadLine();
        Console.Write("Month: ");
        string startingMonth = Console.ReadLine();
        Console.Write("Year: ");
        string startingYear = Console.ReadLine();
        string startingDate = $"{startingYear}-{startingMonth}-{startingDay}";
        return startingDate;
    }
    public static string GetEndingDate()
    {
        Console.WriteLine("Input ending date: ");
        Console.Write("Day: ");
        string endingDay = Console.ReadLine();
        Console.Write("Month: ");
        string endingMonth = Console.ReadLine();
        Console.Write("Year: ");
        string endingYear = Console.ReadLine();
        string endingDate = $"{endingYear}-{endingMonth}-{endingDay}";
        return endingDate;
    }

    public static string GetCurrencyName()
    {
        Console.WriteLine("Input currency name: ");
        var currencyName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(currencyName))
        {
            Console.WriteLine("Input a valid name: ");
        }

        return currencyName.ToUpper();
    }
    public static void DisplayCurrencyWithHighestValueLoss()
    {
        
    }
	public static void DisplayValueFromNameButton()
    {
        Console.WriteLine(DisplayValueFromName(GetCurrencyName()));
    }
	public static void DisplayAllCurrenciesButton()
    {
        Console.WriteLine("All currencies: ");
		DisplayAllCurrencies();
    }
    public static void DisplayValueFromNameInTimePeriodButton()
    {
        DisplayValueFromNameInTimePeriod(GetCurrencyName());
    }
    public static void DisplayCurrencyWithHighestValueLossButton()
    {
        Console.WriteLine("Currency with highest value loss: ");
        DisplayCurrencyWithHighestValueLoss();
    }
}
