using Microsoft.AspNetCore.Mvc;
using MuseumApplication.Service.Interface;

namespace MuseumApplication.Web.Controllers
{
    public class VisitorHistoriesController : Controller
    {
        private readonly IVisitorHistoryService _visitorHistoryService;

        public VisitorHistoriesController(IVisitorHistoryService visitorHistoryService)
        {
            _visitorHistoryService = visitorHistoryService;
        }

        // GET: VisitorHistories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            // TODO: Implement method
            // Create ViewModel with Count or pass it through ViewBag/ViewData
            _visitorHistoryService.GetVisitorHistoryDetails(id);
            return View();
        }
    }
}
