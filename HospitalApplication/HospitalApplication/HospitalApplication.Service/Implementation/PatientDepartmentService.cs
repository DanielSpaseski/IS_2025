using HospitalApplication.Domain.DomainModels;
using HospitalApplication.Repository.Interface;
using HospitalApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Service.Implementation
{
    public class PatientDepartmentService : IPatientDepartmentService
    {
        private readonly IPatientService _patientService;
        private readonly IDepartmentService _departmentService;
        private readonly IRepository<PatientDepartment> _repository;
        private readonly IRepository<TransferRequest> _repositoryTransfer;
        private readonly IRepository<PatientTransfer> _patientTransfer;

        public PatientDepartmentService(IPatientService patientService, IDepartmentService departmentService, IRepository<PatientDepartment> repository, IRepository<TransferRequest> repositoryTransfer, IRepository<PatientTransfer> patientTransferRepository)
        {
            _patientService = patientService;
            _departmentService = departmentService;
            _repository = repository;
            _repositoryTransfer = repositoryTransfer;
            _patientTransfer = patientTransferRepository;
        }

        public PatientDepartment AddPatientToDepartment(Guid patientId, Guid departmentId, string userId)
        {
            var patient = _patientService.GetById(patientId);
            var department = _departmentService.GetById(departmentId);

            if(patient == null || department == null) 
            {
                throw new ArgumentNullException(nameof(patient));
            }

            var patientDepartment = new PatientDepartment()
            {
                Patient = patient,
                Department = department,
                PatientId = patientId,
                DepartmentId = departmentId,
                DateAssigned = DateTime.Now,
                OwnerId = userId
            };

            return _repository.Insert(patientDepartment);
        }

        public TransferRequest CreateTransferRequest(string userId)
        {
            var patientDepartments = this.GetAllByCurrentUser(userId);
            var newTransferRequest = new TransferRequest
            {
                Id = Guid.NewGuid(),
                dateCreated = DateTime.Now,
                OwnerId = userId
            };
            _repositoryTransfer.Insert(newTransferRequest);

            var patientsTransfer = patientDepartments.Select(z => new PatientTransfer
            {
                PatientId = z.PatientId,
                TransferRequestId = newTransferRequest.Id,
            }).ToList();

            foreach(var pt in patientsTransfer)
            {
                _patientTransfer.Insert(pt);
            }

            foreach(var pd in patientDepartments)
            {
                this.DeleteById(pd.Id);
            }
            return newTransferRequest;
        }

        public PatientDepartment DeleteById(Guid id)
        {
            var pd = this.GetById(id);
            return _repository.Delete(pd);
        }

        public List<PatientDepartment> GetAll()
        {
            return _repository.GetAll(selector: x => x).ToList();
        }

        public List<PatientDepartment> GetAllByCurrentUser(string userId)
        {
            return _repository.GetAll(selector: x => x,
                predicate: x => x.OwnerId == userId,
                include: x => x.Include(y => y.Patient).Include(y => y.Department).Include(y => y.Owner)).ToList();
        }

        public PatientDepartment? GetById(Guid id)
        {
            return _repository.Get(selector: x => x,
                predicate: x => x.Id == id,
                include: x => x.Include(y => y.Patient).Include(x => x.Owner).Include(x => x.Department));
        }
    }
}
