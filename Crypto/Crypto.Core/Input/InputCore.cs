using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypto.Core.Models;

public class InputCore
    {
        public static string AddCurrencyName()
        {
            Console.WriteLine("Input currency name: ");
            var currencyNameAdd = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(currencyNameAdd))
            {
                Console.WriteLine("Input a valid name: ");
                currencyNameAdd = Console.ReadLine();
            }

            return currencyNameAdd.ToUpper();
        }
    }
