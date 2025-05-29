using JobsApplication.Domain.DomainModels;
using JobsApplication.Repository.Interface;
using JobsApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace JobsApplication.Service.Implementation
{
    public class InterviewService : IInterviewService
    {
        private readonly IRepository<Interview> _interviewRepository;

        public InterviewService(IRepository<Interview> interviewRepository)
        {
           _interviewRepository = interviewRepository;
        }

        public List<Interview> GetAll()
        {
            return _interviewRepository
                .GetAll(
                    selector: x => x,
                    include: x => x.Include(z => z.JobPosition)
                ).ToList();
        }

        public Interview? GetById(Guid id)
        {
            return _interviewRepository
                .Get(
                    selector: x => x,
                    predicate: x => x.Id == id,
                    include: x => x.Include(z => z.JobPosition)
                );
        }

        public Interview Insert(Interview interview)
        {
            interview.Id = Guid.NewGuid();
            return _interviewRepository.Insert(interview);
        }

        public Interview Update(Interview interview)
        {
            return _interviewRepository.Update(interview);
        }
        public Interview DeleteById(Guid id)
        {
            var interview = GetById(id);
            if (interview == null)
            {
                throw new Exception("Interview not found");
            }
            return _interviewRepository.Delete(interview);
        }

    }
}
