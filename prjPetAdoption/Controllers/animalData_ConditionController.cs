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
    public class animalData_ConditionController : Controller
    {
        private DbAnimal db = new DbAnimal();
        AllAniDataViewModel AllAniData = new AllAniDataViewModel();
        // GET: animalData_Condition
        public ActionResult Index()
        {
            var animalData_Condition = db.animalData_Condition.Include(a => a.animalData);
            return View(animalData_Condition.ToList());
        }

        // GET: animalData_Condition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            animalData_Condition animalData_Condition = db.animalData_Condition.Find(id);
            if (animalData_Condition == null)
            {
                return HttpNotFound();
            }
            return View(animalData_Condition);
        }

        // GET: animalData_Condition/Create
        public ActionResult Create()
        {
            int? intIdt = db.animalData.Max(u => (int?)u.animalID);
            ViewBag.animalID = intIdt;
            return View();
        }

        // POST: animalData_Condition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "conditionID,condition_animalID,conditionAge,conditionEconomy,conditionHome,conditionFamily,conditionReply,conditionPaper,conditionFee,conditionOther")] animalData_Condition animalData_Condition)
        {
            if (ModelState.IsValid)
            {
                db.animalData_Condition.Add(animalData_Condition);
                db.SaveChanges();
                return RedirectToAction("Create", "animalData_Pic1");
            }

            ViewBag.condition_animalID = new SelectList(db.animalData, "animalID", "animalKind", animalData_Condition.condition_animalID);
            return View(animalData_Condition);
        }

        // GET: animalData_Condition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var aniID = db.animalData_Condition.Where(x => x.condition_animalID == id).Select(x => x.conditionID);
            foreach (var intID in aniID)
            { 
                animalData_Condition animalData_Condition = db.animalData_Condition.Find(intID);
            
            if (animalData_Condition == null)
            {
                return HttpNotFound();
            }
            ViewBag.condition_animalID = new SelectList(db.animalData, "animalID", "animalKind", animalData_Condition.condition_animalID);
            return View(animalData_Condition);
            }
            return View();
        }

        // POST: animalData_Condition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "conditionID,condition_animalID,conditionAge,conditionEconomy,conditionHome,conditionFamily,conditionReply,conditionPaper,conditionFee,conditionOther")] animalData_Condition animalData_Condition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animalData_Condition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("picList", "animalData_Pic1", new { id = animalData_Condition.condition_animalID });
            }
            ViewBag.condition_animalID = new SelectList(db.animalData, "animalID", "animalKind", animalData_Condition.condition_animalID);
            return View(animalData_Condition);
        }

        // GET: animalData_Condition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            animalData_Condition animalData_Condition = db.animalData_Condition.Find(id);
            if (animalData_Condition == null)
            {
                return HttpNotFound();
            }
            return View(animalData_Condition);
        }

        // POST: animalData_Condition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            animalData_Condition animalData_Condition = db.animalData_Condition.Find(id);
            db.animalData_Condition.Remove(animalData_Condition);
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
