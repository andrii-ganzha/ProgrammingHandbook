using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NOC_DKRKM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace NOC_DKRKM.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Topics
        public ActionResult Index(int?page)
        {
            ViewBag.Slash = " | ";
            var news = db.Information.OrderByDescending(i => i.Date);
            if (page == null)
                page = 1;
            int pageSize = 3;
            int pageIndex = (page ?? 1);

            return View(news.ToPagedList(pageIndex, pageSize));
        }
    }
}
//        public ActionResult Index()
//        {
//            var viewModal = new MyCources();
//            //viewModal.Cources = db.Courses.Where(c => c.Groups.Where(g => g.Group.Students.(s => s.Id == System.Web.HttpContext.Current.User.Identity.GetUserId())));
//            string UserID = System.Web.HttpContext.Current.User.Identity.GetUserId();
//            //var User = db.Users.Where(u => u.Id == UserID);
//            //var q_UserRole = db.Roles.Where(r => r.Users == User);
//            //ViewBag.UserRole = User.ToList();
////var User = db.Users.Include(u=>u.Group).Where(u => u.Id == UserID);
////IQueryable<ApplicationUser> varUser = from u in db.Users.Include(s=>s.Group)
////           where u.Id == UserID
////           select u.GroupID;

////var lUser = User.ToList();
////int GroupID = (int)User.Average();

////var Group = from u in db.Groups.Include(g => g.Cources)
////            where  == GroupID
////            select u.Cources;


////var Cource = from c in db.GroupInCources
////             where c.Cource == Group
////             select c;

////viewModal.Cources = Cource;

////IQueryable<ApplicationUser> QUser = db.Users.Where(u => u.Id == UserID);

////List<ApplicationUser> User = QUser.ToList();

////IQueryable<Group> QGroup = db.Groups.Where(g => g.Students == User);

////List<Group> Group = QGroup.ToList();



////var q_Cources = from c in db.Courses
////                          join gc in db.GroupInCources on c.CourceID equals gc.CourceID
////                          join g in db.Groups on gc.GroupID equals g.GroupID
////                          join s in db.Users on g.GroupID equals s.GroupID
////                          where s.Id == UserID
////                          select c;
////            var Cources = q_Cources.ToList();
////            viewModal.Cources = Cources;
////            var q_News = db.Information.Include(i=>i.Author).OrderByDescending(i=>i.Date);
////            var News = q_News.ToList();
////            viewModal.News = News;

            
//            return View(viewModal);








//            /*
//            //ViewBag.Cources = new SelectList(db.Courses.Where(c=>c. , "CourseID", "Name");
//            //var GroupInCource = db.GroupInCources.Where(g => g.Group == Group);
//            //var Cources = db.Courses.Where(c => c.Groups == GroupInCource);
//            //viewModal.Users = db.Users.Where(u => u.Id == System.Web.HttpContext.Current.User.Identity.GetUserId());
//            ////viewModal.Cources = db.Courses.Where(c=>c.Students.Where(s=>s.Id == System.Web.HttpContext.Current.User.Identity.GetUserId()==true)
//            ////viewModal.Cources = db.Courses.Where(c => c.Students.Where(s => s.Id == System.Web.HttpContext.Current.User.Identity.GetUserId())==viewModal.Users);
//            ////viewModal.Cources = db.Courses.Where(c => c.Groups.Where(g=>g.Group.Students.Where(s=>s == viewModal.Users)==viewModal.Users)==viewModal.Users);
//            //viewModal.Cources=viewModal.Users.Select(u=>u.)
//            //viewModal.Informations = db.Information;
//            //viewModal.Cources = Cources;
//            */
//        }

//        public ActionResult About()
//        {
//            ViewBag.Message = "Your application description page.";

//            return View();
//        }

//        public ActionResult Contact()
//        {
//            ViewBag.Message = "Your contact page.";

//            return View();
//        }
//    }
//}