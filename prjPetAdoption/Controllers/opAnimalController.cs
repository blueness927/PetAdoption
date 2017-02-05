using Newtonsoft.Json;
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
        string targetURI = "http://data.coa.gov.tw/Service/OpenData/AnimalOpenData.aspx?$top=50";

        public async Task<ActionResult>opAnimalList(string districts, string types)  //篩選後倒資料
        {
            ViewBag.Districts =
            await this.GetSelectList(await this.GetDistricts(), districts);
            ViewBag.SelectedDistrict = districts;

            var typeSelectList =
            await this.GetSelectList(await this.GetType(), types);
            ViewBag.Types = typeSelectList.ToList();
            ViewBag.SelectedType = types;

            var source = await this.GetOPAnimalData();
            source = source.AsQueryable();

            if (!string.IsNullOrWhiteSpace(districts))
            {
                source = source.Where(x => x.animal_area_pkid == districts);
            }
            if (!string.IsNullOrWhiteSpace(types))
            {
                source = source.Where(x => x.animal_kind == types);
            }
            if (source.Count() == 0)
            {
                ViewBag.IMG = "http://i.imgur.com/8P7z9ys.png";
                return View(source.OrderBy(x => x.animal_area_pkid).ToList());
            }
            else
            {
                return View(source.OrderBy(x => x.animal_area_pkid).ToList());
            }
           
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

        public async Task<ActionResult> opAniOne(string id)  //篩選ID取資料
        {
           // targetURI = "http://data.coa.gov.tw/Service/OpenData/AnimalOpenData.aspx?$top=50";
            var  source = await this.GetOPAnimalData();
            source = source.AsQueryable();

            source = source.Where(x => x.animal_id.Equals(id));
            
            return View( source.OrderBy(x => x.animal_area_pkid).ToList());
        }



        private async Task<IEnumerable<OpenData>> RetriveOPAnimalData(string cacheName)  //連線OP DATA
        {
             targetURI = "http://data.coa.gov.tw/Service/OpenData/AnimalOpenData.aspx?$top=50";
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

        private async Task<IEnumerable<SelectListItem>> GetSelectList(IEnumerable<string>source,string selectedItem)
        {
            //List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem { Text = "台北", Value = "2" });
            //items.Add(new SelectListItem { Text = "新北", Value = "3" });
            //items.Add(new SelectListItem { Text = "基隆", Value = "4" });
            //items.Add(new SelectListItem { Text = "宜蘭", Value = "5" });
            //items.Add(new SelectListItem { Text = "桃園", Value = "6" });
            //items.Add(new SelectListItem { Text = "新竹縣", Value = "7" });
            //items.Add(new SelectListItem { Text = "新竹市", Value = "8" });
            //items.Add(new SelectListItem { Text = "苗栗縣", Value = "9" });
            //items.Add(new SelectListItem { Text = "台中", Value = "10" });
            //items.Add(new SelectListItem { Text = "彰化", Value = "11" });
            //items.Add(new SelectListItem { Text = "南投", Value = "12" });
            //items.Add(new SelectListItem { Text = "雲林", Value = "13" });
            //items.Add(new SelectListItem { Text = "嘉義縣", Value = "14" });
            //items.Add(new SelectListItem { Text = "嘉義市", Value = "15" });
            //items.Add(new SelectListItem { Text = "台南市", Value = "16" });
            //items.Add(new SelectListItem { Text = "高雄市", Value = "17" });
            //items.Add(new SelectListItem { Text = "屏東縣", Value = "18" });
            //items.Add(new SelectListItem { Text = "花蓮縣", Value = "19" });
            //items.Add(new SelectListItem { Text = "台東縣", Value = "20" });
            //items.Add(new SelectListItem { Text = "澎湖", Value = "21" });
            //items.Add(new SelectListItem { Text = "金門", Value = "22" });
            //items.Add(new SelectListItem { Text = "連江", Value = "23" });
            //this.ViewData["list"] = items;

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