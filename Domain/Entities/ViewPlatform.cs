using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ViewPlatform
    {
        public int PlatformId { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
