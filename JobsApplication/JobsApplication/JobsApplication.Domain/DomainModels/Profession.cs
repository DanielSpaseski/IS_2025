using JobsApplication.Domain.IdentityModels;

namespace JobsApplication.Domain.DomainModels
{
    public class Profession : BaseEntity
    {
        public string? OwnerId { get; set; }
        public JobsApplicationUser Owner { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<CandidateInProfession>? CandidatesInProfession {  get; set; }
    }
}
