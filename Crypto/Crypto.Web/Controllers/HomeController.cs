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
using System.Globalization;

namespace Crypto.Web.Controllers
{
    public class HomeController : Controller
    {
        private static List<CurrencyTest> newcurrencies = new List<CurrencyTest>();
        public const string Descending = "desc";
        public const string Name = "name";
        public const string Price = "price";
        public const string PreviousPrice = "prev_price";
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
            var provider = new CultureInfo("en-US");



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
                case PreviousPrice:
                    currencyList = sortDir == Descending ? currencyList.OrderByDescending(x => x.Prices.Last()) : currencyList.OrderBy(x => x.Prices.Last());
                    break;
                default:
                    currencyList = currencyList.OrderBy(x => x.Currency);
                    break;
            }

            var change = new List<string>();
            var lastPrice = currencyList.Select(x => x.Prices.Last()).ToList();
            var secondLastPrice = currencyList.Select(x => x.Prices[^2]).ToList();

            for (int i = 0; i < currencyList.Count(); i++)
            {
                change.Add(((DecimalParse(secondLastPrice[i]) - DecimalParse(lastPrice[i])) / DecimalParse(secondLastPrice[i]))
                    .ToString("P", CultureInfo.InvariantCulture));
            }



            model.SortColumn = sortColumn;
            model.SortDirection = sortDir;
            model.MinPrice = minValue;
            model.MaxPrice = maxValue;
            model.CurencyName = currencyName;
            model.CurrencyList = currencyList;
            model.PriceChange = change;
            model.NewCurrencies = newcurrencies;

            return View(model);
        }

        private decimal DecimalParse(string number)
        {
            var provider = new CultureInfo("en-US");
            return decimal.Parse(number, provider);
        }
        public IActionResult Favorite(IEnumerable<CurrencyTest> listOfFavorite)
        {
            for (int i = 0; i < JsonFile.CryptoCurrencies.Count; i++)
            {
                JsonFile.CryptoCurrencies[i].Favorite = listOfFavorite.ToArray()[i].Favorite;

            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFavorites(IEnumerable<CurrencyTest> listOfFavorite)
        {
            var deletefavorite = JsonFile.CryptoCurrencies.Where(x =>x.Favorite).ToList();

            for (int i = 0; i < deletefavorite.Count; i++)
            {
                if (listOfFavorite.ToArray()[i].isselected)
                {
                    deletefavorite[i].Favorite = listOfFavorite.ToArray()[i].Favorite;
                }
            }

            return RedirectToAction("FavoriteList");
        }


        public IActionResult FavoriteList()
        {
            var model = new CurrencyListModel();
            var currencylist = JsonFile.CryptoCurrencies.Where(c => c.Favorite);
            model.CurrencyList = currencylist;

            var change = new List<string>();
            var lastPrice = currencylist.Select(x => x.Prices.Last()).ToList();
            var secondLastPrice = currencylist.Select(x => x.Prices[^2]).ToList();

            for (int i = 0; i < currencylist.Count(); i++)
            {
                change.Add(((DecimalParse(secondLastPrice[i]) - DecimalParse(lastPrice[i])) / DecimalParse(secondLastPrice[i]))
                    .ToString("P", CultureInfo.InvariantCulture));
            }

            model.PriceChange = change;

            return View(model);
        }

        public IActionResult AddCurrency(string currencyName, string currencyPrice)
        {
            var currencyList = JsonFile.CryptoCurrencies.Select(c => c.Currency);
            var myCurrency = newcurrencies.Select(x => x.Currency);

            var newCurrency = new CurrencyTest
            {
                Currency = currencyName.ToUpper(),
                Prices = new[] { currencyPrice },
                Timestamps = new[] { DateTime.Now.ToString() }


            };
            if (currencyList.Contains(newCurrency.Currency.ToUpper()) == false && myCurrency.Contains(newCurrency.Currency.ToUpper()) == false) 
            {
                newcurrencies.Add(newCurrency);
            }

            return RedirectToAction("MyCurrencies");
        }

        public IActionResult CurrencyDelete(string currencyDelete)
        {
            newcurrencies.Remove(newcurrencies.FirstOrDefault(t => t.Currency == currencyDelete));


            return RedirectToAction("MyCurrencies");
        }


        public IActionResult MyCurrencies()
        {
            var model = new CurrencyListModel();
            model.NewCurrencies = newcurrencies;


            return View(model);
        }
    }
}
