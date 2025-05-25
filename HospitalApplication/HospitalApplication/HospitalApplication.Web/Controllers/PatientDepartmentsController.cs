using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalApplication.Domain.DomainModels;
using HospitalApplication.Repository.Data;
using HospitalApplication.Service.Implementation;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using HospitalApplication.Service.Interface;

namespace HospitalApplication.Web.Controllers
{
    public class PatientDepartmentsController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IPatientDepartmentService _patientDepartmentService;

        public PatientDepartmentsController(IDepartmentService departmentService, IPatientDepartmentService patientDepartmentService)
        {
            _departmentService = departmentService;
            _patientDepartmentService = patientDepartmentService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(_patientDepartmentService.GetAllByCurrentUser(userId));
        }

        // GET: PatientDepartments/Create
        public IActionResult Create(Guid patientId)
        {
            ViewData["PatientId"] = patientId;
            ViewData["DepartmentId"] = new SelectList(_departmentService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: PatientDepartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see h
        // ttp://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,PatientId,DepartmentId,DateAssigned")] PatientDepartment patientDepartment)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _patientDepartmentService.AddPatientToDepartment(patientDepartment.PatientId, patientDepartment.DepartmentId, userId);
            return RedirectToAction(nameof(Index));
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            // TODO: Implement method
            _patientDepartmentService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: PatientDepartments/CreateTransferRequest
        [HttpPost, ActionName("CreateTransferRequest")]
        [Authorize]
        public IActionResult CreateTransferRequest()
        {
            // TODO: Implement method
            // Find current user, call service method, redirect to details in TransferRequests controller
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newTransferRequest = _patientDepartmentService.CreateTransferRequest(userId);
            return RedirectToAction("Details", "TransferRequests", new { id = newTransferRequest.Id });
        }
    }
}
