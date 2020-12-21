using System;
using System.Collections.Generic;

namespace University.Models
{
    public partial class Gqw
    {
        public int Id { get; set; }
        public int? GraduateId { get; set; }
        public int? ReviewerId { get; set; }
        public int? TeacherId { get; set; }
        public int? SecId { get; set; }
        public string Theme { get; set; }
        public sbyte? Grade { get; set; }
        public sbyte? ReviewerGrade { get; set; }
        public DateTime? DateOfDefence { get; set; }
        public sbyte? ProtocolNumber { get; set; }
        public bool? IsArchived { get; set; }

        public virtual Graduate Graduate { get; set; }
        public virtual Partner Reviewer { get; set; }
        public virtual Sec Sec { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
