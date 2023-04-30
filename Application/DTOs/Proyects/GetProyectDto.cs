using Domain.Entities;

namespace Application.DTOs.Proyects
{
    public class GetProyectDto
    {
        public int ProyectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ImageGuidId { get; set; }
        public string GithubUrl { get; set; }
        public string DomainUrl { get; set; }
        public int PlatformId { get; set; }
        public string PlatformName { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public virtual IEnumerable<ViewProyectSkill> ProyectSkills { get; set; }
    }
}
