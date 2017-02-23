using Newtonsoft.Json;
using PagedList;
using prjPetAdoption.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace prjPetAdoption.Controllers
{
    public class opAnimalController : Controller
    {
        string targetURI = "http://data.coa.gov.tw/Service/OpenData/AnimalOpenData.aspx?$top=150";

        public async Task<ActionResult>opAnimalList(int? page, string districts, string types,string sex)  //篩選後倒資料
        {
            ViewBag.Districts =
            await this.GetSelectList(await this.GetDistricts(), districts);
            ViewBag.SelectedDistrict = districts;

            var typeSelectList =
            await this.GetSelectList(await this.GetType(), types);
            ViewBag.Types = typeSelectList.ToList();
            ViewBag.SelectedType = types;


            var sexSelectList =
           await this.GetSelectList(await this.GetSex(), sex);
            ViewBag.Sex = sexSelectList.ToList();
            ViewBag.SelectedType = sex;




            var source = await this.GetOPAnimalData();
            source = source.AsQueryable();

            if (!string.IsNullOrWhiteSpace(districts))
            {
                source = source.Where(x => x.animal_area_pkid == districts);
            }
            if (!string.IsNullOrWhiteSpace(types))
            {
                source = source.Where(x => x.animal_kind== types);
            }

            if (!string.IsNullOrWhiteSpace(sex))
            {
                source = source.Where(x => x.animal_sex == sex);
            }



            if (source.Count() == 0)
            {
                ViewBag.IMG = "http://i.imgur.com/8P7z9ys.png";
                source.OrderBy(x => x.animal_area_pkid).ToList();
            }
            else
            {
                source.OrderBy(x => x.animal_area_pkid).ToList();
            }

            int pageIndex = page ?? 1;
            int pageSize = 12;
            int totalCount = 0;

            totalCount = source.Count();
            source= source.OrderBy(x => x.animal_area_pkid).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var pagedResult = new StaticPagedList<OpenData>(source, pageIndex, pageSize, totalCount);


            return View(pagedResult);

        }

       
        public async Task<IEnumerable<OpenData>> GetOPAnimalData() //一開始取得全DATA
        {
            string cacheName = "OpenData";
            ObjectCache cache = MemoryCache.Default;
            CacheItem cacheContents = cache.GetCacheItem(cacheName);

            if (cacheContents == null)
            {
                return await RetriveOPAnimalData(cacheName);
            }
            else
                return cacheContents.Value as IEnumerable<OpenData>;  
        }

        public async Task<ActionResult> opAniOne(string id)  //篩選ID取單筆資料
        {
           // targetURI = "http://data.coa.gov.tw/Service/OpenData/AnimalOpenData.aspx?";
            var  source = await this.GetOPAnimalData();
            source = source.AsQueryable();

            source = source.Where(x => x.animal_id.Equals(id));

            if (source.Count() == 0)
            {
                ViewBag.IMG = "http://i.imgur.com/8P7z9ys.png";
                source.OrderBy(x => x.animal_area_pkid).ToList();
            }

            return View( source.OrderBy(x => x.animal_area_pkid).ToList());
        }



        private async Task<IEnumerable<OpenData>> RetriveOPAnimalData(string cacheName)  //連線OP DATA
        {
             targetURI = "http://data.coa.gov.tw/Service/OpenData/AnimalOpenData.aspx?$top=150";
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = Int32.MaxValue;
            var response = await client.GetStringAsync(targetURI);
            var collection = JsonConvert.DeserializeObject<IEnumerable<OpenData>>(response);

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now.AddMinutes(30);

            ObjectCache cacheItem = MemoryCache.Default;
            cacheItem.Add(cacheName, collection, policy);
            return collection;
        }

        /// <summary>
        /// 取得縣市ID
        /// </summary>
        /// <returns></returns>
        private async Task<List<string>>GetDistricts()  //縣市分類
        {
            var source = await this.GetOPAnimalData();
            if (source != null)
            {
                var districts = source.OrderBy(x => x.animal_area_pkid)
                                              .Select(x => x.animal_area_pkid)
                                              .Distinct();

                return districts.ToList();
            }
            return new List<string>();
        }

        /// <summary>
        /// 取得類型
        /// </summary>
        /// <returns></returns>

        private async Task<List<string>> GetType()  //動物類型分類
        {
            var source = await this.GetOPAnimalData();
            if (source != null)
            {
                var Type = source.OrderBy(x => x.animal_kind)
                                              .Select(x => x.animal_kind)
                                              .Distinct();

                return Type.ToList();
            }
            return new List<string>();
        }

        private async Task<List<string>> GetSex()  //動物類型分類
        {
            var source = await this.GetOPAnimalData();
            if (source != null)
            {
                var Sex = source.OrderBy(x => x.animal_sex)
                                              .Select(x => x.animal_sex)
                                              .Distinct();

                return Sex.ToList();
            }
            return new List<string>();
        }





        private async Task<IEnumerable<SelectListItem>> GetSelectList(IEnumerable<string>source,string selectedItem)
        {
          
            var selectList = source.Select(item => new SelectListItem()
            {
                Text = item,
                Value = item,
                Selected = !string.IsNullOrWhiteSpace(selectedItem) && item.Equals(selectedItem, StringComparison.OrdinalIgnoreCase)

            });
            return selectList;
        }
    }
}