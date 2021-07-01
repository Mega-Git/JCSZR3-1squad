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

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index(string sortOrder)
        {
            ViewData["NameSort"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSort"] = sortOrder == "price" ? "price_desc" : "price";

            var currencies = JsonFile.CryptoCurrencies.Select(x => x);

            switch (sortOrder)
            {
                case "name_desc":
                    currencies = currencies.OrderByDescending(x => x.Currency);
                    break;
                case "price":
                    currencies = currencies.OrderBy(x => x.Prices.Last());
                    break;
                case "price_desc":
                    currencies = currencies.OrderByDescending(x => x.Prices.Last());
                    break;
                default:
                    currencies = currencies.OrderBy(x => x.Currency);
                    break;
            }

            return View(currencies);
        }
    }
}
