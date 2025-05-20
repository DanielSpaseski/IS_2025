using LibraryApplication.Domain.IdentityModels;

namespace LibraryApplication.Domain.DomainModels
{
    public class BookInSection : BaseEntity
    {
        public Guid LibrarySectionId { get; set; }
        public LibrarySection? LibrarySection { get; set; }
        public Guid BookId { get; set; }
        public Book? Book { get; set; }
    }
}
