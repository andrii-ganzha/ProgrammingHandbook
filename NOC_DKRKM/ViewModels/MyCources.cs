using NOC_DKRKM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NOC_DKRKM.ViewModels
{
    public class MyCources
    {
        public IEnumerable<Cource> Cources { get; set; }

        public IEnumerable<Information> News { get; set; }
    }
}