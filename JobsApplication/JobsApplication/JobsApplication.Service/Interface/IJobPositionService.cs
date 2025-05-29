using JobsApplication.Domain.DomainModels;

namespace JobsApplication.Service.Interface
{
    public interface IJobPositionService
    {
        List<JobPosition> GetAll();
        JobPosition? GetById(Guid id);
        JobPosition Insert(JobPosition jobPosition);
        JobPosition Update(JobPosition jobPosition);
        JobPosition DeleteById(Guid id);
    }
}
