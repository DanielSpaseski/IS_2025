using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Repository.Interface;
using AthletesApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthletesApplication.Service.Implementation
{
    public class CompetitionService : ICompetitionService
    {
        private readonly IRepository<Competition> _competitionRepository;

        public CompetitionService(IRepository<Competition> competitionRepository)
        {
            _competitionRepository = competitionRepository;
        }

        public Competition DeleteById(Guid id)
        {
            var competition = this.GetById(id);
            return _competitionRepository.Delete(competition);
        }

        public List<Competition> GetAll()
        {
            return _competitionRepository.GetAll(selector: x => x).ToList();
        }

        public Competition? GetById(Guid id)
        {
            return _competitionRepository.Get(selector: x => x,
                predicate: x => x.Id == id);
        }

        public Competition Insert(Competition competition)
        {
            return _competitionRepository.Insert(competition);
        }

        public Competition Update(Competition competition)
        {
            return _competitionRepository.Update(competition);
        }
    }
}
