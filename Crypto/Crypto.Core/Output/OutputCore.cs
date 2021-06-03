using System;

public class OutputCore
{
	public string Display(string value)
	{
		return $"{value}";
	}

	public string DisplayValueFromName(string name)
    {
		var NewName = Search.SearchByName(name);
		var Value = Search.CurrencyList.Find(x => x.Contains(NewName));
		return Display(Value);
    }
	public void DisplayAllCurrencies()
    {
        Search.CurrencyList.ForEach(x => Display(x));
    }
	public void DisplayValueFromNameInTimePeriod(string name)
    {
        var NewName = Search.SearchByName(name);
		var Value = Search.CurrencyList.Find(x => x.Contains(NewName));
        string StartingDate = GetStartingDate();
        string EndingDate = GetEndingDate();
        //Search.CurrencyList.FindAll(x => x.Value >= StartingDate && x.Value <= EndingDate) ????????
    }
    public string GetStartingDate()
    {
        Console.WriteLine("Input starting date: ");
        Console.Write("Day: ");
        string StartingDay = Console.ReadLine();
        Console.Write("Month: ");
        string StartingMonth = Console.ReadLine();
        Console.Write("Year: ");
        string StartingYear = Console.ReadLine();
        string StartingDate = $"{StartingDay} {StartingMonth} {StartingYear}";
        return StartingDate;
    }
    public string GetEndingDate()
    {
        Console.WriteLine("Input ending date: ");
        Console.Write("Day: ");
        string EndingDay = Console.ReadLine();
        Console.Write("Month: ");
        string EndingMonth = Console.ReadLine();
        Console.Write("Year: ");
        string EndingYear = Console.ReadLine();
        string EndingDate = $"{EndingDay} {EndingMonth} {EndingYear}";
        return EndingDate;
    }
    public void DisplayCurrencyWithHighestValueLoss()
    {
        //Search.CurrencyList.Find(x => x.ValueLoss.Max()); ????
    }
	public void DisplayValueFromNameButton()
    {
		Console.WriteLine("Input currency name: ");
        string CurrencyName = Console.ReadLine();
        Display(DisplayValueFromName(CurrencyName));
    }
	public void DisplayAllCurrenciesButton()
    {
        Console.WriteLine("All currencies: ");
		DisplayAllCurrencies();
    }
    public void DisplayValueFromNameInTimePeriodButton()
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
