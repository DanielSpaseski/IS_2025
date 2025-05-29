using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesApplication.Service.Implementation
{
    public class SemesterService : ISemesterService
    {
        private readonly IRepository<Semester> _semesterRepository;

        public SemesterService(IRepository<Semester> semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }

        public Semester DeleteById(Guid id)
        {
            var semester = this.GetById(id);
            return _semesterRepository.Delete(semester);
        }

        public List<Semester> GetAll()
        {
            return _semesterRepository.GetAll(selector: x => x).ToList();
        }

        public Semester? GetById(Guid id)
        {
            return _semesterRepository.Get(selector: x => x,
                predicate: x => x.Id == id);    
        }

        public Semester Insert(Semester flight)
        {
            return _semesterRepository.Insert(flight);
        }

        public ICollection<Semester> InsertMany(ICollection<Semester> flights)
        {
            return _semesterRepository.InsertMany(flights);
        }

        public Semester Update(Semester flight)
        {
            return _semesterRepository.Update(flight);
        }
    }
}
