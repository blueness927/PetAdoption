﻿using prjPetAdoption.Controllers;
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
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }
            return View(animalData);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            animalData animalData = db.animalData.Find(id);
            db.animalData.Remove(animalData);
            db.SaveChanges();
            return RedirectToAction("Index");
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
