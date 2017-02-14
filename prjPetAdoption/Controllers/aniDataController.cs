using prjPetAdoption.Models;
using prjPetAdoption.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace prjPetAdoption.Controllers
{
    public class aniDataController : Controller
    {
        private DbAnimal db = new DbAnimal();
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

        //追蹤清單明細
        public ActionResult followAni(string id)
        {
            var followAni = db.followAni.Where(x => x.follow_userId == id).ToList();         
            AllAniData.followAniList = followAni;

            var followAniCount = db.followAni.Where(x => x.follow_userId == id).ToList().Count();
            if (followAniCount == 0)
            {
                ViewBag.noFollow = "http://i.imgur.com/4Ooj5kV.png";
            }
            
            return View(AllAniData);
        }
        //追蹤清單刪除
        public ActionResult DelfollowAni(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            follow follow = db.follow.Find(id);
            if (follow == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index", "Manage");
        }
        //追蹤清單刪除

        [HttpDelete]
        public ActionResult DelfollowAniSure(int id)
        {
            follow follow = db.follow.Find(id);
            db.follow.Remove(follow);
            db.SaveChanges();
            return RedirectToAction("Index", "Manage");
        }


        public ActionResult showForAdopt(string id)   //顯示送養筆數
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
            var board = db.boardUser.Where(x => x.board_animalID == id).ToList();
            var Data = db.animalData.Where(x => x.animalID == id).ToList();
            var condi = db.animalData_Condition.Where(x => x.condition_animalID == id).ToList();

            AllAniData.animalData_PicList = aniData;
            AllAniData.aniDataPicOneList = myanimalPic;
            AllAniData.boardUserList = board;
            AllAniData.animalDataList = Data;
            AllAniData.animalData_ConList = condi;
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



        public ActionResult BoardCreate()
        {
            return RedirectToAction("oneAni", "aniData"/*, new { id = board.board_animalID }*/);
        }

        // POST: boards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BoardCreate([Bind(Include = "boardID,boardTime,board_userID,board_animalID,boardContent")] board board)
        {
            if (ModelState.IsValid)
            {
                db.board.Add(board);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("oneAni", "aniData", new { id = board.board_animalID });
                }
                catch (Exception e)
                {
                    return RedirectToAction("oneAni", "aniData", new { id = board.board_animalID });
                }
                //return View();  
                
            }

            ViewBag.board_animalID = new SelectList(db.animalData, "animalID", "animalKind", board.board_animalID);
            ViewBag.board_userID = new SelectList(db.AspNetUsers, "Id", "Email", board.board_userID);
            return RedirectToAction("oneAni", "aniData", new { id = board.board_animalID });
        }
        


        public ActionResult followCreate()
        {

            return View();
        }

        // POST: follows/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult followCreate([Bind(Include = "followID,follow_userId,follow_animalID")] follow follow)
        {
            if (ModelState.IsValid)
            {
                db.follow.Add(follow);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("oneAni", "aniData", new { id = follow.follow_animalID });
                }
                catch (Exception e)
                {
                    return RedirectToAction("oneAni", "aniData", new { id = follow.follow_animalID });
                }

                
            }

            ViewBag.follow_animalID = new SelectList(db.animalData, "animalID", "animalKind", follow.follow_animalID);
            ViewBag.follow_userId = new SelectList(db.AspNetUsers, "Id", "Email", follow.follow_userId);
            return RedirectToAction("oneAni", "aniData", new { id = follow.follow_animalID });
        }



        // GET: Msgs/Create
        public ActionResult MsgCreate()
        {
            return View();
        }

        // POST: Msgs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MsgCreate([Bind(Include = "msgID,msgTime,msgFrom_userID,msgTo_userID,msgType,msgFromURL,msgContent,msgRead")] Msg msg)
        {
            if (ModelState.IsValid)
            {
                db.Msg.Add(msg);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("oneAni", "aniData", new { id = msg.msgFromURL });
                }
                catch (Exception e)
                {
                    return RedirectToAction("oneAni", "aniData", new { id = msg.msgFromURL });
                }
            }


            return View();
        }




    }
}