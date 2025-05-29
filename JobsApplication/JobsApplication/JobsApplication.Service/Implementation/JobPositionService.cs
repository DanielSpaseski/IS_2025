using JobsApplication.Domain.DomainModels;
using JobsApplication.Repository.Interface;
using JobsApplication.Service.Interface;

namespace JobsApplication.Service.Implementation
{
    public class JobPositionService : IJobPositionService
    {
        private readonly IRepository<JobPosition> _jobPositionRepository;

        public JobPositionService(IRepository<JobPosition> jobPositionRepository)
        {
            _jobPositionRepository = jobPositionRepository;
        }

        public List<JobPosition> GetAll()
        {
            return _jobPositionRepository
                .GetAll(
                    selector: x => x
                ).ToList();
        }

        public JobPosition? GetById(Guid id)
        {
            return _jobPositionRepository
                .Get(
                    selector: x => x,
                    predicate: x => x.Id == id
                );
        }

        public JobPosition Insert(JobPosition jobPosition)
        {
            jobPosition.Id = Guid.NewGuid();
            return _jobPositionRepository.Insert(jobPosition);
        }

        public JobPosition Update(JobPosition jobPosition)
        {
            return _jobPositionRepository.Update(jobPosition);
        }

        public JobPosition DeleteById(Guid id)
        {
            var jobPosition = GetById(id);
            if (jobPosition == null)
            {
                throw new Exception("JobPosition not found");
            }
            return _jobPositionRepository.Delete(jobPosition);
        }
    }
}
