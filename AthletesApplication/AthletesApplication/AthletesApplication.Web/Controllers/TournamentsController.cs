﻿using AthletesApplication.Service.Implementation;
using AthletesApplication.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AthletesApplication.Web.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly ITournamentService _tournamensService;

        public TournamentsController(ITournamentService tournamensService)
        {
            _tournamensService = tournamensService;
        }

        // POST: Tournaments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create()
        {
            // TODO: Implement method
            // 1. Get current user
            // 2. Call method CreateTournament from _tournamensService
            // 3. Redirect to Details
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newTournament = _tournamensService.CreateTournament(userId);
            return RedirectToAction("Details", new {id = newTournament.Id});
            
        }

        // GET: Tournaments/Details/5
        // Bonus task
        public IActionResult Details(Guid id)
        {
            // TODO: Implement method
            // Call service method, return view with tournament

            var tournament = _tournamensService.GetTournamentDetails(id);
            return View(tournament);
        }
    }
}
