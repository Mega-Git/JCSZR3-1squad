using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Menu
{
    class menuDraw
    {
        private int indexMenu = 0;
        /*ustawienie indexu menu default,
        początkowa domyślna pozycja*/


        /*elements-elementy menu,*/
        public string firstMainMenu(List<string> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (i == indexMenu)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(elements[i]);
                }
                else
                {
                    Console.WriteLine(elements[i]);
                }
                Console.ResetColor();
            }

            ConsoleKeyInfo arrow = Console.ReadKey();
            if (arrow.Key == ConsoleKey.DownArrow)
            {
                if (indexMenu == elements.Count - 1) { }
                else { indexMenu++; }

            }
            else if (arrow.Key == ConsoleKey.UpArrow)
            {
                if (indexMenu <= 0) { }
                else { indexMenu--; }
            }
            else if (arrow.Key == ConsoleKey.Enter)
            {
                return elements[indexMenu];
            }
            else
            {
                Console.Clear();
                return "";
            }

            Console.Clear();
            return "";

        }

    }



}

