using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleApplication.Models;

namespace SimpleApplication.Areas.Test.Controllers
{
    public class AreaModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Test/AreaModels
        public ActionResult Index()
        {
            return View(db.AreaModels.ToList());
        }

        // GET: Test/AreaModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaModel areaModel = db.AreaModels.Find(id);
            if (areaModel == null)
            {
                return HttpNotFound();
            }
            return View(areaModel);
        }

        // GET: Test/AreaModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/AreaModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title")] AreaModel areaModel)
        {
            if (ModelState.IsValid)
            {
                db.AreaModels.Add(areaModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(areaModel);
        }

        // GET: Test/AreaModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaModel areaModel = db.AreaModels.Find(id);
            if (areaModel == null)
            {
                return HttpNotFound();
            }
            return View(areaModel);
        }

        // POST: Test/AreaModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title")] AreaModel areaModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areaModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(areaModel);
        }

        // GET: Test/AreaModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaModel areaModel = db.AreaModels.Find(id);
            if (areaModel == null)
            {
                return HttpNotFound();
            }
            return View(areaModel);
        }

        // POST: Test/AreaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AreaModel areaModel = db.AreaModels.Find(id);
            db.AreaModels.Remove(areaModel);
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
