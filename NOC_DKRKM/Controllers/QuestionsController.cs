using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NOC_DKRKM.Models;
using Microsoft.AspNet.Identity;
using System.IO;

namespace NOC_DKRKM.Controllers
{
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        //public ActionResult Index()
        //{
        //    var questions = db.Questions.Include(q => q.Topic);
        //    return View(questions.ToList());
        //}

        // GET: Questions/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Question question = db.Questions.Find(id);
        //    if (question == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(question);
        //}

        // GET: Questions/Create
        [Authorize(Roles = "Викладач")]
        public ActionResult Create()
        {
            int selectedIndex = 1;
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Name", selectedIndex);
            ViewBag.CourceID = new SelectList(db.Courses, "CourceID", "Name");
            return View();
        }

        public ActionResult GetItems (int id)
        {
            return PartialView(db.Topics.Where(t => t.CourceID == id).ToList());
        }

        // POST: Questions/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Викладач")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionID,Text,Code,TopicID,Check")] Question question, HttpPostedFileBase uploadImage)
        {
            if ((uploadImage.ContentType != "image/gif") && (uploadImage.ContentType != "image/jpeg") && (uploadImage.ContentType != "image/png"))
            {
                ModelState.AddModelError("Image", "Не коректний тип файлу. Підтримуються: *.gif, *.jpg, *.png!");
            }
            if(uploadImage.ContentLength > 1000000)
            {
                ModelState.AddModelError("Image", "Завеликий файл! Підтримуються файли до 1МБ.");
            }
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    question.Image = imageData;
                }

                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourceID = new SelectList(db.Courses, "CourceID", "Name");
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Name", question.TopicID);
            return View(question);
        }

        // GET: Questions/Edit/5
        [Authorize(Roles = "Викладач")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Name", question.TopicID);
            ViewBag.CourceID = new SelectList(db.Courses, "CourceID", "Name");
            return View(question);
        }

        // POST: Questions/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Викладач")]
        public ActionResult Edit([Bind(Include = "QuestionID,Text,Code,TopicID,Check")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Name", question.TopicID);
            return View(question);
        }

        // GET: Questions/Delete/5
        [Authorize(Roles = "Викладач")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [Authorize(Roles = "Викладач")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Questions
        [Authorize(Roles = "Студент")]
        public ActionResult BeginTest(int? TopicID)
        {
            ViewBag.i = 1;
            var questions = db.Questions.Include(q => q.Topic)
                .Where(q => q.TopicID == TopicID);
            ViewBag.answers = new int[questions.Count()];
            ViewBag.count = questions.Count();
            ViewBag.TopicID = TopicID;
            return View(questions.ToList());
        }

        // GET: Questions
        [Authorize(Roles = "Студент")]
        public ActionResult Test(int? TopicID)
        {
            var UserID = User.Identity.GetUserId();
            var q_allAnswers = db.StudentAnswers.Where(a => a.Answer.Question.TopicID == TopicID);
            var allAnswers = q_allAnswers.ToList();
            var q_Answers = allAnswers.Where(a => a.StudentID == UserID);
            var Answers = q_Answers.ToList();
            foreach (var item in Answers)
            {
                db.StudentAnswers.Remove(item);
            }
            db.SaveChanges();


            ViewBag.i = 1;
            var questions = db.Questions.Include(q => q.Topic)
                .Where(q => q.TopicID == TopicID);
            ViewBag.answers = new int[questions.Count()];
            return View(questions.ToList());

            //var viewModel = new Test();

            //viewModel.Questions = db.Questions
            //    .Where(q => q.TopicID == TopicID);

            //viewModel.Answers = db.Answers;
            //return View(viewModel);
        }

        [Authorize(Roles = "Студент")]
        public string SaveRadioAnswer(int answerID, int questionID)
        {
            StudentAnswer answer = new StudentAnswer();
            var UserID = User.Identity.GetUserId();
            var q_answers = db.StudentAnswers.Where(a => a.Answer.QuestionID == questionID);
            var answers = q_answers.ToList();
            foreach (var item in answers)
            {
                if (item.StudentID == UserID)
                    item.Change = false;
            }
            var q_answer = answers.Where(a => a.AnswerID == answerID);
            var r_answer = q_answer.ToList();
            var q2_answer = r_answer.Where(a => a.StudentID == UserID);
            var r2_answer = q2_answer.ToList();
            if (r2_answer.Count == 0)
            {
                answer.StudentID = UserID;
                answer.AnswerID = answerID;
                answer.Change = true;

                db.StudentAnswers.Add(answer);
                db.SaveChanges();
            }
            else
            {
                answer = q_answer.First();
                answer.Change = true;
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
            }

            return "Відповідь збережено!";
        }

        [Authorize(Roles = "Студент")]
        public string SaveAnswer(int answerID)
        {
            StudentAnswer answer = new StudentAnswer();
            var UserID = User.Identity.GetUserId();
            var q_answer = db.StudentAnswers.Where(a => a.AnswerID == answerID);
            var r_answer = q_answer.ToList();
            var q2_answer = r_answer.Where(a => a.StudentID == UserID);
            var r2_answer = q2_answer.ToList();
            if (r2_answer.Count == 0)
            {
                answer.StudentID = UserID;
                answer.AnswerID = answerID;
                answer.Change = true;

                db.StudentAnswers.Add(answer);
                db.SaveChanges();
                return "Відповідь збережено";
            }
            else
            {
                answer = q_answer.First();
                if (answer.Change == true)
                    answer.Change = false;
                else
                    answer.Change = true;
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                return "Відповідь скасовано";
            }
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
