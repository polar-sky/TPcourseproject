using System;
using System.Collections.Generic;

namespace University.Models
{
    public partial class Graduate
    {
        public Graduate()
        {
            Gqw = new HashSet<Gqw>();
        }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public int? GroupId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CurrentCity { get; set; }
        public int? CompanyId { get; set; }
        public bool? DisciplineLecture { get; set; }
        public bool? DisciplineLaboratoryWorks { get; set; }
        public int? AcademicDegreeId { get; set; }

        public virtual AcademicDegree AcademicDegree { get; set; }
        public virtual Company Company { get; set; }
        public virtual AcademicGroup Group { get; set; }
        public virtual ICollection<Gqw> Gqw { get; set; }
    }
}
