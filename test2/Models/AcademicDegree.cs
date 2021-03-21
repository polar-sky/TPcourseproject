using System;
using System.Collections.Generic;

namespace University.Models
{
    public partial class AcademicDegree
    {
        public AcademicDegree()
        {
            Graduate = new HashSet<Graduate>();
            Partner = new HashSet<Partner>();
            Teacher = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public string Degree { get; set; }

        public virtual ICollection<Graduate> Graduate { get; set; }
        public virtual ICollection<Partner> Partner { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
    }
}
