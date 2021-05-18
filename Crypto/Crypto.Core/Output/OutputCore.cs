using System;

public class OutputCore
{
	public string Display(string name)
	{
		return $"{name}";
	}

	public void DisplayValueFromName(string name)
    {
		var NewName = Search.SearchValueByName(name);
		Display(NewName);
    }
}
