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

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index(string sortOrder)
        {
            var nameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var priceSort = sortOrder == "price" ? "price_desc" : "price";

            IEnumerable<CurrencyTest> currencyList; 


            switch (sortOrder)
            {
                case "name_desc":
                    currencyList = JsonFile.CryptoCurrencies.OrderByDescending(x => x.Currency);
                    break;
                case "price":
                    currencyList = JsonFile.CryptoCurrencies.OrderBy(x => x.Prices.Last());
                    break;
                case "price_desc":
                    currencyList = JsonFile.CryptoCurrencies.OrderByDescending(x => x.Prices.Last());
                    break;
                default:
                    currencyList = JsonFile.CryptoCurrencies.OrderBy(x => x.Currency);
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
        public IActionResult Favorites()    
        {
            List<CurrencyTest> CurrencyListFavorite = new List<CurrencyTest>();

            CurrencyListFavorite.AddRange()



            return View();
        }
    }
}
