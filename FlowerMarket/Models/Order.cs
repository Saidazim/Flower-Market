using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlowerMarket.DataAccess;
using System.ComponentModel.DataAnnotations;
namespace FlowerMarket.Models
{
    public class Order
    {
        public int ID { get; set; }
        [Required]
        public int Flower { get; set; }
        [Required]
        public int Client { get; set; }
        [Required]
        public DateTime Sell_Date { get; set; }
        [Required]
        [Range(0, Int32.MaxValue)]
        public int Quantity { get; set; }
        public double Price { get; set; }

        public Client GetClient
        {
            get
            {
                ClientManager manager = new ClientManager();
                return manager.GetCLientByID(Client);
            }
        }
        public Flower GetFlower
        {
            get
            {
                FlowerManager manager = new FlowerManager();

                return manager.GetFlowers(Flower);
            }
        }
    }
}