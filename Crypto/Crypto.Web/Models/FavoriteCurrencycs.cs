using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Web.Models
{
    public class FavoriteCurrencycs
    {

        public string NameSort { get; set; }
        public string PriceSort { get; set; }
        public IEnumerable<CurrencyListModel> CurrencyListsCopy { get; set; }
    }
}
