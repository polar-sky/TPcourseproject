using System;
using System.Collections.Generic;

namespace University.Models
{
    public partial class AcademicGroup
    {
        public AcademicGroup()
        {
            Graduate = new HashSet<Graduate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? DirectionId { get; set; }

        public virtual Direction Direction { get; set; }
        public virtual ICollection<Graduate> Graduate { get; set; }
    }
}
