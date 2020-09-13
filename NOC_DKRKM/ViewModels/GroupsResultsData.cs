using NOC_DKRKM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NOC_DKRKM.ViewModels
{
    public class GroupsResultsData
    {
        public IEnumerable<ApplicationUser> Students { get; set; }
        public IEnumerable<Topic> Topics { get; set; }
        public IEnumerable<TestResult> TestResults { get; set; }
    }
}