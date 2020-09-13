using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diplom_White.Models;

namespace Diplom_White.ViewModels
{
    public class TopicIndexData
    {
        public IEnumerable<Topic> Topics { get; set; }
        public IEnumerable<Document> Documents { get; set; }
    }
}