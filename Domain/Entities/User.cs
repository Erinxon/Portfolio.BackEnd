using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class User
    {
        public User()
        {
            Proyects = new HashSet<Proyect>();
            Skills = new HashSet<Skill>();
            WorkExperiences = new HashSet<WorkExperience>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? CreateDate { get; set; }

        public virtual ICollection<Proyect> Proyects { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<WorkExperience> WorkExperiences { get; set; }
    }
}
