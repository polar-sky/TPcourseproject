using System;
using System.Collections.Generic;

namespace University.Models
{
    public partial class Sec
    {
        public Sec()
        {
            Gqw = new HashSet<Gqw>();
        }

        public int Id { get; set; }
        public int? SecretaryId { get; set; }
        public int? ChairmanId { get; set; }
        public string Year { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Partner Chairman { get; set; }
        public virtual Department Department { get; set; }
        public virtual Teacher Secretary { get; set; }
        public virtual ICollection<Gqw> Gqw { get; set; }
    }
}
