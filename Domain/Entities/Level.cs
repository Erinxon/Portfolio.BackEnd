using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Level
    {
        public Level()
        {
            Skills = new HashSet<Skill>();
        }

        public int LevelId { get; set; }
        public string? Name { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}
