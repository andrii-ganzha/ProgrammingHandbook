using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NOC_DKRKM.Models;
using NOC_DKRKM.ViewModels;

namespace Diplom_White.Controllers
{
    public class KindsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Kinds
        //public ActionResult Index()
        //{
        //    return View(db.Kinds.ToList());
        //}

        public ActionResult Index(int? id, int? documentId)
        {
            var viewModel = new KindIndexData();

            viewModel.Kinds = db.Kinds;

            if (id != null)
            {
                ViewBag.KindID = id.Value;
                viewModel.Documents = viewModel.Kinds.Where(i => i.KindId == id.Value).Single().Documents;
            }

            return View(viewModel);
        }

        // GET: Kinds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kind kind = db.Kinds.Find(id);
            if (kind == null)
            {
                return HttpNotFound();
            }
            return View(kind);
        }

        // GET: Kinds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kinds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KindId,KindName")] Kind kind)
        {
            if (ModelState.IsValid)
            {
                db.Kinds.Add(kind);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kind);
        }

        // GET: Kinds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kind kind = db.Kinds.Find(id);
            if (kind == null)
            {
                return HttpNotFound();
            }
            return View(kind);
        }

        // POST: Kinds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KindId,KindName")] Kind kind)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kind).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kind);
        }

        // GET: Kinds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kind kind = db.Kinds.Find(id);
            if (kind == null)
            {
                return HttpNotFound();
            }
            return View(kind);
        }

        // POST: Kinds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kind kind = db.Kinds.Find(id);
            db.Kinds.Remove(kind);
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
