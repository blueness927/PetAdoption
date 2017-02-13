using prjPetAdoption.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace prjPetAdoption.Controllers
{
    public class AniPicOneController : BaseController
    {
        
        public ActionResult Index()
        {

            var animalSource = this.GetAnimalData();
            ViewBag.Model = animalSource;
            return View();
        }

        private IEnumerable<aniDataPicOne>GetAnimalData()
        {
            var animalData = db.aniDataPicOne.ToList();

            return animalData;

        }

      
    }
}