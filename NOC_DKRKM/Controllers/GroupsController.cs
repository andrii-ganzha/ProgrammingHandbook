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

namespace NOC_DKRKM.Controllers
{
    public class GroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Groups
        //[Authorize(Roles = "Адміністратор,Викладач")]
        public ActionResult Index(int? id)
        {
            //return View(db.Groups.ToList());
            var viewModel = new GroupsIndexData();

            ViewBag.Slash = " | ";

            viewModel.Groups = db.Groups.Include(g=>g.Students);

            if (id != null)
            {
                ViewBag.GroupID = id.Value;
                viewModel.Students = viewModel.Groups.Where(i => i.GroupID == id.Value).Single().Students.OrderBy(i=>i.sname);
                viewModel.GroupsInCource = viewModel.Groups.Where(i => i.GroupID == id.Value).Single().Cources;
            }

            return View(viewModel);
        }

        //GET: Groups/Details/5
        public ActionResult Details(int? id, int? CourceID)
        {
            var viewModel = new GroupsResultsData();
            ViewBag.Group = db.Groups.Find(id).Name;


            if (id == null || CourceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var students = db.Users.Where(u => u.GroupID == id).Include(u=>u.TestResults);
            viewModel.Students = students.Include(u=>u.TestResults).ToList();

            var topics = db.Topics.Where(t => t.CourceID == CourceID);
            viewModel.Topics = topics.ToList();
            



            return View(viewModel);
        }

        // GET: Groups/Create
        [Authorize(Roles = "Адміністратор")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Адміністратор")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,Name")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        [Authorize(Roles = "Адміністратор")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Адміністратор")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,Name")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        [Authorize(Roles = "Адміністратор")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Адміністратор")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
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
