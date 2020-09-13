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
    public class TopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: Topics
        //public ActionResult Index()
        //{
        //    var topics = db.Topics.Include(t => t.Cource);
        //    return View(topics.ToList());
        //}

        // GET: Topics/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Topic topic = db.Topics.Find(id);
        //    if (topic == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(topic);
        //}

        // GET: Topics/Create
        [Authorize(Roles = "Викладач")]
        public ActionResult Create()
        {
            ViewBag.CourceID = new SelectList(db.Courses, "CourceID", "Name");
            return View();
        }

        // POST: Topics/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Викладач")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TopicID,Name,CourceID")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourceID = new SelectList(db.Courses, "CourceID", "Name", topic.CourceID);
            return View(topic);
        }

        // GET: Topics/Edit/5
        [Authorize(Roles = "Викладач")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourceID = new SelectList(db.Courses, "CourceID", "Name", topic.CourceID);
            return View(topic);
        }

        // POST: Topics/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Викладач")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TopicID,Name,CourceID")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourceID = new SelectList(db.Courses, "CourceID", "Name", topic.CourceID);
            return View(topic);
        }

        // GET: Topics/Delete/5
        [Authorize(Roles = "Викладач")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Викладач")]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
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
