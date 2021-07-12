using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crypto.Core.Models;

namespace Crypto.Web.Models
{
    public class CurrencyListModel
    {
        public string NameSort { get; set; }
        public string PriceSort { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public IEnumerable<CurrencyTest> CurrencyList { get; set; }
    }
}
