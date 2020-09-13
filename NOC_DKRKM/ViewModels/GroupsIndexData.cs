using NOC_DKRKM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NOC_DKRKM.ViewModels
{
    public class GroupsIndexData
    {
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<ApplicationUser> Students { get; set; }
        public IEnumerable<GroupInCource> GroupsInCource { get; set; }
    }
}