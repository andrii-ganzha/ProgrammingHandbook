using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NOC_DKRKM.Models;

namespace NOC_DKRKM.ViewModels
{
    public class Test
    {
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
    }
}