namespace JobsApplication.Domain.DomainModels
{
    public class JobPosition : BaseEntity
    {
        public string Title { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public ICollection<JobPosition>? JobPositions { get; set; }
        public ICollection<Application>? Applications { get; set; }
    }
}
