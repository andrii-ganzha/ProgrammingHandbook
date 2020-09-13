using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNet.Identity;
using NOC_DKRKM.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Word = Microsoft.Office.Interop.Word;

namespace NOC_DKRKM.Controllers
{
    public class TestResultsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index(string userID)
        {
            if(userID == "")
                userID = User.Identity.GetUserId();
            var tests = db.TestResults.Where(t => t.StudentID == userID);
            return View(tests.ToList());
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult test = db.TestResults.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }
        [Authorize(Roles = "Студент")]
        public ActionResult GetResult(int topicID)
        {
            var UserID = User.Identity.GetUserId();
            var q_allAnswers = db.StudentAnswers.Where(a => a.Answer.Question.TopicID == topicID);
            var allAnswers = q_allAnswers.ToList();
            var q_Answers = allAnswers.Where(a => a.StudentID == UserID);
            var Answers = q_Answers.ToList();
            var test = new TestResult();
            test.TopicID = topicID;
            test.StudentID = UserID;
            test.Date = DateTime.UtcNow.AddHours(3);
            foreach (var item in Answers)
            {
                if (item.Change == true)
                {
                    test.Questions.Add(item.Answer.Question);
                    test.Answers.Add(item.Answer);
                    test.Mark += item.Answer.value;
                }
                db.StudentAnswers.Remove(item);
            }
            db.TestResults.Add(test);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = test.ID});
        }
        [HttpGet]
        public ActionResult Save(int TestID)
        {
            // var q_test = db.TestResults.Where(t => t.ID == TestID);
            // var test = q_test.First();
            // Word.Application word = new Word.Application();
            // word.Visible = true;
            // Word.Document doc = word.Documents.Add();
            // doc.Select();
            // word.Selection.TypeText("ТЕСТ\n");
            // word.Selection.TypeText("Студент\t" + test.Student.lname + " " + test.Student.name + " " + test.Student.sname +"\n");
            // word.Selection.TypeText("Тема\t" + test.Topic.Name + "\n");
            // word.Selection.TypeText("Дата, час\t" + test.Date+"\n");
            // word.Selection.TypeText("Оцінка\t" + test.Mark + "\n");
            // foreach(var item in test.Answers)
            //{
            //     word.Selection.TypeText(item.Question.Text + "\n");
            //     word.Selection.TypeText(item.Text+"\n");
            //     word.Selection.TypeText(item.value + "\n\n");
            // //   < h4 > @item.Question.Text </ h4 >
            // //   @item.Text
            // //< h5 > @item.value бали </ h5 >
            //}

            // return RedirectToAction("Index");

            var q_test = db.TestResults.Where(t => t.ID == TestID);
            var test = q_test.First();

            var filePath = Server.MapPath("~/Content/Doc.docx");
            string contentType = "application/docx";
            WordprocessingDocument word = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document);
            //WordprocessingDocument word = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document);
            MainDocumentPart mainPart = word.AddMainDocumentPart();
            mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            Body body = mainPart.Document.AppendChild(new Body());

            Paragraph para = body.AppendChild(new Paragraph());
            Run head = para.AppendChild(new Run());
            head.AppendChild(new Text("ТЕСТ"));
            para = body.AppendChild(new Paragraph());
            head = para.AppendChild(new Run());
            head.AppendChild(new Text("Студент\t" + test.Student.sname + " " + test.Student.name + " " + test.Student.lname + "\n"));
            para = body.AppendChild(new Paragraph());
            head = para.AppendChild(new Run());
            head.AppendChild(new Text("Тема\t" + test.Topic.Name));
            para = body.AppendChild(new Paragraph());
            head = para.AppendChild(new Run());
            head.AppendChild(new Text("Дата, час\t" + test.Date));
            para = body.AppendChild(new Paragraph());
            head = para.AppendChild(new Run());
            head.AppendChild(new Text("Бали\t" + test.Mark));
            foreach (var item in test.Answers)
            {
                Paragraph par = body.AppendChild(new Paragraph());
                Run text = par.AppendChild(new Run());
                text.AppendChild(new Text(item.Question.Text));
                par = body.AppendChild(new Paragraph());
                text = par.AppendChild(new Run());
                text.AppendChild(new Text(item.Text));
                par = body.AppendChild(new Paragraph());
                text = par.AppendChild(new Run());
                text.AppendChild(new Text(item.value + " балів"));
                par = body.AppendChild(new Paragraph());
                text = par.AppendChild(new Run());
                text.AppendChild(new Text(""));
            }
            word.MainDocumentPart.Document.Save();
            word.Close();

            return File(filePath, contentType, "Тестування "+test.Student.sname+" "+test.Date.ToString()+".docx");

        }
    }
}
        //[HttpPost]
        //public ActionResult GetResult(TestResult result, int[] answers)
        //{
        //    result.StudentID = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //    Answer item;
        //    for (int i=0; i<answers.Count(); i++)
        //    {
        //        item = (Answer)db.Answers.Where(m => m.AnswerID == answers[i]);
        //        result.Answers.Add(item);
        //        result.Mark += item.value;
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        db.TestResults.Add(result);
        //        db.SaveChanges();
        //    }

//    return View();
//}
//    }
//}