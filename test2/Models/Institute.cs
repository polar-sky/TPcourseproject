using System;
using System.Collections.Generic;

namespace University.Models
{
    public partial class Institute
    {
        public Institute()
        {
            Department = new HashSet<Department>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Department> Department { get; set; }
    }
}
