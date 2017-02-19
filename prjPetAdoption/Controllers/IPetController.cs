using PagedList;
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
    public class iPetController : BaseController
    {
        // GET: IPet
        public ActionResult iPetInformation(int? page,string city,string types,string kind)
        {
            var iPet = db.aniDataPicOne;

            //縣市查詢
            ViewBag.City = this.GetSelectList(this.GetCity(), city);
            ViewBag.SelectedCity = city;

            //種類查詢
            var typeSelectList =GetSelectList(this.GetTypes(), types);
            ViewBag.Types = typeSelectList.ToList();
            ViewBag.SelectedType = types;

            //品種查詢
            var kindSelectList = GetSelectList(this.GetKind(), kind);
            ViewBag.Kind = kindSelectList.ToList();
            ViewBag.SelectedKind = kind;

            var source = GetAnimalData();
            source = source.AsQueryable();

            if(!string.IsNullOrWhiteSpace(city))
            {
                source = source.Where(x => x.animalAddress == city); 
            }
            if (!string.IsNullOrWhiteSpace(types))
            {
                source = source.Where(x => x.animalType == types);
            }
              if (!string.IsNullOrWhiteSpace(kind))
            {
               
            }

            int pageIndex = page ?? 1;
            int pageSize = 15;
            int totalCount = 0;

            totalCount = source.Count();

            source = source.OrderByDescending(x => x.animalID)
                           .Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize)
;

            var pagedResult = new StaticPagedList<aniDataPicOne>(source, pageIndex, pageSize, totalCount);

            return View(pagedResult);

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

        private List<string> GetKind()
        {
            var source = db.aniDataPicOne;
            if (source != null)
            {
                var kind = source.OrderBy(x => x.animalKind)
                                .Select(x => x.animalKind)
                                .Distinct();
                return kind.ToList();
            }
            return new List<string>();
        }


        private List<string> GetTypes()
        {
            var source = db.aniDataPicOne;
            if (source != null)
            {
                var Types = source.OrderBy(x => x.animalType)
                                .Select(x => x.animalType)
                                .Distinct();
                return Types.ToList();
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