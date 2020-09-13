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
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NOC_DKRKM;
using System.Net.Mail;

namespace NOC_DKRKM.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: News
        //public ActionResult Index()
        //{
        //    return View(db.Information.ToList());
        //}

        // GET: News/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Information information = db.Information.Find(id);
        //    if (information == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(information);
        //}

        public Task SendAsync(IdentityMessage message)
        {
            //настройка логина, пароля отправителя
            var from = "somemail@mail.com";
            var pass = "******";

            //адрес и порт smtp-сервера, с которого мы и будем отправлять письмо
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, pass);
            client.EnableSsl = true;

            // создаем письмо: message.Destination - адрес получателя
            var mail = new MailMessage(from, message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            return client.SendMailAsync(mail);
            //// Подключите здесь службу электронной почты для отправки сообщения электронной почты.
            //return Task.FromResult(0);
        }

        // GET: News/Create
        [Authorize(Roles ="Адміністратор,Викладач")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Адміністратор,Викладач")]
        public async System.Threading.Tasks.Task<ActionResult> Create([Bind(Include = "InformationID,Head,Text")] Information information)
        {
            information.AuthorID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            information.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Information.Add(information);
                db.SaveChanges();
                IdentityMessage message = new IdentityMessage();
                foreach (var item in db.Users)
                {
                    message.Destination = item.Email;
                    message.Subject = information.Head;
                    message.Body = information.Text;
                    await SendAsync(message);
                    //             await UserManager.SendEmailAsync(user.Id, "Вас зараєстровано в системі \"Посібник з програмування\"",
                    //"Вітаємо, " + user.name + "! Вас як студента групи " + group.Name + " зареєстровано в системі \"Посібник з програмування\" коледжу ракетно-космічного машинобудування.\n Для входу на сайт скористайтеся <a href=\"http:\\\\dkrkm-noc.azurewebsites.net\\Login\">формою входу</a>");
                }
                return RedirectToAction("..");
            }

            return View(information);
        }

        // GET: News/Edit/5
        [Authorize(Roles = "Адміністратор,Викладач")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Information information = db.Information.Find(id);
            if (information == null)
            {
                return HttpNotFound();
            }
            return View(information);
        }

        // POST: News/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Адміністратор,Викладач")]
        public ActionResult Edit([Bind(Include = "InformationID,Head,Text")] Information information)
        {
            if (ModelState.IsValid)
            {
                information.Date = DateTime.Now;
                db.Entry(information).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("..");
            }
            return View(information);
        }

        // GET: News/Delete/5
        [Authorize(Roles = "Адміністратор,Викладач")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Information information = db.Information.Find(id);
            if (information == null)
            {
                return HttpNotFound();
            }
            return View(information);
        }

        // POST: News/Delete/5
        [Authorize(Roles = "Адміністратор,Викладач")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Information information = db.Information.Find(id);
            db.Information.Remove(information);
            db.SaveChanges();
            return RedirectToAction("..");
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
