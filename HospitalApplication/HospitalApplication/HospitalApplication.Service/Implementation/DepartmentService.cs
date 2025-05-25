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
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _repository;

        public DepartmentService(IRepository<Department> repository)
        {
            _repository = repository;
        }

        public Department DeleteById(Guid id)
        {
            var department = GetById(id);
            if (department == null)
            {
                throw new ArgumentNullException("Department");
            }
            return _repository.Delete(department);
        }

        public List<Department> GetAll()
        {
            return _repository.GetAll(selector: x => x).ToList();
        }

        public Department? GetById(Guid id)
        {
            return _repository.Get(selector: x => x,
                predicate: x => x.Id == id);
        }

        public Department Insert(Department department)
        {
            return _repository.Insert(department);
        }

        public Department Update(Department department)
        {
            return _repository.Update(department);
        }
    }
}
