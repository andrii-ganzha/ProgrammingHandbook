//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using System.Data.Entity;
//using System.Data.Entity.Migrations;

//namespace NOC_DKRKM.Models
//{
//    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
//    {
//        protected override void Seed(ApplicationDbContext context)
//        {
//            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

//            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

//            //добавляем группу
//            context.Groups.AddOrUpdate(p => p.Name,
//                new Group
//                {
//                    Name = "Викладачі"
//                }
//                );


//            //создаём три роли:
//            var role1 = new IdentityRole { Name = "Адміністратор" };
//            var role2 = new IdentityRole { Name = "Викладач" };
//            var role3 = new IdentityRole { Name = "Студент" };

//            //добавляем роли в бд
//            roleManager.Create(role1);
//            roleManager.Create(role2);
//            roleManager.Create(role3);

//            var admin = new ApplicationUser { Email = "admin@dkrkm.org.ua", UserName = "admin@dkrkm.org.ua", GroupID = 1 };
//            string password = "123456";
//            var result = userManager.Create(admin, password);

//            //если создание пользователя прошло успешно
//            if (result.Succeeded)
//            {
//                //добавляем для пользователя роль
//                userManager.AddToRole(admin.Id, role1.Name);
//            }
//            base.Seed(context);
//        }
//    }
//}