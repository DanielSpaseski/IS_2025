using JobsApplication.Domain.DomainModels;

namespace JobsApplication.Service.Interface
{
    public interface IProfessionService
    {
        Profession Create(string userId);
        Profession GetDetailsById(Guid id);
    }
}
