using Microsoft.AspNet.Identity;
using prjPetAdoption.Controllers;
using prjPetAdoption.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace prjPetAdoption.Models
{
    public class animalDatasController : BaseController
    {

       
        AllAniDataViewModel AllAniData = new AllAniDataViewModel();

        // GET: animalDatas
        public ActionResult Index()
        {
            return View(db.animalData.ToList());
        }

        // GET: animalDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            animalData animalData = db.animalData.Find(id);
            if (animalData == null)
            {
                return HttpNotFound();
            }
            return View(animalData);
        }

        // GET: animalDatas/Create
        public ActionResult Create()
        {
            //AllAniData.animalData_Type = db.animalData_Type.ToList;

            return View();
        }

        // POST: animalDatas/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "animalID,animalKind,animalType,animalName,animalAddress,animalDate,animalGender,animalAge,animalColor,animalBirth,animalChip,animalHealthy,animalDisease_Other,animalOwner_userID,animalReason,animalGetter_userID,animalAdopted,animalAdoptedDate,animalNote")] animalData animalData)
        {
            if (ModelState.IsValid)
            {
                
                db.animalData.Add(animalData);
                db.SaveChanges();
                return RedirectToAction("Create", "animalData_Condition");
            }

            return View(animalData);
        }

        // GET: animalDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            animalData animalData = db.animalData.Find(id);
            if (animalData == null)
            {
                return HttpNotFound();
            }
            return View(animalData);
        }

        // POST: animalDatas/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "animalID,animalKind,animalType,animalName,animalAddress,animalDate,animalGender,animalAge,animalColor,animalBirth,animalChip,animalHealthy,animalDisease_Other,animalOwner_userID,animalReason,animalGetter_userID,animalAdopted,animalAdoptedDate,animalNote")] animalData animalData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animalData).State = EntityState.Modified;
                db.SaveChanges();
                Session["EditAID"] = animalData.animalID;
                return RedirectToAction("Edit", "animalData_Condition",new { id = animalData.animalID });
            }
            return View(animalData);
        }



        //編輯送養狀態
        public ActionResult EditAdopt(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            animalData animalData = db.animalData.Find(id);
            if (animalData == null)
            {
                return HttpNotFound();
            }
            return PartialView(animalData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdopt([Bind(Include = "animalID,animalKind,animalType,animalName,animalAddress,animalDate,animalGender,animalAge,animalColor,animalBirth,animalChip,animalHealthy,animalDisease_Other,animalOwner_userID,animalReason,animalGetter_userID,animalAdopted,animalAdoptedDate,animalNote")] animalData animalData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animalData).State = EntityState.Modified;
                db.SaveChanges();
                Session["EditAID"] = animalData.animalID;
                return RedirectToAction("showForAdopt_part", "aniData", new { id = @User.Identity.GetUserId() ,title="showForAdopt_part" });
            }
            return PartialView(animalData);
        }

        // GET: animalDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            animalData animalData = db.animalData.Find(id);
            if (animalData == null)
            {
                return HttpNotFound();
            }
            return View(animalData);
        }

        // POST: animalDatas/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            animalData animalData = db.animalData.Find(id);
            db.animalData.Remove(animalData);
            db.SaveChanges();
            return RedirectToAction("Index", "Manage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
