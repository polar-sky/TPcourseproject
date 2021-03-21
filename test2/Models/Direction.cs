using System;
using System.Collections.Generic;

namespace University.Models
{
    public partial class Direction
    {
        public Direction()
        {
            AcademicGroup = new HashSet<AcademicGroup>();
        }

        public int Id { get; set; }
        public string NumberOfDirection { get; set; }
        public string Name { get; set; }
        public string FormOfEducation { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<AcademicGroup> AcademicGroup { get; set; }
    }
}
