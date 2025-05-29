using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobsApplication.Domain.DomainModels;
using JobsApplication.Service.Interface;

namespace JobsApplication.Web.Controllers
{
    public class JobPositionsController : Controller
    {
        private readonly IJobPositionService _jobPositionService;

        public JobPositionsController(IJobPositionService jobPositionService)
        {
            _jobPositionService = jobPositionService;
        }

        // GET: JobPositions
        public IActionResult Index()
        {
            return View(_jobPositionService.GetAll());
        }

        // GET: JobPositions/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPosition = _jobPositionService.GetById(id.Value);
            if (jobPosition == null)
            {
                return NotFound();
            }

            return View(jobPosition);
        }

        // GET: JobPositions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobPositions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Department,Location")] JobPosition jobPosition)
        {
            if (ModelState.IsValid)
            {
                jobPosition.Id = Guid.NewGuid();
                _jobPositionService.Insert(jobPosition);
                return RedirectToAction(nameof(Index));
            }
            return View(jobPosition);
        }

        // GET: JobPositions/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPosition = _jobPositionService.GetById(id.Value);
            if (jobPosition == null)
            {
                return NotFound();
            }
            return View(jobPosition);
        }

        // POST: JobPositions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Title,Department,Location")] JobPosition jobPosition)
        {
            if (id != jobPosition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _jobPositionService.Update(jobPosition);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPositionExists(jobPosition.Id))
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
            return View(jobPosition);
        }

        // GET: JobPositions/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPosition = _jobPositionService.GetById(id.Value);
            if (jobPosition == null)
            {
                return NotFound();
            }

            return View(jobPosition);
        }

        // POST: JobPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var jobPosition = _jobPositionService.GetById(id);
            if (jobPosition != null)
            {
                _jobPositionService.DeleteById(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool JobPositionExists(Guid id)
        {
            return _jobPositionService.GetById(id) != null;
        }
    }
}
