using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Platform
    {
        public Platform()
        {
            Proyects = new HashSet<Proyect>();
        }

        public int PlatformId { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual ICollection<Proyect> Proyects { get; set; }
    }
}
