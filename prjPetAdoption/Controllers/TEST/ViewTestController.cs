using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjPetAdoption.Controllers.TEST
{
    public class ViewTestController : Controller
    {
        // GET: ViewTest
        public ActionResult Index()  //INDEX頁面名稱
        {
            return View();
        }
    }
}