using HospitalApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Service.Interface
{
    public interface IPatientDepartmentService
    {
        List<PatientDepartment> GetAll();
        List<PatientDepartment> GetAllByCurrentUser(string userId);
        PatientDepartment? GetById(Guid id);
        PatientDepartment DeleteById(Guid id);
        TransferRequest CreateTransferRequest(string userId);
        PatientDepartment AddPatientToDepartment(Guid patientId, Guid departmentId, string userId);
    }
}
