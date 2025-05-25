using HospitalApplication.Domain.DomainModels;
using HospitalApplication.Repository.Interface;
using HospitalApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Service.Implementation
{
    public class PatientService : IPatientService
    {
        IRepository<Patient> _repository;

        public PatientService(IRepository<Patient> repository)
        {
            _repository = repository;
        }

        public Patient DeleteById(Guid id)
        {
            var department = GetById(id);
            if (department == null)
            {
                throw new ArgumentNullException("Department");
            }
            return _repository.Delete(department);
        }

        public List<Patient> GetAll()
        {
            return _repository.GetAll(selector: x => x).ToList();
        }

        public Patient? GetById(Guid id)
        {
            return _repository.Get(selector: x => x,
                                    predicate: x => x.Id == id);
        }

        public Patient Insert(Patient patient)
        {
            return _repository.Insert(patient);
        }

        public Patient Update(Patient patient)
        {
            return _repository.Update(patient);
        }
    }
}
