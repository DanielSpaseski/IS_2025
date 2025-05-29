using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobsApplication.Domain.DomainModels;
using JobsApplication.Service.Interface;

namespace JobsApplication.Web.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        // GET: Candidates
        public async Task<IActionResult> Index()
        {
            return View(_candidateService.GetAll());
        }

        // GET: Candidates/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = _candidateService.GetById(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // GET: Candidates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,Email,DateOfBirth,PhoneNumber")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                _candidateService.Insert(candidate);
                return RedirectToAction(nameof(Index));
            }
            return View(candidate);
        }

        // GET: Candidates/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = _candidateService.GetById(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }
            return View(candidate);
        }

        // POST: Candidates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,FirstName,LastName,Email,DateOfBirth,PhoneNumber")] Candidate candidate)
        {
            if (id != candidate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _candidateService.Update(candidate);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateExists(candidate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(candidate);
        }

        // GET: Candidates/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = _candidateService.GetById(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var candidate = _candidateService.GetById(id);
            if (candidate != null)
            {
                _candidateService.DeleteById(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateExists(Guid id)
        {
            return _candidateService.GetById(id) != null;
        }
    }
}
