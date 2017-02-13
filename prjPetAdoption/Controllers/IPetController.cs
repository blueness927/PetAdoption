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
        public ActionResult iPetInformation(int? page,string city,string type,string kind)
        {
            var iPet = db.aniDataPicOne;

            //縣市查詢
            ViewBag.City = this.GetSelectList(this.GetCity(), city);
            ViewBag.SelectedCity = city;

            //種類查詢
            var typeSelectList =GetSelectList(this.GetType(), type);
            ViewBag.Type = typeSelectList.ToList();
            ViewBag.SelectedType = type;

            //品種查詢
            var kindSelectList = GetSelectList(this.GetKind(), kind);
            ViewBag.Kind = typeSelectList.ToList();
            ViewBag.SelectedKind = kind;



            var source = GetAnimalData();
            source = source.AsQueryable();

            if(!string.IsNullOrWhiteSpace(city))
            {
                source = source.Where(x => x.animalAddress == city); 
            }
            if (!string.IsNullOrWhiteSpace(type))
            {
                source = source.Where(x => x.animalType == type);
            }
              if (!string.IsNullOrWhiteSpace(kind))
            {
                source = source.Where(x => x.animalKind == kind);
            }




            int pageIndex = page ?? 1;
            int pageSize = 9;
            int totalCount = 0;

            totalCount = source.Count();

            source = source.OrderBy(x => x.animalAddress)
                           .Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize);

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


        private List<string> GetType()
        {
            var source = db.aniDataPicOne;
            if (source != null)
            {
                var Type = source.OrderBy(x => x.animalType)
                                .Select(x => x.animalType)
                                .Distinct();
                return Type.ToList();
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