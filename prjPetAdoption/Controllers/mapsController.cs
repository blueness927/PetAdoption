using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using prjPetAdoption.Models;
using prjPetAdoption.ViewModels;

namespace prjPetAdoption.Controllers
{
    public class mapsController : Controller
    {
        private DbAnimal db = new DbAnimal();
        AllAniDataViewModel AllAniData = new AllAniDataViewModel();

        // GET: maps
        public ActionResult Index()
        {
            return View(db.map.ToList());
        }

        public ActionResult Index2(string id)
        {
            var locat = db.map.Where(x => x.mapType.Equals(id)).ToList();
            AllAniData.mapList = locat;

            return View(AllAniData);
        }


        // GET: maps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            map map = db.map.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }
            return View(map);
        }

        // GET: maps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: maps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mapID,mapType,mapName,maplatitude,maplongitude,mapTitle,mapContent,mapAddressCity,mapAddressTown,mapAddressDetail,mapPic")] map map)
        {
            if (ModelState.IsValid)
            {
                db.map.Add(map);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(map);
        }

        // GET: maps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            map map = db.map.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }
            return View(map);
        }

        // POST: maps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mapID,mapType,mapName,maplatitude,maplongitude,mapTitle,mapContent,mapAddressCity,mapAddressTown,mapAddressDetail,mapPic")] map map)
        {
            if (ModelState.IsValid)
            {
                db.Entry(map).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(map);
        }

        // GET: maps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            map map = db.map.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }
            return View(map);
        }

        // POST: maps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            map map = db.map.Find(id);
            db.map.Remove(map);
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
