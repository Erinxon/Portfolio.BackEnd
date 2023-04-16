using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Proyect
    {
        public Proyect()
        {
            ProyectSkills = new HashSet<ProyectSkill>();
        }

        public int ProyectId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public Guid? ImageGuidId { get; set; }
        public string? GithubUrl { get; set; }
        public string? DomainUrl { get; set; }
        public int PlatformId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Platform Platform { get; set; } = null!;
        public virtual User? User { get; set; }
        public virtual ICollection<ProyectSkill> ProyectSkills { get; set; }
    }
}
