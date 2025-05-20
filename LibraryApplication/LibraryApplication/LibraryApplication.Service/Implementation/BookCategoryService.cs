using LibraryApplication.Domain.DomainModels;
using LibraryApplication.Repository.Interface;
using LibraryApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryApplication.Service.Implementation
{
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IRepository<BookCategory> _bookCategoryRepository;
        private readonly IRepository<LibrarySection> _librarySectionRepository;
        private readonly IRepository<BookInSection> _booksInSectionsRepository;

        public BookCategoryService(IRepository<BookCategory> bookCategoryRepository, IRepository<LibrarySection> librarySectionRepository, IRepository<BookInSection> booksInSectionsRepository)
        {
            _bookCategoryRepository = bookCategoryRepository;
            _librarySectionRepository = librarySectionRepository;
            _booksInSectionsRepository = booksInSectionsRepository;
        }
        public List<BookCategory> GetAllByUser(string userId)
        {
            return _bookCategoryRepository
                .GetAll(
                    selector: x => x,
                    predicate: x => x.OwnerId == userId,
                    include: x => x.Include(z => z.Book).Include(z => z.Category).Include(z => z.Owner)
                ).ToList();
        }

        public BookCategory? GetById(Guid id)
        {
            return _bookCategoryRepository
                .Get(
                    selector: x => x,
                    predicate: x => x.Id == id,
                    include: x => x.Include(z => z.Book).Include(z => z.Category).Include(z => z.Owner)
                );
        }

        public BookCategory AddBookToCategory(string userId, Guid bookId, Guid categoryId, bool isFeatured)
        {
            BookCategory bookCategory = new BookCategory
            {
                Id = Guid.NewGuid(),
                OwnerId = userId,
                BookId = bookId,
                CategoryId = categoryId,
                IsFeatured = isFeatured,
                DateAdded = DateTime.Now
            };

            return _bookCategoryRepository.Insert(bookCategory);
        }

        public BookCategory DeleteById(Guid id)
        {
            // TODO: Implement method
            var bookCategory = this.GetById(id);
            return _bookCategoryRepository.Delete(bookCategory);
        }

        public LibrarySection CreateLibrarySection(string userId)
        {
            // TODO: Implement method
            // Hint: Look at OrderProducts method in auditory exercises

            // Get all BookCategories by current user
            // Create new LibrarySection and insert in database
            // Create new books in section and insert in database
            // Delete all book categories
            // Return section

            var bookCategories = this.GetAllByUser(userId);
            var newLibrarySection = new LibrarySection
            {
                Id = Guid.NewGuid(),
                OwnerId = userId
            };
            _librarySectionRepository.Insert(newLibrarySection);
            var booksInSection = bookCategories.Select(z => new BookInSection
            {
                LibrarySectionId = newLibrarySection.Id,
                BookId = z.BookId    
            }).ToList();

            foreach(var book in booksInSection)
            {
                _booksInSectionsRepository.Insert(book);
            }
            foreach(var book in bookCategories)
            {
                this.DeleteById(book.Id);
            }
            return newLibrarySection;
        }
    }
}
