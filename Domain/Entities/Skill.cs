using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Skill
    {
        public Skill()
        {
            ProyectSkills = new HashSet<ProyectSkill>();
        }

        public int SkillId { get; set; }
        public int? LanguageId { get; set; }
        public int? LevelId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Language Language { get; set; }
        public virtual Level Level { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ProyectSkill> ProyectSkills { get; set; }
    }
}
