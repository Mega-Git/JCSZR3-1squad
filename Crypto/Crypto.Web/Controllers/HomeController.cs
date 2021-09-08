using Crypto.Core.Models;
using Crypto.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Crypto.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<CurrencyModel> Newcurrencies = new();
        public const string Descending = "desc";
        public const string Name = "name";
        public const string Price = "price";
        public const string PreviousPrice = "prev_price";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index(string sortColumn, decimal? minValue, decimal? maxValue,
            string currencyName, string sortDir = "")
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
                currencyList = minValue switch
                {
                    >= 0 when maxValue >= minValue => currencyList.Where(x =>
                        Convert.ToDecimal(x.Prices.Last()) > minValue && Convert.ToDecimal(x.Prices.Last()) < maxValue),
                    >= 0 when maxValue == null =>
                        currencyList.Where(x => Convert.ToDecimal(x.Prices.Last()) > minValue),
                    null when maxValue > 0 => currencyList.Where(x => Convert.ToDecimal(x.Prices.Last()) < maxValue),
                    _ => currencyList
                };
            }

            currencyList = sortColumn switch
            {
                Name => sortDir == Descending
                    ? currencyList.OrderByDescending(x => x.Currency)
                    : currencyList.OrderBy(x => x.Currency),
                Price => sortDir == Descending
                    ? currencyList.OrderByDescending(x => x.Prices.Last())
                    : currencyList.OrderBy(x => x.Prices.Last()),
                PreviousPrice => sortDir == Descending
                    ? currencyList.OrderByDescending(x => x.Prices.Last())
                    : currencyList.OrderBy(x => x.Prices.Last()),
                _ => currencyList.OrderBy(x => x.Currency),
            };

            var priceChange = new List<string>();
            var lastPrice = currencyList.Select(x => x.Prices.Last()).ToList();
            var secondLastPrice = currencyList.Select(x => x.Prices[^2]).ToList();

            for (int i = 0; i < currencyList.Count(); i++)
            {
                priceChange.Add(((DecimalParse(secondLastPrice[i]) - DecimalParse(lastPrice[i])) /
                                 DecimalParse(secondLastPrice[i]))
                    .ToString("P", CultureInfo.InvariantCulture));
            }

            model.SortColumn = sortColumn;
            model.SortDirection = sortDir;
            model.MinPrice = minValue;
            model.MaxPrice = maxValue;
            model.CurencyName = currencyName;
            model.CurrencyList = currencyList;
            model.PriceChange = priceChange;
            model.NewCurrencies = Newcurrencies;

            return View(model);
        }

        private static decimal DecimalParse(string number)
        {
            var provider = new CultureInfo("en-US");
            return decimal.Parse(number, provider);
        }

        public IActionResult Favorite(IEnumerable<CurrencyModel> listOfFavorite)
        {
            for (int i = 0; i < JsonFile.CryptoCurrencies.Count; i++)
            {
                JsonFile.CryptoCurrencies[i].Favorite = listOfFavorite.ToArray()[i].Favorite;
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFavorites(IEnumerable<CurrencyModel> listOfFavorite)
        {
            var deleteFavorite = JsonFile.CryptoCurrencies.Where(x => x.Favorite).ToList();

            for (int i = 0; i < deleteFavorite.Count; i++)
            {
                if (listOfFavorite.ToArray()[i].IsSelected)
                {
                    deleteFavorite[i].Favorite = listOfFavorite.ToArray()[i].Favorite;
                }
            }

            return RedirectToAction("FavoriteList");
        }


        public IActionResult FavoriteList()
        {
            var model = new CurrencyListModel();
            var currencyList = JsonFile.CryptoCurrencies.Where(c => c.Favorite);
            model.CurrencyList = currencyList;

            var priceChange = new List<string>();
            var lastPrice = currencyList.Select(x => x.Prices.Last()).ToList();
            var secondLastPrice = currencyList.Select(x => x.Prices[^2]).ToList();

            for (int i = 0; i < currencyList.Count(); i++)
            {
                priceChange.Add(((DecimalParse(secondLastPrice[i]) - DecimalParse(lastPrice[i])) /
                                 DecimalParse(secondLastPrice[i]))
                    .ToString("P", CultureInfo.InvariantCulture));
            }

            model.PriceChange = priceChange;

            return View(model);
        }

        public IActionResult AddCurrency(string currencyName, string currencyPrice)
        {
            var context = new CurrencyContext();
        

            
            var currencyList = JsonFile.CryptoCurrencies.Select(c => c.Currency);
            var myCurrency = context.Currency;

            using (context) {
                var newCurrency = new NewCurrencyModel();
                {
                    newCurrency.Name = currencyName.ToUpper();
                    newCurrency.Prices = new NewCurrencyPricesModel {Price =  currencyPrice},
                //newCurrency.Timestamps = new[] { DateTime.Now.ToString() }

                }
            };

            using (context) { 
            
            
            
            }
            if (currencyList.Contains(newCurrency.Currency.ToUpper()) == false &&
                myCurrency.Contains(newCurrency.Currency.ToUpper()) == false)
            {
                Newcurrencies.Add(newCurrency);
            }

            return RedirectToAction("MyCurrencies");
        }

        public IActionResult CurrencyDelete(string currencyDelete)
        {
            Newcurrencies.Remove(Newcurrencies.FirstOrDefault(t => t.Currency == currencyDelete));


            return RedirectToAction("MyCurrencies");
        }
        
        public IActionResult MyCurrencies()
        {
            var context = new CurrencyContext();

            var currencyList = context.Currency.Include(x => x.Name).Include(x => x.Prices).ThenInclude(x => x.Price).Include(x => x.Timestamps).ThenInclude(x => x.Timestamp);

            //var model = new CurrencyListModel { NewCurrencies = Newcurrencies };

            return View(context);
        }
    }
}