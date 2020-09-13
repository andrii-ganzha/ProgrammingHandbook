using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.IO;
using NOC_DKRKM.Models;

namespace NOC_DKRKM.Models
{
    //Дисциплина
    public class Cource
    {
        public int CourceID { get; set; }

        [Required(ErrorMessage = "Це поле обов'язкове!")]
        [Display(Name = "Назва дисципліни")]
        public string Name { get; set; }

        [Display(Name = "Опис дисципліни")]
        public string Describe { get; set; }

        public virtual ICollection<GroupInCource> Groups { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
    }

    //Тема

    public class Topic
    {
        public int TopicID { get; set; }

        [Required(ErrorMessage = "Це поле обов'язкове!")]
        [Display(Name = "Назва теми")]
        public string Name { get; set; }

        [Display(Name = "Опис теми")]
        public string Describe { get; set; }

        [Display(Name = "Назва дисципліни")]
        public int CourceID { get; set; }
        public virtual Cource Cource { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }

    //Вопрос

    public class Question
    {
        public int QuestionID { get; set; }

        [Required(ErrorMessage = "Це поле обов'язкове!")]
        [Display(Name = "Запитання")]
        public string Text { get; set; }

        [Display(Name = "Лістинг")]
        public string Code { get; set; }

        [Display(Name = "Зображення")]
        public byte[] Image { get; set; }

        [Display(Name = "Назва теми")]
        public int TopicID { get; set; }


        [Display(Name ="Кілька відповідей")]
        public bool Check { get; set; }

        public virtual ICollection<Answer> Answers { get; set;}
        public virtual Topic Topic { get; set; }
    }

    //Ответ

    public class Answer
    {
        public int AnswerID { get; set; }

        [Display(Name = "Відповідь")]
        public string Text { get; set; }

        [Display(Name = "Кількість балів за відповідь")]
        public int value { get; set; }

        [Display(Name = "Запитання")]
        public int QuestionID { get; set; }
        public virtual Question Question { get; set; }
    }

    public class Group
    {
        public int GroupID { get; set; }
        [Display(Name = "Шифр групи")]
        [Required(ErrorMessage = "Це поле обов'язкове!")]
        public string Name { get; set; }

        public ICollection<ApplicationUser> Students { get; set; }

        public virtual ICollection<GroupInCource> Cources { get; set; }
    }

    public class GroupInCource
    {
        [Key]
        [Column(Order = 1)]
        public int CourceID { get; set; }

        [Key]
        [Column(Order =2)]
        public int GroupID { get; set; }

        public virtual Cource Cource { get; set; }
        public virtual Group Group { get; set; }

    }

    public class Information
    {
        public int InformationID { get; set; }

        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Це поле обов'язкове")]
        public string Head { get; set; }

        [Display(Name = "Текст")]
        [Required(ErrorMessage = "Це поле обов'язкове")]
        public string Text { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        //{
        //    get
        //    {
        //        return DateTime.Now;
        //    }
        //}

        public string AuthorID { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }

    public class StudentAnswer
    {
        [Key]
        [Column(Order = 1)]
        public string StudentID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int AnswerID { get; set; }

        public bool Change { get; set; }

        public int k { get; set; }

        public virtual ApplicationUser Student { get; set; }
        public virtual Answer Answer { get; set; }
    }


    public class TestResult
    {
        public int ID { get; set; }

        public int TopicID { get; set; }

        public string StudentID { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public int Mark { get; set; }

        public virtual ApplicationUser Student { get; set; }

        public virtual Topic Topic { get; set; }

        public TestResult()
        {
            Questions = new List<Question>();
            Answers = new List<Answer>();
        }

    }

}