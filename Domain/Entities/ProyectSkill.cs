using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ProyectSkill
    {
        public int ProyectSkillId { get; set; }
        public int ProyectId { get; set; }
        public int SkillId { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Proyect Proyect { get; set; } = null!;
        public virtual Skill Skill { get; set; } = null!;
    }
}
