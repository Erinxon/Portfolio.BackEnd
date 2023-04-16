using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Language
    {
        public Language()
        {
            Skills = new HashSet<Skill>();
        }

        public int LanguageId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Skill> Skills { get; set; }
    }
}
