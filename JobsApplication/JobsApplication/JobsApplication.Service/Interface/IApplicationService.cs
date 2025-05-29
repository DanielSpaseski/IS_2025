using JobsApplication.Domain.DomainModels;

namespace JobsApplication.Service.Interface
{
    public interface IApplicationService
    {
        public Application ScheduleApplicationForCandidateAndPosition(string userId, Guid candidateId, Guid positionId);
        public List<Application> GetAllByCurrentUser(string userId);
        public Application GetById(Guid id);
        public Application DeleteById(Guid id);
    }
}
