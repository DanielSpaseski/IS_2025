using JobsApplication.Domain.DomainModels;
using JobsApplication.Repository.Interface;
using JobsApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace JobsApplication.Service.Implementation
{
    public class ApplicationService : IApplicationService
    {

        private readonly IRepository<Application> _applicationRepository;

        public Application ScheduleApplicationForCandidateAndPosition(string userId, Guid candidateId, Guid positionId)
        {
            var schedule = new Application
            {
                CandidateId = candidateId,
                JobPositionId = positionId,
                OwnerId = userId,
                Status = "Valid",
                AppliedDate = DateTime.Now,
            };

            return _applicationRepository.Insert(schedule);
        }


        public ApplicationService(IRepository<Application> applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public List<Application> GetAllByCurrentUser(string userId)
        {
            return _applicationRepository
                .GetAll(
                    selector: x => x,
                    predicate: x => x.OwnerId == userId,
                    include: x => x.Include(z => z.Candidate).Include(z => z.JobPosition).Include(z => z.Owner)
                ).ToList();
        }

        public Application GetById(Guid id)
        {
            return _applicationRepository
                .Get(
                    selector: x => x,
                    predicate: x => x.Id == id,
                    include: x => x.Include(z => z.Candidate).Include(z => z.JobPosition).Include(z => z.Owner)
                );
        }

        public Application DeleteById(Guid id)
        {
            // TODO: Implement method
            var application = this.GetById(id);
            return _applicationRepository.Delete(application);
        }

    }
}
