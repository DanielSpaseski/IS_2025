using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Repository.Interface;
using AthletesApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthletesApplication.Service.Implementation
{
    public class AthleteService : IAthleteService
    {
        private readonly IRepository<Athlete> _repository;

        public AthleteService(IRepository<Athlete> repository)
        {
            _repository = repository;
        }

        public Athlete DeleteById(Guid id)
        {
            var athlete = this.GetById(id);
            return _repository.Delete(athlete);
        }

        public List<Athlete> GetAll()
        {
            return _repository.GetAll(selector: x => x,
                include: x => x.Include(z => z.Team)).ToList();
        }

        public Athlete? GetById(Guid id)
        {
            return _repository.Get(selector: x => x,
                predicate: x => x.Id == id,
                include: x => x.Include(y => y.Team));
        }

        public Athlete Insert(Athlete athlete)
        {
            return _repository.Insert(athlete);
        }

        public Athlete Update(Athlete athlete)
        {
            return _repository.Update(athlete);
        }
    }
}
