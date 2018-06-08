using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowerMarket.Models;
using FlowerMarket.DataAccess;
namespace FlowerMarket.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {

        [HttpPost]
        [Authorize]
        public ActionResult Create(Order model)
        {
            OrderManager manager = new OrderManager();
            manager.New_Order(model);
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            OrderManager manager = new OrderManager();
            
            IList<Order> list = manager.GetOrders();
            return View(list);
        }

        public ActionResult Create(int id)
        {
            Order model = new Order();
            model.Flower = id;
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            OrderManager manager = new OrderManager();
            return View(manager.GetOrder(id));
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(Order model)
        {
            OrderManager manager = new OrderManager();
            manager.updateOrder(model);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            OrderManager manager = new OrderManager();
            manager.Remove_Order(id);
            return RedirectToAction("Index");
        }
    }
}
