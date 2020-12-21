using System;
using System.Collections.Generic;

namespace University.Models
{
    public partial class Company
    {
        public Company()
        {
            Graduate = new HashSet<Graduate>();
            Partner = new HashSet<Partner>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Site { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Graduate> Graduate { get; set; }
        public virtual ICollection<Partner> Partner { get; set; }
    }
}
