using Crypto.Core.Models;
using Crypto.Core.Providers;
using Crypto.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList;

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
        private readonly CurrencyContext _context;

        public HomeController(ILogger<HomeController> logger, CurrencyContext context)
        {
            _logger = logger;
            _context = context;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index(string sortColumn, int? page, int pageSize, decimal? minValue, decimal? maxValue,
            string currencyName, string sortDir = "")
        {
            if (pageSize == 0)
            {
                pageSize = 10;
            }
            var pSize = pageSize;
            var pageNumber = page ?? 1;

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
                        DecimalParse(x.Prices.Last()) > minValue && DecimalParse(x.Prices.Last()) < maxValue),
                    >= 0 when maxValue == null =>
                        currencyList.Where(x => DecimalParse(x.Prices.Last()) > minValue),
                    null when maxValue > 0 => currencyList.Where(x => DecimalParse(x.Prices.Last()) < maxValue),
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
            var secondLastPrice = currencyList.Select(x => x.Prices.Count() == 1 ? "0" : x.Prices[^2]).ToList();


            for (int i = 0; i < currencyList.Count(); i++)
            {
                if ( DecimalParse(secondLastPrice[i]) == 0)
                {
                    priceChange.Add(DecimalParse("1").ToString("P", CultureInfo.InvariantCulture));
                }
                else
                {
                    priceChange.Add(((DecimalParse(secondLastPrice[i]) - DecimalParse(lastPrice[i])) /
                                                    DecimalParse(secondLastPrice[i]))
                                       .ToString("P", CultureInfo.InvariantCulture));
                }

            }

            model.SortColumn = sortColumn;
            model.SortDirection = sortDir;
            model.MinPrice = minValue;
            model.MaxPrice = maxValue;
            model.CurencyName = currencyName;
            model.PageSize = pSize;
            model.PageSizeList = new SelectList(new int[] {10, 20, 100});
            model.CurrencyList = currencyList.ToPagedList(pageNumber, pSize);
            model.PagedList = (PagedList<CurrencyModel>)currencyList.ToPagedList(pageNumber, pSize);
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
        
            for (int i = 0; i < JsonFile.CryptoCurrencies.Count(); i++)
            {
                NomicsProvider.GetData()[i].Favorite = listOfFavorite.ToArray()[i].Favorite;
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFavorites(IEnumerable<CurrencyModel> listOfFavorite)
        {
            var deleteFavorite = NomicsProvider.GetData().Where(x => x.Favorite).ToList();

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
            var currencyList = NomicsProvider.GetData().Where(c => c.Favorite);
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
            var currencyList = NomicsProvider.GetData().Select(c => c.Currency);
            var myCurrency = _context.Currency;

            using (_context)
            {
                if (currencyList.Contains(currencyName.ToUpper()) == false &&
                    myCurrency.Any(x => x.Name == currencyName.ToUpper()) == false)
                {
                    var newCurrency = new NewCurrencyModel();
                    {
                        newCurrency.Name = currencyName.ToUpper();
                        newCurrency.Prices = new List<NewCurrencyPricesModel>();
                    }
                    var newCurrencyPrice = new NewCurrencyPricesModel()
                    {
                        Price = currencyPrice,
                        Timestamp = DateTime.Now.ToString()
                    };
                    newCurrency.Prices.Add(newCurrencyPrice);
                    _context.Currency.Add(newCurrency);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("MyCurrencies");
        }

        public IActionResult AddPrice(string currencyPrice, string currencyName)
        {
            using (_context)
            {
                if (currencyName == null)
                {
                    ViewBag.ErrorMessage = "Select currency!";

                }
                else
                {
                    var findCurrency = _context.Currency.First(x => x.Name == currencyName);
                    var currencyID = findCurrency.CurrencyId;

                    var newCurrencyPrice = new NewCurrencyPricesModel()
                    {
                        Price = currencyPrice,
                        Timestamp = DateTime.Now.ToString(),
                        CurrencyId = currencyID
                    };
                    _context.Price.Add(newCurrencyPrice);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("MyCurrencies");
        }

        public IActionResult CurrencyDelete(string currencyDelete)
        {
            using (_context)
            {
                _context.Remove(_context.Currency.First(x => x.Name == currencyDelete));
                _context.SaveChanges();
            }

            return RedirectToAction("MyCurrencies");
        }

        public IActionResult MyCurrencies()
        {
            var currencyList = _context.Currency.Include(x => x.Prices).ToList();

            return View(currencyList);
        }
    }
}