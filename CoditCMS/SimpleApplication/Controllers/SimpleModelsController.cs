using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleApplication.Models;

namespace SimpleApplication.Controllers
{
    public class SimpleModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SimpleModels
        public ActionResult Index()
        {
            return View(db.SimpleModels.ToList());
        }

        // GET: SimpleModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SimpleModel simpleModel = db.SimpleModels.Find(id);
            if (simpleModel == null)
            {
                return HttpNotFound();
            }
            return View(simpleModel);
        }

        // GET: SimpleModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SimpleModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title")] SimpleModel simpleModel)
        {
            if (ModelState.IsValid)
            {
                db.SimpleModels.Add(simpleModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(simpleModel);
        }

        // GET: SimpleModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SimpleModel simpleModel = db.SimpleModels.Find(id);
            if (simpleModel == null)
            {
                return HttpNotFound();
            }
            return View(simpleModel);
        }

        // POST: SimpleModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title")] SimpleModel simpleModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(simpleModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(simpleModel);
        }

        // GET: SimpleModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SimpleModel simpleModel = db.SimpleModels.Find(id);
            if (simpleModel == null)
            {
                return HttpNotFound();
            }
            return View(simpleModel);
        }

        // POST: SimpleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SimpleModel simpleModel = db.SimpleModels.Find(id);
            db.SimpleModels.Remove(simpleModel);
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
