using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ViewProyectSkill
    {
        public int ProyectSkillId { get; set; }
        public int ProyectId { get; set; }
        public int SkillId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? LanguageId { get; set; }
        public int? LevelId { get; set; }
        public string LanguageName { get; set; }
        public string LevelName { get; set; }
    }
}
