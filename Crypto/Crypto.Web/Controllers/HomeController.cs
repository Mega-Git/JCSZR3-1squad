using Crypto.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Crypto.Core.Models;

namespace Crypto.Web.Controllers
{
    public class HomeController : Controller
    {
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

        public IActionResult Index(string sortOrder, decimal? minValue, decimal? maxValue)
        {
            var nameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var priceSort = sortOrder == "price" ? "price_desc" : "price";

            var currencyList = JsonFile.CryptoCurrencies.Select(x => x);

            var minPrice = minValue;
            var maxPrice = maxValue;

            if (minPrice != null || maxPrice != null)
            {
                if (minPrice > 0 && maxPrice > minPrice)
                {
                    currencyList = currencyList.Where(x => Convert.ToDecimal(x.Prices.Last()) > minPrice && Convert.ToDecimal(x.Prices.Last()) < maxPrice);
                }
                else if (minPrice > 0 && maxPrice == null)
                {
                    currencyList = currencyList.Where(x => Convert.ToDecimal(x.Prices.Last()) > minPrice);
                }
                else if (minPrice == null && maxPrice > 0)
                {
                    currencyList = currencyList.Where(x => Convert.ToDecimal(x.Prices.Last()) < maxPrice);
                }
            }

            switch (sortOrder)
            {
                case "name_desc":
                    currencyList = currencyList.OrderByDescending(x => x.Currency);
                    break;
                case "price":
                    currencyList = currencyList.OrderBy(x => x.Prices.Last());
                    break;
                case "price_desc":
                    currencyList = currencyList.OrderByDescending(x => x.Prices.Last());
                    break;
                default:
                    currencyList = currencyList.OrderBy(x => x.Currency);
                    break;
            }

            var model = new CurrencyListModel
            {
                NameSort = nameSort,
                PriceSort = priceSort,
                CurrencyList = currencyList
            };

            return View(model);
        }
    }
}
