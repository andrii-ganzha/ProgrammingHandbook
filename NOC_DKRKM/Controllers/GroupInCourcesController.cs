using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NOC_DKRKM.Models;

namespace NOC_DKRKM.Controllers
{
    public class GroupInCourcesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GroupInCources
        //public ActionResult Index()
        //{
        //    var groupInCources = db.GroupInCources.Include(g => g.Cource).Include(g => g.Group);
        //    return View(groupInCources.ToList());
        //}

        // GET: GroupInCources/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    GroupInCource groupInCource = db.GroupInCources.Find(id);
        //    if (groupInCource == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(groupInCource);
        //}

        // GET: GroupInCources/Create
        [Authorize(Roles = "Адміністратор")]
        public ActionResult Create()
        {
            ViewBag.CourceID = new SelectList(db.Courses, "CourceID", "Name");
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name");
            return View();
        }

        // POST: GroupInCources/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Адміністратор")]
        public ActionResult Create([Bind(Include = "CourceID,GroupID")] GroupInCource groupInCource)
        {
            if (ModelState.IsValid)
            {
                db.GroupInCources.Add(groupInCource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourceID = new SelectList(db.Courses, "CourceID", "Name", groupInCource.CourceID);
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", groupInCource.GroupID);
            return View(groupInCource);
        }

        // GET: GroupInCources/Edit/5
        [Authorize(Roles = "Адміністратор")]
        public ActionResult Edit(int? GroupId, int? CourceId)
        {
            if (GroupId == null && CourceId==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupInCource groupInCource = db.GroupInCources.Find(CourceId, GroupId);
            //var q_groupInCource = db.GroupInCources.Where(g => g.CourceID == CourceId && g.GroupID == GroupId);
            //GroupInCource groupInCource =;
            if (groupInCource == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourceID = new SelectList(db.Courses, "CourceID", "Name", groupInCource.CourceID);
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", groupInCource.GroupID);
            return View(groupInCource);
        }

        // POST: GroupInCources/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Адміністратор")]
        public ActionResult Edit([Bind(Include = "CourceID,GroupID")] GroupInCource groupInCource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupInCource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourceID = new SelectList(db.Courses, "CourceID", "Name", groupInCource.CourceID);
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", groupInCource.GroupID);
            return View(groupInCource);
        }

        // GET: GroupInCources/Delete/5
        [Authorize(Roles = "Адміністратор")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupInCource groupInCource = db.GroupInCources.Find(id);
            if (groupInCource == null)
            {
                return HttpNotFound();
            }
            return View(groupInCource);
        }

        // POST: GroupInCources/Delete/5
        [Authorize(Roles = "Адміністратор")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupInCource groupInCource = db.GroupInCources.Find(id);
            db.GroupInCources.Remove(groupInCource);
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
