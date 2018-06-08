using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowerMarket.DataAccess;
using FlowerMarket.Models;

namespace FlowerMarket.Controllers
{
    [Authorize]
    public class FlowerController : Controller
    {

        public ActionResult Index(string sortField)
        {
            IList<Flower> list = new List<Flower>();
            FlowerManager manager = new FlowerManager();
            list = manager.GetFlower(sortField);
            ViewBag.sortFieldName = sortField == "Name"
                ? "Name_desc"
                : "Name";
            ViewBag.sortFieldCountry = sortField == "Country"
                ? "Country_desc"
                : "Country";
            ViewBag.sortFieldSell_Price = sortField == "Sell_Price"
                ? "Price_desc"
                : "Price";
            return View(list);
        }
        [HttpPost]
        public ActionResult Index(string sortField, string query)
        {
            FlowerManager manager = new FlowerManager();
            return View(manager.GetFlower(sortField, query));
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
          
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            return View();
        }

  
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
           

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search(string name = "", string country = "", string Sell_Price = "") {
            FlowerManager manager = new FlowerManager();
            return View("Index", (manager.FlowerFilt(name, country, Sell_Price)));
        }

        public ActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
