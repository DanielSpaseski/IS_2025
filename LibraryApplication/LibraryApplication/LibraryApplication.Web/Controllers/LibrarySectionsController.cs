using LibraryApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApplication.Web.Controllers
{
    public class LibrarySectionsController : Controller
    {
        private readonly ILibrarySectionService _librarySectionService;

        public LibrarySectionsController(ILibrarySectionService librarySectionService)
        {
            _librarySectionService = librarySectionService;
        }

        // GET: LibrarySections/Details/5
        public IActionResult Details(Guid id)
        {
            // TODO: Implement method
            // Create ViewModel with TotalSamples or pass it through ViewBag/ViewData 
            if (id == null)
            {
                return NotFound();
            }
            var librarySection = _librarySectionService.GetLibrarySectionDetails(id);
            return View(librarySection);

        }
    }
}
