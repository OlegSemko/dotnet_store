using System;
namespace MyTestApp.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string SellProductIds { get; set; }
        public string QuantitiesSold { get; set; }
        public DateTime SoldAt { get; set; }
    }
}
