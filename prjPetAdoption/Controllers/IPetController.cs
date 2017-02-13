using prjPetAdoption.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace prjPetAdoption.Controllers
{
    public class IPetController : BaseController
    {
        // GET: IPet
        public ActionResult IPET(string city)
        {
            var iPet = db.aniDataPicOne;

            ViewBag.City = this.GetSelectList(this.GetCity(), city);
            ViewBag.SelectedCity = city;

            var source = GetAnimalData();
            source = source.AsQueryable();

            if(!string.IsNullOrWhiteSpace(city))
            {
                source = source.Where(x => x.animalAddress == city);
                
            }
            return View(source.OrderBy(x => x.animalAddress).ToList());
           
        }

        public IEnumerable<SelectListItem> GetSelectList(IEnumerable<string> source, string selectedItem)
        {
            
            var SelectList = source.Select(item => new SelectListItem()
            {
                Text = item,
                Value = item,
                Selected = !string.IsNullOrWhiteSpace(selectedItem)
                    && item.Equals(selectedItem, StringComparison.OrdinalIgnoreCase)
            });
            return SelectList;
        }

        private List<string> GetCity()
        {
            var source = db.aniDataPicOne;
            if (source != null)
            {
                var city = source.OrderBy(x => x.animalAddress)
                                .Select(x => x.animalAddress)
                                .Distinct();
                return city.ToList();
            }
            return new List<string>();
        }

        public IEnumerable<aniDataPicOne> GetAnimalData()
        {
            var source = db.aniDataPicOne;

            return source;
        }


    }
}