using JobsApplication.Domain.DomainModels;

namespace JobsApplication.Service.Interface
{
    public interface IInterviewService
    {
        List<Interview> GetAll();
        Interview? GetById(Guid id);
        Interview Insert(Interview interview);
        Interview Update(Interview interview);
        Interview DeleteById(Guid id);

    }
}
