using NOC_DKRKM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NOC_DKRKM.ViewModels
{
    public class CourseIndexData
    {
        public IEnumerable<Cource> Courses { get; set; }
        public IEnumerable<Topic> Topics { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public IEnumerable<Document> Documents { get; set; }
    }
}