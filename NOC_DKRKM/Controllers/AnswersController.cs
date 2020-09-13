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
    public class AnswersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Answers
        //public ActionResult Index()
        //{
        //    var answers = db.Answers.Include(a => a.Question);
        //    return View(answers.ToList());
        //}

        // GET: Answers/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Answer answer = db.Answers.Find(id);
        //    if (answer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(answer);
        //}

        // GET: Answers/Create
        [Authorize(Roles = "Викладач")]
        public ActionResult Create()
        {
            int selectedIndex = 1;
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Text", selectedIndex);
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Name");
            ViewBag.CourceID = new SelectList(db.Courses, "CourceID", "Name");
            return View();
        }

        public ActionResult GetTopics(int id)
        {
            return PartialView(db.Topics.Where(t => t.CourceID == id).ToList());
        }

        public ActionResult GetQuestions(int id)
        {
            return PartialView(db.Questions.Where(t => t.TopicID == id).ToList());
        }

        // POST: Answers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Викладач")]
        public ActionResult Create([Bind(Include = "AnswerID,Text,QuestionID,value")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Text", answer.QuestionID);
            return View(answer);
        }

        // GET: Answers/Edit/5
        [Authorize(Roles = "Викладач")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Text", answer.QuestionID);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Викладач")]
        public ActionResult Edit([Bind(Include = "AnswerID,Text,QuestionID,value")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Text", answer.QuestionID);
            return View(answer);
        }

        // GET: Answers/Delete/5
        [Authorize(Roles = "Викладач")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Викладач")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = db.Answers.Find(id);
            db.Answers.Remove(answer);
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
