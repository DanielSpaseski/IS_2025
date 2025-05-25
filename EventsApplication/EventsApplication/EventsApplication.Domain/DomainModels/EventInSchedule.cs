namespace EventsApplication.Domain.DomainModels
{
    public class EventInSchedule : BaseEntity
    {
        public Guid ScheduleId { get; set; }
        public Schedule? Schedule { get; set; }
        public Guid EventId { get; set; }
        public Event? Event { get; set; }

    }
}
