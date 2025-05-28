using AthletesApplication.Domain.IdentityModels;

namespace AthletesApplication.Domain.DomainModels
{
    public class Tournament : BaseEntity
    {
        public string? OwnerId { get; set; }
        public AthletesApplicationUser Owner { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<AthleteInTournament>? AthletesInTournament { get; set; }
    }
}
