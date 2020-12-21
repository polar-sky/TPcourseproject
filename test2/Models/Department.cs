using System;
using System.Collections.Generic;

namespace University.Models
{
    public partial class Department
    {
        public Department()
        {
            Direction = new HashSet<Direction>();
            Partner = new HashSet<Partner>();
            Sec = new HashSet<Sec>();
            Teacher = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? InstituteId { get; set; }

        public virtual Institute Institute { get; set; }
        public virtual ICollection<Direction> Direction { get; set; }
        public virtual ICollection<Partner> Partner { get; set; }
        public virtual ICollection<Sec> Sec { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
    }
}
