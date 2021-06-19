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
            "ALL",
            "TIME",
            "FAVORITE",
            "EXIT"
        };

                menuDraw menu = new menuDraw();

                menu.firstMainMenu(firstBoxes);

                Console.CursorVisible = false;

                //ustawienie pod dany przycisk podstrony
                while (true)
                {
                    Console.Clear();
                    string selectMenu = menu.firstMainMenu(firstBoxes);
                    Console.Clear();


                    if (selectMenu == "EXIT")
                    {
                        Environment.Exit(0);

                    }

                    else if (selectMenu == "CRYPTOCURRENCY")
                    {
                        OutputCore.DisplayValueFromNameButton();
                        Console.ReadKey();
                    }
                    else if (selectMenu == "ALL")
                    {
                        OutputCore.DisplayAllCurrenciesButton();
                        Console.ReadKey();
                    }
                    else if (selectMenu == "TIME")
                    {
                        OutputCore.DisplayValueFromNameInTimePeriodButton();
                        Console.ReadKey();
                    }


                }

            }



           

        }
    }
}

