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
    public class boardsController : Controller
    {
        private DbAnimal db = new DbAnimal();

        // GET: boards
        public ActionResult Index()
        {
            var board = db.board.Include(b => b.animalData).Include(b => b.AspNetUsers);
            return View(board.ToList());
        }

        // GET: boards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            board board = db.board.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }

        // GET: boards/Create
        public ActionResult Create()
        {
            ViewBag.board_animalID = new SelectList(db.animalData, "animalID", "animalKind");
            ViewBag.board_userID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: boards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "boardID,boardTime,board_userID,board_animalID,boardContent")] board board)
        {
            if (ModelState.IsValid)
            {
                db.board.Add(board);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.board_animalID = new SelectList(db.animalData, "animalID", "animalKind", board.board_animalID);
            ViewBag.board_userID = new SelectList(db.AspNetUsers, "Id", "Email", board.board_userID);
            return View(board);
        }

        // GET: boards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            board board = db.board.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            ViewBag.board_animalID = new SelectList(db.animalData, "animalID", "animalKind", board.board_animalID);
            ViewBag.board_userID = new SelectList(db.AspNetUsers, "Id", "Email", board.board_userID);
            return View(board);
        }

        // POST: boards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "boardID,boardTime,board_userID,board_animalID,boardContent")] board board)
        {
            if (ModelState.IsValid)
            {
                db.Entry(board).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.board_animalID = new SelectList(db.animalData, "animalID", "animalKind", board.board_animalID);
            ViewBag.board_userID = new SelectList(db.AspNetUsers, "Id", "Email", board.board_userID);
            return View(board);
        }

        // GET: boards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            board board = db.board.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }

        // POST: boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            board board = db.board.Find(id);
            db.board.Remove(board);
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
