using EventsApplication.Domain.IdentityModels;

namespace EventsApplication.Domain.DomainModels
{
    public class Schedule : BaseEntity
    {
        public string? OwnerId { get; set; }
        public EventsApplicationUser Owner { get; set; }
        public virtual ICollection<EventInSchedule>? EventInSchedules { get; set; }
    }
}
