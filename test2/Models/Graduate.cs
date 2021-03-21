using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace University.Models
{
    public partial class Graduate
    {
        public Graduate()
        {
            Gqw = new HashSet<Gqw>();
        }

        public int Id { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Группа")]
        public int? GroupId { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "email")]
        public string Email { get; set; }

        [Display(Name = "Город")]
        public string CurrentCity { get; set; }

        [Display(Name = "Компания")]
        public int? CompanyId { get; set; }

        [Display(Name = "Лекции")]
        public bool? DisciplineLecture { get; set; }

        [Display(Name = "Лабораторные")]
        public bool? DisciplineLaboratoryWorks { get; set; }

        [Display(Name = "Академическая степень")]
        public int? AcademicDegreeId { get; set; }

        public virtual AcademicDegree AcademicDegree { get; set; }
        public virtual Company Company { get; set; }
        public virtual AcademicGroup Group { get; set; }
        public virtual ICollection<Gqw> Gqw { get; set; }
    }

}
