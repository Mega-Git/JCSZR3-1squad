using Crypto.Menu;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Crypto.Core.Models;
using Crypto.Core.CryptoList;

namespace Crypto.Display.CryptoList
{
    public class Program
    {
        


        static void Main(string[] args)
        {
            {
                JsonFile.InitializeCurrienciesListFromFile();
                
                List<string> firstBoxes = new List<string>()
        {
            "CRYPTOCURRENCY",
            "ADD",
            "FAVORITE",
            "EXIT"
        };

                menuDraw menu = new menuDraw();

                menu.firstMainMenu(firstBoxes);

                Console.CursorVisible = false;

                //ustawienie pod dany przycisk podstrony
                while (true)
                {
                    string selectMenu = menu.firstMainMenu(firstBoxes);


                    if (selectMenu == "EXIT")
                    {
                        Environment.Exit(0);

                    }

                    else if (selectMenu == "CRYPTOCURRENCY")
                    {
                        Console.Clear();
                        //tutaj dodajcie scieżke swojego pliku json

                        foreach (var item in JsonFile.CryptoCurrencies)
                        {

                            Console.WriteLine(item.Currency + " " + item.Prices[0]);


                        }
                        Console.ReadKey();
                        break;
                    }
                    else if (selectMenu == "ADD")
                    {
                        Console.Clear();
                        InputCore.AddCurrencyName();
                        Console.ReadKey();
                        break;
                    }


                }

            }



           

        }
    }
}

