using Crypto.Menu;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace Crypto.Display.CryptoList
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                List<string> firstBoxes = new List<string>()
        {
            "CRYPTOCURRENCY",
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
                        var path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Crypto.Core\\jsonfiles\\waluty\\Prices.json"); //@"D:\CryptoApp\JCSZR3-1squad\Crypto\Crypto.Core\jsonFiles\waluty\Prices.json";
                        string jsonFile = File.ReadAllText(path);


                        var CryptoCurrency = JsonConvert.DeserializeObject<List<currencyList>>(jsonFile);


                        foreach (var item in CryptoCurrency)
                        {

                            Console.WriteLine(item.currency + " " + item.prices[0]);


                        }
                        Console.ReadKey();
                        break;
                    }


                }

            }



           

        }
    }
}

