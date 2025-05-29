using JobsApplication.Domain.IdentityModels;

namespace JobsApplication.Domain.DomainModels
{
    public class Application : BaseEntity
    {
        public Candidate Candidate { get; set; }
        public Guid CandidateId { get; set; }
        public JobPosition JobPosition { get; set; }
        public Guid JobPositionId { get; set; }
        public string Status { get; set; }
        public DateTime AppliedDate { get; set; }
        public string OwnerId { get; set; }
        public JobsApplicationUser? Owner { get; set; }
    }
}
