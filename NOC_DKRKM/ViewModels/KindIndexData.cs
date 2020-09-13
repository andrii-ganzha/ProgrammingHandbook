using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NOC_DKRKM.Models;

namespace NOC_DKRKM.ViewModels
{
    public class KindIndexData
    {
        public IEnumerable<Kind> Kinds { get; set; }
        public IEnumerable<Document> Documents { get; set; }
    }
}