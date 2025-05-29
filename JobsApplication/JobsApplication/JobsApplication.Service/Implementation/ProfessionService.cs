using JobsApplication.Domain.DomainModels;
using JobsApplication.Repository.Interface;
using JobsApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace JobsApplication.Service.Implementation 
{
    public class ProfessionService : IProfessionService
    {
        private readonly IRepository<Profession> _professionRepository;
        private readonly IRepository<CandidateInProfession> _candidateInProfessionRepository;
        private readonly IApplicationService _applicationService;

        public ProfessionService(IRepository<Profession> professionRepository, IRepository<CandidateInProfession> candidateInProfessionRepository, IApplicationService applicationService)
        {
            _professionRepository = professionRepository;
            _candidateInProfessionRepository = candidateInProfessionRepository;
            _applicationService = applicationService;
        }

        public Profession Create(string userId)
        {
            // TODO: Implement method
            // Hint: Look at auditory exercises: OrderProducts method in ShoppingCartService

            // Get all Applications by current user
            // Create new Profession and insert in database
            // Create new CandidatesInProfession using Candidates from the Applications and insert in database
            // Delete all Applications
            // Return Profession

            var applications = _applicationService.GetAllByCurrentUser(userId);
            var newProfession = new Profession
            {
                Id = Guid.NewGuid(),
                OwnerId = userId,
                DateCreated = DateTime.Now
            };
            _professionRepository.Insert(newProfession);

            var candidatesInProfession = applications.Select(x => new CandidateInProfession
            {
                CandidateId = x.CandidateId,
                ProfessionId = newProfession.Id
            }).ToList();

            foreach(var item in candidatesInProfession)
            {
                _candidateInProfessionRepository.Insert(item);
            }
            foreach(var item in applications)
            {
                _applicationService.DeleteById(item.Id);
            }
            return newProfession;
        }

        // Bonus task
        public Profession GetDetailsById(Guid id)
        {
            // TODO: Implement method
            return _professionRepository.Get(selector: x => x,
                predicate: x => x.Id == id,
                include: x => x.Include(y => y.CandidatesInProfession)
                .ThenInclude(z => z.Candidate)
                .Include(y => y.Owner));
        }
    }
}
