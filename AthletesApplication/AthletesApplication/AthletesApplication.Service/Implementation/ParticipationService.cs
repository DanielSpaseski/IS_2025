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
    public class ParticipationService : IParticipationService
    {
        private readonly IRepository<Participation> _participationRepository;

        public ParticipationService(IRepository<Participation> participationRepository)
        {
            _participationRepository = participationRepository;
        }

        public Participation AddParticipationForAthleteAndCompetition(string userId, Guid athleteId, Guid competitionId)
        {
            Participation participation = new Participation
            {
                Id = Guid.NewGuid(),
                CompetitionId = competitionId,
                AthleteId = athleteId,
                OwnerId = userId
            };
            return _participationRepository.Insert(participation);
        }

        public Participation DeleteById(Guid id)
        {
            var participation = this.GetById(id);
            return _participationRepository.Delete(participation);
        }

        public List<Participation> GetAllByCurrentUser(string userId)
        {
            return _participationRepository.GetAll(selector: x => x,
                predicate: x => x.OwnerId == userId,
                include: x => x.Include(z => z.Athlete)
                .Include(z => z.Competition)
                .Include(z => z.Owner))
                .ToList();
        }

        public Participation GetById(Guid id)
        {
            return _participationRepository.Get(selector: x => x,
                predicate: x => x.Id == id,
                include: x => x.Include(z => z.Athlete)
                .Include(z => z.Competition)
                .Include(z => z.Owner));
             
        }
    }
}
