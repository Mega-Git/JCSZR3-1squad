using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Web.Models
{
    public class NewCurrencyPricesModel
    {
        [Key]
        public int PriceId { get; set; }
        public string Price { get; set; }
        public string Timestamp { get; set; }
        public int CurrencyId { get; set; }
        public NewCurrencyModel Currency { get; set; }
    }
}