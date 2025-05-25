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
    public class TreatmentService : ITreatmentService
    {
        private readonly IRepository<Treatment> _repository;

        public TreatmentService(IRepository<Treatment> repository)
        {
            _repository = repository;
        }

        public Treatment DeleteById(Guid id)
        {
            var department = GetById(id);
            if (department == null)
            {
                throw new ArgumentNullException("Department");
            }
            return _repository.Delete(department);
        }

        public List<Treatment> GetAll()
        {
            return _repository.GetAll(selector: x => x).ToList();
        }

        public Treatment? GetById(Guid id)
        {
            return _repository.Get(selector: x => x,
                predicate: x => x.Id == id);
        }

        public Treatment Insert(Treatment treatment)
        {
            return _repository.Insert(treatment);
        }

        public Treatment Update(Treatment treatment)
        {
            return _repository.Update(treatment);
        }
    }
}
