namespace Crypto.Core.Models
{
    public class CurrencyModel
    {
        public string Currency { get; set; }
        public string[] Prices { get; set; }
        public string[] Timestamps { get; set; }
        public bool Favorite { get; set; }
        public bool IsSelected { get; set; }

    }
}
