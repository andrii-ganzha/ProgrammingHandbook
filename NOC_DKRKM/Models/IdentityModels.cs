using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using NOC_DKRKM.Models;
using System.Collections.Generic;

namespace NOC_DKRKM.Models
{
    // Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        //МОИ ПОЛЯ
        [Display(Name = "Ім'я")]
        public string name { get; set; }

        [Display(Name ="Прізвище")]
        public string sname { get; set; }

        [Display(Name ="По батькові")]
        public string lname { get; set; }

        public int GroupID { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }

        //КОНЕЦ МОИХ ПОЛЕЙ

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //МОИ ТАБЛИЦЫ

        public DbSet<Cource> Courses { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupInCource> GroupInCources { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<TestResult> TestResults { get; set; }

        //КОНЕЦ МОИХ ТАБЛИЦ

        //ТАБЛИЦЫ ВОВЫ
        public DbSet<Document> Documents { get; set; }
        public DbSet<Kind> Kinds { get; set; }
        //КОнец

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}