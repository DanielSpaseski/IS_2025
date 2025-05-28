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
    public class TeamService : ITeamService
    {
        private readonly IRepository<Team> _repository;

        public TeamService(IRepository<Team> repository)
        {
            _repository = repository;
        }

        public Team DeleteById(Guid id)
        {
            var team = this.GetById(id);
            return _repository.Delete(team);
        }

        public List<Team> GetAll()
        {
            return _repository.GetAll(selector: x => x).ToList();
        }

        public Team? GetById(Guid id)
        {
            return _repository.Get(selector: x => x,
                predicate: x => x.Id == id);
        }

        public Team Insert(Team team)
        {
            
            return _repository.Insert(team);
        }

        public Team Update(Team team)
        {
            return _repository.Update(team);
        }
    }
}
