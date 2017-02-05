using prjPetAdoption.Models;
using prjPetAdoption.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjPetAdoption.Controllers
{
    public class aniDataController : Controller
    {
        private petstationEntities1 db = new petstationEntities1();
        AllAniDataViewModel AllAniData= new AllAniDataViewModel();
        // GET: aniData
        public ActionResult Index()
        {
            var aniData = db.animalData.ToList();
            var aniDataPic = db.animalData_Pic.ToList();

            AllAniData.animalDataList = aniData;
            AllAniData.animalData_PicList = aniDataPic;

            return View(AllAniData);
        }

        public ActionResult showForAdopt(string id)   //顯示送養比數
        {           
            var aniData = db.aniDataAll.Where(x=> x.animalOwner_userID==id).ToList();
            var myanimal = db.aniDataPicOne.Where(x => x.animalOwner_userID.Equals(id)).ToList();               
            AllAniData.aniDataAllList = aniData;
            AllAniData.aniDataPicOneList = myanimal;
                                        
           return View(AllAniData);
            
        }

        public ActionResult oneAni(int? id)   //顯示送養寵物明細資料
        {
            var aniData = db.animalData_Pic.Where(x => x.animalPic_animalID == id).ToList();
            var myanimalPic = db.aniDataPicOne.Where(x => x.animalID == id).ToList();
            AllAniData.animalData_PicList = aniData;
            AllAniData.aniDataPicOneList = myanimalPic;

            return View(AllAniData);

        }


        public ActionResult showForAni()   //顯示待認養清單
        {

            var myanimal = db.aniDataPicOne.ToList();
            AllAniData.aniDataPicOneList = myanimal;

            var myanimal2 = db.aniDataPic.ToList();
            AllAniData.aniDataPicList = myanimal2;

            return View(AllAniData);
        }

        public ActionResult show()   //顯示待認養清單
        {

            return View();
        }



        public ActionResult Create()
        {
            return View();
        }

        // POST: aniDataAlls/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "animalID,animalKind,animalType,animalAge,animalColor,animalDate,animalChip,animalAddress,animalGender,animalHealthy,animalName,animalNote,animalReason,animalOwner_userID")] animalData animalData)
        {
            if (ModelState.IsValid)
            {
                db.animalData.Add(animalData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(AllAniData);
        }




    }
}