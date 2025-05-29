using JobsApplication.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobsApplication.Web.Controllers
{
    public class ProfessionsController : Controller
    {
        private readonly IProfessionService _professionService;

        public ProfessionsController(IProfessionService professionService)
        {
            _professionService = professionService;
        }

        // GET: Professions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create()
        {
            // TODO: Implement method
            // Get current user, call service method, redirect to Details
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newProfession = _professionService.Create(userId);
            return RedirectToAction("Details", new {id = newProfession.Id});
        }


        // Bonus task
        // GET: Professions/Details/5
        public IActionResult Details(Guid id)
        {
            // TODO: Implement method
            return View(_professionService.GetDetailsById(id));
        }
    }
}
