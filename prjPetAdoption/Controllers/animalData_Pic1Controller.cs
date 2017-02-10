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
    public class animalData_Pic1Controller : Controller
    {
        private DbAnimal db = new DbAnimal();

        // GET: animalData_Pic1
        public ActionResult Index()
        {
            var animalData_Pic = db.animalData_Pic.Include(a => a.animalData);
            return View(animalData_Pic.ToList());
        }

        // GET: animalData_Pic1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            animalData_Pic animalData_Pic = db.animalData_Pic.Find(id);
            if (animalData_Pic == null)
            {
                return HttpNotFound();
            }
            return View(animalData_Pic);
        }

        // GET: animalData_Pic1/Create
        public ActionResult Create()
        {
            int? intIdt = db.animalData.Max(u => (int?)u.animalID);
            ViewBag.animalID = intIdt;
            return View();
        }

        // POST: animalData_Pic1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "animalPicID,animalPic_animalID,animalPicAddress")] animalData_Pic animalData_Pic)
        {
            if (ModelState.IsValid)
            {
                db.animalData_Pic.Add(animalData_Pic);
                db.SaveChanges();
                return RedirectToAction("showForAdopt","aniData");
            }

            ViewBag.animalPic_animalID = new SelectList(db.animalData, "animalID", "animalKind", animalData_Pic.animalPic_animalID);
            return View(animalData_Pic);
        }

        // GET: animalData_Pic1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            animalData_Pic animalData_Pic = db.animalData_Pic.Find(id);
            if (animalData_Pic == null)
            {
                return HttpNotFound();
            }
            ViewBag.animalPic_animalID = new SelectList(db.animalData, "animalID", "animalKind", animalData_Pic.animalPic_animalID);
            return View(animalData_Pic);
        }

        // POST: animalData_Pic1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "animalPicID,animalPic_animalID,animalPicAddress")] animalData_Pic animalData_Pic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animalData_Pic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.animalPic_animalID = new SelectList(db.animalData, "animalID", "animalKind", animalData_Pic.animalPic_animalID);
            return View(animalData_Pic);
        }

        // GET: animalData_Pic1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            animalData_Pic animalData_Pic = db.animalData_Pic.Find(id);
            if (animalData_Pic == null)
            {
                return HttpNotFound();
            }
            return View(animalData_Pic);
        }

        // POST: animalData_Pic1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            animalData_Pic animalData_Pic = db.animalData_Pic.Find(id);
            db.animalData_Pic.Remove(animalData_Pic);
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
