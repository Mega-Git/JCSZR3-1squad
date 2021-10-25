using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crypto.Core.Models;

namespace Crypto.Web.Models
{
    public class CurrencyListModel
    {
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool MinPriceIsValid { get; set; }
        public string CurencyName { get; set; }
        public IEnumerable<CurrencyModel> CurrencyList { get; set; }
        public List<string> PriceChange { get; set; }

        public IEnumerable<CurrencyModel> NewCurrencies { get; set; }
    }
}