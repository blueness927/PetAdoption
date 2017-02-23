using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using prjPetAdoption.Models;

namespace prjPetAdoption.Controllers
{
    public class aniDataPicsController : Controller
    {
        private DbAnimal db = new DbAnimal();

        // GET: aniDataPics
        public ActionResult picMGIndex()
        {
            return View(db.aniDataPic.ToList());
        }

        // GET: aniDataPics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aniDataPic aniDataPic = db.aniDataPic.Find(id);
            if (aniDataPic == null)
            {
                return HttpNotFound();
            }
            return View(aniDataPic);
        }

        // GET: aniDataPics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: aniDataPics/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "animalPicID,animalID,animalName,animalKind,animalType,animalColor,animalGender,animalAddress,animalAge,animalPicAddress,animalOwner_userID")] aniDataPic aniDataPic)
        {
            if (ModelState.IsValid)
            {
                db.aniDataPic.Add(aniDataPic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aniDataPic);
        }

        // GET: aniDataPics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aniDataPic aniDataPic = db.aniDataPic.Find(id);
            if (aniDataPic == null)
            {
                return HttpNotFound();
            }
            return View(aniDataPic);
        }

        // POST: aniDataPics/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "animalPicID,animalID,animalName,animalKind,animalType,animalColor,animalGender,animalAddress,animalAge,animalPicAddress,animalOwner_userID")] aniDataPic aniDataPic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aniDataPic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aniDataPic);
        }

        // GET: aniDataPics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aniDataPic aniDataPic = db.aniDataPic.Find(id);
            if (aniDataPic == null)
            {
                return HttpNotFound();
            }
            return View(aniDataPic);
        }

        // POST: aniDataPics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            aniDataPic aniDataPic = db.aniDataPic.Find(id);
            db.aniDataPic.Remove(aniDataPic);
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
