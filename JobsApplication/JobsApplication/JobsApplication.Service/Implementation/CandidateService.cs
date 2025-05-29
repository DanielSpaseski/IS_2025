using JobsApplication.Domain.DomainModels;
using JobsApplication.Repository.Interface;
using JobsApplication.Service.Interface;

namespace JobsApplication.Service.Implementation
{
    public class CandidateService : ICandidateService
    {
        private IRepository<Candidate> _candidateRepository;

        public CandidateService(IRepository<Candidate> candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public List<Candidate> GetAll()
        {
            return _candidateRepository
                .GetAll(
                    selector: x => x
                ).ToList();
        }

        public Candidate? GetById(Guid id)
        {
            return _candidateRepository
                .Get(
                    selector: x => x,
                    predicate: x => x.Id == id
                );
        }

        public Candidate Insert(Candidate candidate)
        {
            candidate.Id = Guid.NewGuid();
            return _candidateRepository.Insert(candidate);
        }

        public Candidate Update(Candidate candidate)
        {
            return _candidateRepository.Update(candidate);
        }
        public Candidate DeleteById(Guid id)
        {
            var candidate = GetById(id);
            if (candidate == null)
            {
                throw new Exception("Candidate not found");
            }
            return _candidateRepository.Delete(candidate);
        }
    }
}
