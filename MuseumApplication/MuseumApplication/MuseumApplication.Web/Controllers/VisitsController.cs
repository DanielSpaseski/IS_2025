using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MuseumApplication.Domain.DomainModels;
using MuseumApplication.Repository.Data;
using MuseumApplication.Service.Implementation;
using MuseumApplication.Service.Interface;

namespace MuseumApplication.Web.Controllers
{
    public class VisitsController : Controller
    {
        private readonly IVisitService visitService;
        private readonly IArtifactService artifactService;
        private readonly IVisitorHistoryService visitorHistoryService;

        public VisitsController(IVisitService visitService, IArtifactService artifactService, IVisitorHistoryService visitorHistoryService)
        {
            this.visitService = visitService;
            this.artifactService = artifactService;
            this.visitorHistoryService = visitorHistoryService;
        }

        // GET: Visits
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(visitService.GetAllByCurrentUser(userId));
        }


        // GET: Visits/Create
        // TODO: Add the VisitorId as parameter and use it in the view as a value for the hidden input
        // You can make a separate ViewModel or send the parameter via ViewData
        // Use the SelectList to populate the drop-down list in the view
        // Replace the usage of ApplicationDbContext with the appropriate service
        public IActionResult Create(Guid visitorId)
        {
            ViewData["VisitorId"] = visitorId;
            ViewData["ArtifactId"] = new SelectList(artifactService.GetAll(), "Id", "Description");
            return View();
        }

        // POST: Visits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // TODO: Bind the form from the view to this POST action in order to create the Visit
        // Implement the IVisitService and use it here to create the visit
        // After successful creation, the user should be redirected to Index page of Visitors

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,VisitorId,ArtifactId,DateVisited")] Visit visit)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            visitService.AddVisitForVisitorAndArtifact(visit.VisitorId, visit.ArtifactId, userId);
            return RedirectToAction(nameof(Index));
            
            
        }
        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            // TODO: Implement method
            visitService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Visits/CreateTransferRequest
        [HttpPost, ActionName("CreateVisitorHistory")]
        [Authorize]
        public IActionResult CreateVisitorHistory()
        {
            // TODO: Implement method
            // Find current user, call service method, redirect to details in TransferRequests controller
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newVisitorHistory = visitService.CreateVisitorHistory(userId);
            return RedirectToAction("VisitorHistories","Details", new {id = newVisitorHistory.Id});
        }
    }
}
