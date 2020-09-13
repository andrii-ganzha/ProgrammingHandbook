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
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

namespace Diplom_White.Controllers
{
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documents
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string filter)
        {
            ViewBag.filter = filter;
            return View();
        }

        [HttpGet]
        public ActionResult Save()
        {
            var filePath = Server.MapPath("~/Content/Doc.docx");
            string contentType = "application/docx";
            WordprocessingDocument word = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document);
            //WordprocessingDocument word = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document);
            MainDocumentPart mainPart = word.AddMainDocumentPart();
            mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            Body body = mainPart.Document.AppendChild(new Body());

            Paragraph para = body.AppendChild(new Paragraph());
            Run head = para.AppendChild(new Run());
            head.AppendChild(new Text("Збережені дані з таблиці матеріали"));
            foreach (NOC_DKRKM.Models.Document b in db.Documents.Include(d => d.Topic))
            {
                Paragraph par = body.AppendChild(new Paragraph());
                Run text = par.AppendChild(new Run());
                text.AppendChild(new Text("Назва матеріалу: " + b.Name.ToString() + " | "));
                text.AppendChild(new Text("Назва теми: " + b.Topic.Name.ToString() + " | "));
                text.AppendChild(new Text("Посилання на матеріал: " + b.Fail.ToString()));
            }
            word.MainDocumentPart.Document.Save();
            word.Close();

            return File(filePath, contentType, "Doc.docx");
        }

        public ActionResult GetDocuments(string filter = null)
        {
            var documents = db.Documents.Include(d => d.Topic);
            return View("_TableDocuments", filter == null ? documents.ToList() : documents.ToList().Where(x => (x.Name.Contains(filter)) || (x.Kind.KindName.Contains(filter)) || (x.Topic.Name.Contains(filter))));
        }
        //public ActionResult Index()
        //{
        //    var documents = db.Documents.Include(d => d.Kind).Include(d => d.Topic);
        //    return View(documents.ToList());
        //}

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NOC_DKRKM.Models.Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        public ActionResult Create()
        {
            ViewBag.KindID = new SelectList(db.Kinds, "KindId", "KindName");
            ViewBag.TopicID = new SelectList(db.Topics, "TopicId", "Name");
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocumentID,Name,TopicID,Fail,Describe,KindID")] NOC_DKRKM.Models.Document document)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KindID = new SelectList(db.Kinds, "KindId", "KindName", document.KindID);
            ViewBag.TopicID = new SelectList(db.Topics, "TopicId", "Name", document.TopicID);
            return View(document);
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NOC_DKRKM.Models.Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            ViewBag.KindID = new SelectList(db.Kinds, "KindId", "KindName", document.KindID);
            ViewBag.TopicID = new SelectList(db.Topics, "TopicId", "Name", document.TopicID);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentID,Name,TopicID,Fail,Describe,KindID")] NOC_DKRKM.Models.Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KindID = new SelectList(db.Kinds, "KindId", "KindName", document.KindID);
            ViewBag.TopicID = new SelectList(db.Topics, "TopicId", "Name", document.TopicID);
            return View(document);
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NOC_DKRKM.Models.Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NOC_DKRKM.Models.Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
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
