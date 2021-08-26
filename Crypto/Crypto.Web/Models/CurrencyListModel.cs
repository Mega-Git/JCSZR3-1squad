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
        public IEnumerable<CurrencyTest> CurrencyList { get; set; }
        public List<string> PriceChange { get; set; }
        
        public  IEnumerable<CurrencyTest> NewCurrencies { get; set; }

        

        


        public FavoriteListModel FavoriteList { get; set; }



    }

    public class FavoriteListModel
    {
        public string FavoriteName { get; set; }
        public bool SelectedCheck { get; set; }

    }
}
