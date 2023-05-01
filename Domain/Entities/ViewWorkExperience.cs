using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ViewWorkExperience
    {
        public int WorkExperienceId { get; set; }
        public string CompanyName { get; set; }
        public string PositionName { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
