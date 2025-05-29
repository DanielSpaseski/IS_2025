using JobsApplication.Domain.DomainModels;

namespace JobsApplication.Service.Interface
{
    public interface ICandidateService
    {
        List<Candidate> GetAll();
        Candidate? GetById(Guid id);
        Candidate Insert(Candidate candidate);
        Candidate Update(Candidate candidate);
        Candidate DeleteById(Guid id);
    }
}
