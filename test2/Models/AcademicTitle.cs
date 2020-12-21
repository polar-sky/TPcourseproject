using System;
using System.Collections.Generic;

namespace University.Models
{
    public partial class AcademicTitle
    {
        public AcademicTitle()
        {
            Partner = new HashSet<Partner>();
            Teacher = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Partner> Partner { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
    }
}
