using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace University.Models
{
    public class GraduateListViewModel
    {
        public IEnumerable<Graduate> Graduates { get; set; }
        public SelectList Groups { get; set; }
        public SelectList Company { get; set; }
        public SelectList AcademicDegree { get; set; }
    }
}