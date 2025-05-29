namespace JobsApplication.Domain.DomainModels
{
    public class CandidateInProfession : BaseEntity
    {
        public Candidate? Candidate { get; set; }
        public Guid CandidateId { get; set; }
        public Profession? Profession { get; set; }
        public Guid ProfessionId { get; set; }
    }
}
