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
    public class followsController : Controller
    {
        private DbAnimal db = new DbAnimal();

        // GET: follows
        public ActionResult Index()
        {
            var follow = db.follow.Include(f => f.animalData).Include(f => f.AspNetUsers);
            return View(follow.ToList());
        }

        // GET: follows/Details/5
        public ActionResult Details(int? id)
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
            return View(follow);
        }

        // GET: follows/Create
        public ActionResult Create()
        {
            ViewBag.follow_animalID = new SelectList(db.animalData, "animalID", "animalKind");
            ViewBag.follow_userId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: follows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "followID,follow_userId,follow_animalID")] follow follow)
        {
            if (ModelState.IsValid)
            {
                db.follow.Add(follow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.follow_animalID = new SelectList(db.animalData, "animalID", "animalKind", follow.follow_animalID);
            ViewBag.follow_userId = new SelectList(db.AspNetUsers, "Id", "Email", follow.follow_userId);
            return View(follow);
        }

        // GET: follows/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.follow_animalID = new SelectList(db.animalData, "animalID", "animalKind", follow.follow_animalID);
            ViewBag.follow_userId = new SelectList(db.AspNetUsers, "Id", "Email", follow.follow_userId);
            return View(follow);
        }

        // POST: follows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "followID,follow_userId,follow_animalID")] follow follow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(follow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.follow_animalID = new SelectList(db.animalData, "animalID", "animalKind", follow.follow_animalID);
            ViewBag.follow_userId = new SelectList(db.AspNetUsers, "Id", "Email", follow.follow_userId);
            return View(follow);
        }

        // GET: follows/Delete/5
        public ActionResult Delete(int? id)
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
            return View(follow);
        }

        // POST: follows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            follow follow = db.follow.Find(id);
            db.follow.Remove(follow);
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
