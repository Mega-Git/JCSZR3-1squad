using Crypto.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class OutputCore
{
        //var searchedValue = OutputCore.GetStringFromUser("Podaj nazwę do wyszukania");
	
	public static string DisplayValueFromName(string name)
    {
		var NewName = Search.SearchByName(name);
        if (NewName == "No match")
        {
            return NewName;
        }
        else
        {
		var Value = JsonFile.CryptoCurrencies.Find(x => x.Currency.Contains(NewName)).Prices[0];
		return $"{NewName} {Value}";
        }
    }
	public static void DisplayAllCurrencies()
    {
        JsonFile.CryptoCurrencies.ForEach(x => Console.WriteLine($"{x.Currency} {x.Prices[0]}"));
    }
	public static void DisplayValueFromNameInTimePeriod(string name)
    {
        var NewName = Search.SearchByName(name);
        if (NewName == "No match")
        {
            Console.WriteLine(NewName);
        }
        else
        {
		    var Value = JsonFile.CryptoCurrencies.Find(x => x.Currency.Contains(NewName)).Prices[0];
            DateTime StartingDate = DateTime.Parse(GetStartingDate());
            DateTime EndingDate = DateTime.Parse(GetEndingDate());
            //List<DateTime> Dates = Enumerable.Range(0, int.MaxValue).Select(index => new DateTime(StartingDate.AddDays(index))).TakeWhile(date => date <= EndingDate).ToList();
            var UserDates = new List<DateTime>();
            for (var date = StartingDate; date <= EndingDate; date = date.AddDays(1))
            {
                UserDates.Add(date);
            }
            //var Dates = Enumerable.Range(0, 1 + EndingDate.Subtract(StartingDate).Days).Select(offset => StartingDate.AddDays(offset)).ToList();
            var CurrencyDates = JsonFile.CryptoCurrencies.Where(x => x.Currency.Contains(NewName)).SelectMany(x => x.Timestamps).ToList();
            //CurrencyDates = CurrencyDates.Select(x => DateTime.Parse(x));
            int i = 0;
            var OverlappingDates = CurrencyDates.Select(x => x).Intersect(UserDates.Select(y => y.ToString())).ToList();
            //foreach (var item in UserDates)
            //{
            //    var CurrentDate = CurrencyDates.Where(x => UserDates.Contains(DateTime.Parse(x)));
            //    if (CurrentDate != null)
            //    {
            //        OverlappingDates.Add(CurrentDate.ToString());
            //        i++;
            //    }
            //}
            if (OverlappingDates.Count() == 0)
            {
                Console.WriteLine("No matches");
            }
            else
            {
                OverlappingDates.ForEach(x => Console.WriteLine(x));
            }
        }
        //Search.CurrencyList.FindAll(x => x.Value >= StartingDate && x.Value <= EndingDate) ????????
    }
    public static string GetStartingDate()
    {
        Console.WriteLine("Input starting date: ");
        Console.Write("Day: ");
        string StartingDay = Console.ReadLine();
        Console.Write("Month: ");
        string StartingMonth = Console.ReadLine();
        Console.Write("Year: ");
        string StartingYear = Console.ReadLine();
        string StartingDate = $"{StartingYear}-{StartingMonth}-{StartingDay}";
        return StartingDate;
    }
    public static string GetEndingDate()
    {
        Console.WriteLine("Input ending date: ");
        Console.Write("Day: ");
        string EndingDay = Console.ReadLine();
        Console.Write("Month: ");
        string EndingMonth = Console.ReadLine();
        Console.Write("Year: ");
        string EndingYear = Console.ReadLine();
        string EndingDate = $"{EndingYear}-{EndingMonth}-{EndingDay}";
        return EndingDate;
    }

    //public static string GetStringFromUser(string message)
    //{
    //    Console.Clear();
    //    Console.WriteLine(message);

    //    var userString = Console.ReadLine();

    //    if (string.IsNullOrWhiteSpace(userString))
    //    {
    //        Console.WriteLine("please input value");
    //    }

    //    return userString;
    //}

    public void DisplayCurrencyWithHighestValueLoss()
    {
        //Search.CurrencyList.Find(x => x.ValueLoss.Max()); ????
    }
	public static void DisplayValueFromNameButton()
    {
		Console.WriteLine("Input currency name: ");
        string CurrencyName = Console.ReadLine();
        Console.WriteLine(DisplayValueFromName(CurrencyName));
    }
	public static void DisplayAllCurrenciesButton()
    {
        Console.WriteLine("All currencies: ");
		DisplayAllCurrencies();
    }
    public static void DisplayValueFromNameInTimePeriodButton()
    {
        Console.WriteLine("Input currency name: ");
        string CurrencyName = Console.ReadLine();
        DisplayValueFromNameInTimePeriod(CurrencyName);
    }
    public void DisplayCurrencyWithHighestValueLossButton()
    {
        Console.WriteLine("Currency with highest value loss: ");
        DisplayCurrencyWithHighestValueLoss();
    }
}
