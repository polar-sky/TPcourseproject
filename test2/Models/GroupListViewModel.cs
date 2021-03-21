using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace University.Models
{
    public class GroupListViewModel
    {
        public List<Graduate> Graduates { get; set; }
        public SelectList Groups { get; set; }
        public string GraduateGroup { get; set; }
        public string SearchString { get; set; }
    }
}