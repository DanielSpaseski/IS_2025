using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobsApplication.Domain.DomainModels;
using JobsApplication.Service.Interface;
using JobsApplication.Service.Implementation;

namespace JobsApplication.Web.Controllers
{
    public class InterviewsController : Controller
    {
        private readonly IInterviewService _interviewService;
        private readonly IJobPositionService _jobPositionService;

        public InterviewsController(IInterviewService interviewService, IJobPositionService jobPositionService)
        {
            _interviewService = interviewService;
            _jobPositionService = jobPositionService;
        }

        // GET: Interviews
        public IActionResult Index()
        {
            return View(_interviewService.GetAll());
        }

        // GET: Interviews/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = _interviewService.GetById(id.Value);
            if (interview == null)
            {
                return NotFound();
            }

            return View(interview);
        }

        // GET: Interviews/Create
        public IActionResult Create()
        {
            ViewData["JobPositionId"] = new SelectList(_jobPositionService.GetAll(), "Id", "Department");
            return View();
        }

        // POST: Interviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,InterviewDate,InterviewType,Notes,JobPositionId")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                _interviewService.Insert(interview);
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobPositionId"] = new SelectList(_jobPositionService.GetAll(), "Id", "Department", interview.JobPositionId);
            return View(interview);
        }

        // GET: Interviews/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = _interviewService.GetById(id.Value);
            if (interview == null)
            {
                return NotFound();
            }
            ViewData["JobPositionId"] = new SelectList(_jobPositionService.GetAll(), "Id", "Department", interview.JobPositionId);
            return View(interview);
        }

        // POST: Interviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,InterviewDate,InterviewType,Notes,JobPositionId")] Interview interview)
        {
            if (id != interview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _interviewService.Update(interview);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterviewExists(interview.Id))
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
            ViewData["JobPositionId"] = new SelectList(_jobPositionService.GetAll(), "Id", "Department", interview.JobPositionId);
            return View(interview);
        }

        // GET: Interviews/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = _interviewService.GetById(id.Value);
            if (interview == null)
            {
                return NotFound();
            }

            return View(interview);
        }

        // POST: Interviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var interview = _interviewService.GetById(id);
            if (interview != null)
            {
                _interviewService.DeleteById(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool InterviewExists(Guid id)
        {
            return _interviewService.GetById(id) != null;
        }
    }
}
