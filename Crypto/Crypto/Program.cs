using Crypto.Menu;
using System;
using System.Collections.Generic;

namespace Crypto
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                List<string> firstBoxes = new List<string>()
        {
            "start",
            "stop",
            "exit"
        };

                menuDraw menu = new menuDraw();

                menu.firstMainMenu(firstBoxes);

                Console.CursorVisible = false;
                while (true)
                {
                    string selectMenu = menu.firstMainMenu(firstBoxes);


                 if (selectMenu == "exit")
                    {
                        Environment.Exit(0);
                        
                    }
                }



            }



            Console.WriteLine("siema!");
            Console.WriteLine("test, rafal");
            Console.WriteLine("Monika test added");

        }
    }
}

