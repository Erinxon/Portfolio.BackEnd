using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class WorkExperience
    {
        public int WorkExperienceId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string PositionName { get; set; } = null!;
        public int? UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual User? User { get; set; }
    }
}
