using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowerMarket.Models
{
    public class Flower
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public double Sell_Price { get; set; }
    }
}