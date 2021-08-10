using Crypto.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Crypto.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Crypto.Web.Controllers
{
    public class HomeController : Controller
    {
        private static List<CurrencyTest> newcurrencies = new List<CurrencyTest>();
        public const string Descending = "desc";
        public const string Name = "name";
        public const string Price = "price";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index(bool selectedcheck, string sortColumn, decimal? minValue, decimal? maxValue, string currencyName, string sortDir = "")
        {
            if (maxValue == 0)
            {
                maxValue = null;
            }
            var currencyList = JsonFile.CryptoCurrencies.Select(x => x);
            var model = new CurrencyListModel
            {
                MinPriceIsValid = minValue < maxValue || minValue == null || maxValue == null
            };

            if (!string.IsNullOrEmpty(currencyName))
            {
                currencyList = currencyList.Where(x => x.Currency.Contains(currencyName.ToUpper()));
            }

            if (minValue != null || maxValue != null)
            {
                if (minValue >= 0 && maxValue >= minValue)
                {
                    currencyList = currencyList.Where(x => Convert.ToDecimal(x.Prices.Last()) > minValue && Convert.ToDecimal(x.Prices.Last()) < maxValue);
                }
                else if (minValue >= 0 && maxValue == null)
                {
                    currencyList = currencyList.Where(x => Convert.ToDecimal(x.Prices.Last()) > minValue);
                }
                else if (minValue == null && maxValue > 0)
                {
                    currencyList = currencyList.Where(x => Convert.ToDecimal(x.Prices.Last()) < maxValue);
                }
            }

            switch (sortColumn)
            {
                case Name:
                    currencyList = sortDir == Descending ? currencyList.OrderByDescending(x => x.Currency) : currencyList.OrderBy(x => x.Currency);
                    break;
                case Price:
                    currencyList = sortDir == Descending ? currencyList.OrderByDescending(x => x.Prices.Last()) : currencyList.OrderBy(x => x.Prices.Last());
                    break;
                default:
                    currencyList = currencyList.OrderBy(x => x.Currency);
                    break;
            }




            model.SortColumn = sortColumn;
            model.SortDirection = sortDir;
            model.MinPrice = minValue;
            model.MaxPrice = maxValue;
            model.CurencyName = currencyName;
            model.CurrencyList = currencyList;
            model.NewCurrencies = newcurrencies;

            return View(model);
        }
        public IActionResult Favorite(IEnumerable<CurrencyTest> listOfFavorite)
        {
            for (int i = 0; i < JsonFile.CryptoCurrencies.Count; i++)
            {
                JsonFile.CryptoCurrencies[i].Favorite = listOfFavorite.ToArray()[i].Favorite;

            }
            return RedirectToAction("Index");
        }

        public IActionResult FavoriteList()
        {
            return View(JsonFile.CryptoCurrencies.Where(c => c.Favorite));
        }



        


        public IActionResult AddCurrency(string currencyName, string currencyPrice)
        {
            newcurrencies.Add(
                new CurrencyTest { 
                    Currency = currencyName, 
                    Prices = new[] { currencyPrice } 
                }
                );
           

            return RedirectToAction("Index");
        }
    }
}
