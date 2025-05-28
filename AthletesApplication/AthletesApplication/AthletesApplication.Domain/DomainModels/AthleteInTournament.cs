namespace AthletesApplication.Domain.DomainModels
{
    public class AthleteInTournament : BaseEntity
    {
        public Guid AthleteId { get; set; }
        public Athlete? Athlete { get; set; }
        public Guid TournamentId { get; set; }
        public Tournament? Tournament { get; set; }
    }
}
