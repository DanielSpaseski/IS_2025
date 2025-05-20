using LibraryApplication.Domain.DomainModels;
using LibraryApplication.Repository.Interface;
using LibraryApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryApplication.Service.Implementation
{
    public class LibrarySectionService : ILibrarySectionService
    {
        private readonly IRepository<LibrarySection> _librarySectionRepository;

        public LibrarySectionService(IRepository<LibrarySection> librarySectionRepository)
        {
            _librarySectionRepository = librarySectionRepository;
        }

        public LibrarySection GetLibrarySectionDetails(Guid id)
        {
            // TODO: Implement method
            return _librarySectionRepository.Get(selector: x => x,
                predicate: x => x.Id == id,
                include: x => x.Include(y => y.BookInSections)
                .ThenInclude(z => z.Book).Include(y => y.Owner));
        }
    }
}
