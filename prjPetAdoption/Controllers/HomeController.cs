using prjPetAdoption.Models;
using prjPetAdoption.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjPetAdoption.Controllers
{
    public class HomeController : Controller
    {
        AllAniDataViewModel AllAniData = new AllAniDataViewModel();
        private DbAnimal db = new DbAnimal();

        public ActionResult Index()
        {
            var list = (from t in db.aniDataPicOne
                        orderby t.animalID descending
                        select t).Take(6);

            AllAniData.aniDataPicOneList = list;
            return View(AllAniData);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FAQ()
        {
            ViewBag.Message = "FAQ";

            return View();
        }

        public ActionResult Help()
        {
            ViewBag.Message = "Help";

            return View();
        }

        public ActionResult Information()
        {
            ViewBag.Message = "Information";

            return View();
        }

        public ActionResult Cat()
        {
            ViewBag.Message = "Cat";

            return View();
        }
        public ActionResult Dog()
        {
            ViewBag.Message = "Dog";

            return View();
        }

        public ActionResult Rabbit()
        {
            ViewBag.Message = "Rabbit";

            return View();
        }

        public ActionResult Bird()
        {
            ViewBag.Message = "Bird";

            return View();
        }

        public ActionResult aniwiki()
        {
            ViewBag.Message = "WIKI";

            return View();
        }
    }
}