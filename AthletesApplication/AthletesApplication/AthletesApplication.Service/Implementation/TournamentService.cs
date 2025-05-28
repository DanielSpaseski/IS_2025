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
    public class TournamentService : ITournamentService
    {
        private readonly IRepository<Tournament> _tournamentRepository;
        private readonly IParticipationService _participationService;
        private readonly IRepository<AthleteInTournament> _athletesInTournamentRepository;

        public TournamentService(IRepository<Tournament> tournamentRepository, IParticipationService participationService, IRepository<AthleteInTournament> athletesInTournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
            _participationService = participationService;
            _athletesInTournamentRepository = athletesInTournamentRepository;
        }

        public Tournament CreateTournament(string userId)
        {
            var participations = _participationService.GetAllByCurrentUser(userId);
            var newTournament = new Tournament
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.Now
            };
            _tournamentRepository.Insert(newTournament);
            var athletesInTournament = participations.Select(z => new AthleteInTournament
            {
                TournamentId = newTournament.Id,
                AthleteId = z.AthleteId
            }).ToList();

            foreach(var item in  athletesInTournament)
            {
                _athletesInTournamentRepository.Insert(item);
            }
            foreach(var item in participations)
            {
                _participationService.DeleteById(item.Id);
            }
            return newTournament;
        }

        public Tournament GetTournamentDetails(Guid id)
        {
            return _tournamentRepository.Get(selector: x => x,
                predicate: x => x.Id == id,
                include: x => x.Include(y => y.AthletesInTournament)
                .ThenInclude(z => z.Athlete).Include(y => y.Owner));
        }
    }
}
