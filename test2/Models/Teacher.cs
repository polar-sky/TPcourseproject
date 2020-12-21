using System;
using System.Collections.Generic;

namespace University.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Gqw = new HashSet<Gqw>();
            Sec = new HashSet<Sec>();
        }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? AcademicDegreeId { get; set; }
        public int? AcademicTitleId { get; set; }
        public int? DepartmentId { get; set; }

        public virtual AcademicDegree AcademicDegree { get; set; }
        public virtual AcademicTitle AcademicTitle { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Gqw> Gqw { get; set; }
        public virtual ICollection<Sec> Sec { get; set; }
    }
}
