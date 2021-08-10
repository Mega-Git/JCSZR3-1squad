using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Web.Models
{
    public class NewCurrency
    {
        public string NwCurrency { get; set; }
        public string[] NwPrices { get; set; }
        public string[] NwTimestamps { get; set; }
    }
}
