using LibraryApplication.Domain.IdentityModels;

namespace LibraryApplication.Domain.DomainModels
{
    public class LibrarySection : BaseEntity
    {
        public string? OwnerId { get; set; }
        public LibraryApplicationUser? Owner { get; set; }
        public virtual ICollection<BookInSection>? BookInSections { get; set; }
    }
}
