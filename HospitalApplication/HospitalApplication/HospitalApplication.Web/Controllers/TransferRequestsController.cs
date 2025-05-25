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
    public class TransferRequestsController : Controller
    {
        private readonly ITransferRequestService _transferRequestService;

        public TransferRequestsController(ITransferRequestService transferRequestService)
        {
            _transferRequestService = transferRequestService;
        }


        // GET: TransferRequests/Details/5
        public IActionResult Details(Guid id)
        {
            // TODO: Implement method
            // Create ViewModel with Count or pass it through ViewBag/ViewData
         
            var transferRequest = _transferRequestService.GetTransferRequestDetails(id);
            if(transferRequest == null)
            {
                return NotFound();
            }
            //ViewData["Count"] = transferRequest.PatientTransfers.Count;
            return View(transferRequest);
        }
    }
}
