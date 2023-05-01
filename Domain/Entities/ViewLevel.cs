using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ViewLevel
    {
        public int LevelId { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
