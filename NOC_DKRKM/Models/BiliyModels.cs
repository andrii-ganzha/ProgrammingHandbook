using NOC_DKRKM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NOC_DKRKM.Models
{
     public class Document
    {
        public int DocumentID { get; set; }

        [Display(Name = "Назва матеріалу")]
        [Required(ErrorMessage = "Обов'язкове поле!")]
        public string Name { get; set; }

        [Display(Name = "Тема")]
        [Required(ErrorMessage = "Обов'язкове поле!")]
        public int TopicID { get; set; }

        [Display(Name = "Посилання на матеріал")]
        [Required(ErrorMessage = "Обов'язкове поле!")]
        [DataType(DataType.Url)]
        public string Fail { get; set; }

        [Display(Name = "Опис матеріалу")]
        public string Describe { get; set; }

        [Display(Name = "Вид матеріалу")]
        public int KindID { get; set; }

        public virtual Kind Kind { get; set; }
        public virtual Topic Topic { get; set; }
    }

    public class Kind
    {
        public int KindId { get; set; }

        [Display(Name = "Вид матеріалу")]
        [Required(ErrorMessage = "Обов'язкове поле!")]
        public string KindName { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }

}