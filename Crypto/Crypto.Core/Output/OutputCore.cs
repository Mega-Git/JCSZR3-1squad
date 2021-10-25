using Crypto.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class OutputCore
{
	public static string DisplayValueFromName(string name)
    {
		var currencyName = Search.SearchByName(name);
        if (currencyName == "No match")
        {
            return currencyName;
        }
        else
        {
		    var price = JsonFile.CryptoCurrencies.Find(x => x.Currency.Contains(currencyName)).Prices[0];
		    return $"{currencyName} {price}";
        }
    }
	public static string DisplayAllCurrencies()
    {
        List<string> currencyList = new List<string>();
        JsonFile.CryptoCurrencies.ForEach(x => currencyList.Add($"{x.Currency} {x.Prices[0]}"));
        return string.Join("\n", currencyList);
    }
	public static string DisplayValueFromNameInTimePeriod(string name)
    {
        var currencyName = Search.SearchByName(name);
        if (currencyName == "No match")
        {
            return currencyName;
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
            var currencyDates = JsonFile.CryptoCurrencies.Where(x => x.Currency.Contains(currencyName)).SelectMany(x => x.Timestamps).ToList();
            List<DateTime> currencyDatesToDateTime = currencyDates.Select(x => DateTime.Parse(x)).ToList();
            var overlappingDates = currencyDatesToDateTime.Select(x => x).Intersect(userDates.Select(y => y)).ToList();
            if (overlappingDates.Count == 0)
            {
                return "No matches";
            }
            else
            {
                List<string> resultList = new List<string>();
                var datesWithValues = JsonFile.CryptoCurrencies.Where(x => x.Currency.Contains(currencyName)).Select(x => new {x.Prices, x.Timestamps}).ToList();
                var datesWithValuesTimestamps = datesWithValues.SelectMany(x => x.Timestamps).ToList();
                var datesWithValuesTimestampsToDateTime = datesWithValuesTimestamps.Select(x => DateTime.Parse(x)).ToList();
                for (int i = 0; i < overlappingDates.Count; i++)
                {
                    for (int o = 0; o < datesWithValuesTimestamps.Count; o++)
                    {
                        var dateFind = datesWithValuesTimestampsToDateTime[o].Equals(overlappingDates[i]);
                        if (dateFind)
                        {
                            resultList.Add($"Time: {datesWithValues[0].Timestamps[o]} Price: {datesWithValues[0].Prices[o]} ");
                            break;
                        }
                    }
                }

                return string.Join("\n", resultList);
            }
        }
    }

    public static string GetDay()
    {
        Console.WriteLine("Day: ");
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int day))
            {
                if (day > 0 && day <= 31)
                {
                    return day.ToString();
                }
            }
            Console.WriteLine("Input a valid day: ");
        }
    }
    public static string GetMonth()
    {
        Console.WriteLine("Month: ");
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int month))
            {
                if (month > 0 && month <= 12)
                {
                    return month.ToString();
                }
            }
            Console.WriteLine("Input a valid month: ");
        }
    }
    public static string GetYear()
    {
        Console.WriteLine("Year: ");
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                if (year > 0 && year <= DateTime.Today.Year)
                {
                    return year.ToString();
                }
            }
            Console.WriteLine("Input a valid year: ");
        }
    }
    public static string GetStartingDate()
    {
        while (true)
        {
            Console.WriteLine("Input starting date: ");
            string startingDay = GetDay();
            string startingMonth = GetMonth();
            string startingYear = GetYear();
            string startingDate = $"{startingYear}-{startingMonth}-{startingDay}";
            
            if (!DateTime.TryParse(startingDate, out DateTime date))
            {
                Console.WriteLine("Invalid date, try again.");
            }
            else
            {
                return startingDate;
            }
        }
    }
    public static string GetEndingDate()
    {
        while (true)
        {
            Console.WriteLine("Input ending date: ");
            string endingDay = GetDay();
            string endingMonth = GetMonth();
            string endingYear = GetYear();
            string endingDate = $"{endingYear}-{endingMonth}-{endingDay}";
            if (!DateTime.TryParse(endingDate, out DateTime date))
            {
                Console.WriteLine("Invalid date, try again.");
            }
            else
            {
                return endingDate;
            }
        }
    }

    public static string GetCurrencyName()
    {
        Console.WriteLine("Input currency name: ");
        var currencyName = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(currencyName))
        {
            Console.WriteLine("Input a valid name: ");
            currencyName = Console.ReadLine();
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
        //Console.WriteLine("All currencies: ");
		DisplayAllCurrencies();
    }
    public static void DisplayValueFromNameInTimePeriodButton()
    {
        DisplayValueFromNameInTimePeriod(GetCurrencyName());
    }
    public static void DisplayCurrencyWithHighestValueLossButton()
    {
        //Console.WriteLine("Currency with highest value loss: ");
        DisplayCurrencyWithHighestValueLoss();
    }
}
