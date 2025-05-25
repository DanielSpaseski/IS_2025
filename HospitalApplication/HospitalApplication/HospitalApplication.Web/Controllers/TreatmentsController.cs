using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalApplication.Domain.DomainModels;
using HospitalApplication.Repository.Data;
using HospitalApplication.Service.Interface;

namespace HospitalApplication.Web.Controllers
{
    public class TreatmentsController : Controller
    {
        private readonly ITreatmentService _treatmentService;
        private readonly IPatientService _patientService;

        public TreatmentsController(ITreatmentService treatmentService, IPatientService patientService)
        {
            _treatmentService = treatmentService;
            _patientService = patientService;
        }

        // GET: Treatments
        public async Task<IActionResult> Index()
        {
            return View(_treatmentService.GetAll());
        }

        // GET: Treatments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treatment = _treatmentService.GetById(id.Value);
            if (treatment == null)
            {
                return NotFound();
            }

            return View(treatment);
        }

        // GET: Treatments/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_patientService.GetAll(), "Id", "FirstName");
            return View();
        }

        // POST: Treatments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateAdminstered,FollowUpRequested,PatientId")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                _treatmentService.Insert(treatment);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_patientService.GetAll(), "Id", "FirstName", treatment.PatientId);
            return View(treatment);
        }

        // GET: Treatments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treatment = _treatmentService.GetById(id.Value);
            if (treatment == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_patientService.GetAll(), "Id", "FirstName", treatment.PatientId);
            return View(treatment);
        }

        // POST: Treatments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateAdminstered,FollowUpRequested,PatientId")] Treatment treatment)
        {
            if (id != treatment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _treatmentService.Update(treatment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreatmentExists(treatment.Id))
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
            ViewData["PatientId"] = new SelectList(_patientService.GetAll(), "Id", "FirstName", treatment.PatientId);
            return View(treatment);
        }

        // GET: Treatments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treatment = _treatmentService.GetById(id.Value);
            if (treatment == null)
            {
                return NotFound();
            }

            return View(treatment);
        }

        // POST: Treatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _treatmentService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TreatmentExists(Guid id)
        {
            return _treatmentService.GetById(id) != null;
        }
    }
}
