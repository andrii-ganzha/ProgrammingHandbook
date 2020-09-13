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
using Microsoft.AspNet.Identity;

namespace NOC_DKRKM.Controllers
{
    public class CourcesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cources
        [Authorize]
        public ActionResult Index(int? id, int? TopicID, int? QuestionID)
        {
            //return View(db.Courses.ToList());
            ViewBag.Slash = " | ";
            var viewModel = new CourseIndexData();
            viewModel.Courses = db.Courses;

            if (User.IsInRole("Адміністратор") || User.IsInRole("Викладач"))
            {
                //viewModel.Questions = db.Questions;
                if (id != null)
                    ViewBag.id = id.Value;
                if (TopicID != null)
                    ViewBag.TopicID = TopicID.Value;
                if (QuestionID != null)
                    ViewBag.QestionID = QuestionID.Value;

                if (id != null)
                {
                    ViewBag.CourseID = id.Value;
                    viewModel.Topics = viewModel.Courses.Where(i => i.CourceID == id.Value).Single().Topics;
                }

                if (TopicID != null)
                {
                    ViewBag.TopicID = TopicID.Value;
                    var selectedTopic_q = viewModel.Topics.Where(x => x.TopicID == TopicID).Single().Questions;
                    viewModel.Questions = selectedTopic_q;

                    ViewBag.TopicID = TopicID.Value;
                    var selectedTopic_d = viewModel.Topics.Where(t => t.TopicID == TopicID).Single().Documents;
                    viewModel.Documents = selectedTopic_d;
                }

                if (QuestionID != null)
                {
                    ViewBag.QuestionID = QuestionID.Value;
                    var selectedQuestion = viewModel.Questions.Where(x => x.QuestionID == QuestionID).Single().Answers;
                    viewModel.Answers = selectedQuestion;
                }
            }
            else
            {
                var UserID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                var q_Cources = from c in db.Courses
                                join gc in db.GroupInCources on c.CourceID equals gc.CourceID
                                join g in db.Groups on gc.GroupID equals g.GroupID
                                join s in db.Users on g.GroupID equals s.GroupID
                                where s.Id == UserID
                                select c;
                var Cources = q_Cources.ToList().OrderBy(c=>c.Name);
                viewModel.Courses = Cources;

                if (id != null)
                {
                    ViewBag.CourseID = id.Value;
                    viewModel.Topics = viewModel.Courses.Where(i => i.CourceID == id.Value).Single().Topics;
                }

                if(TopicID != null)
                {
                    ViewBag.TopicID = TopicID.Value;
                    var selectedTopic = viewModel.Topics.Where(t => t.TopicID == TopicID).Single().Documents;
                    viewModel.Documents = selectedTopic;
                }
            }

            return View(viewModel);
        }

        // GET: Cources/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cource cource = db.Courses.Find(id);
            if (cource == null)
            {
                return HttpNotFound();
            }
            return View(cource);
        }

        // GET: Cources/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cources/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Адміністратор")]
        public ActionResult Create([Bind(Include = "CourceID,Name,Describe")] Cource cource)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(cource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cource);
        }

        // GET: Cources/Edit/5
        [Authorize(Roles = "Адміністратор")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cource cource = db.Courses.Find(id);
            if (cource == null)
            {
                return HttpNotFound();
            }
            return View(cource);
        }

        // POST: Cources/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Адміністратор")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourceID,Name,Describe")] Cource cource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cource);
        }

        // GET: Cources/Delete/5
        [Authorize(Roles = "Адміністратор")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cource cource = db.Courses.Find(id);
            if (cource == null)
            {
                return HttpNotFound();
            }
            return View(cource);
        }

        // POST: Cources/Delete/5
        [Authorize(Roles = "Адміністратор,Викладач")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cource cource = db.Courses.Find(id);
            db.Courses.Remove(cource);
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
