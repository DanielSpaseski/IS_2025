using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesApplication.Service.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseRepository;

        public CourseService(IRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Course DeleteById(Guid id)
        {
            var course = GetById(id);
            return _courseRepository.Delete(course);
        }

        public List<Course> GetAll()
        {
            return _courseRepository.GetAll(selector: x => x).ToList();
        }

        public Course? GetById(Guid id)
        {
            return _courseRepository.Get(selector: x => x,
                predicate: x => x.Id == id,
                include: x => x
                .Include(y => y.EnrolledStudents).ThenInclude(z => z.Semester)
                .Include(y => y.EnrolledStudents).ThenInclude(z => z.Student));
        }

        public Course Insert(Course course)
        {
            return _courseRepository.Insert(course);
        }

        public ICollection<Course> InsertMany(ICollection<Course> courses)
        {
            return _courseRepository.InsertMany(courses);
        }

        public Course Update(Course flight)
        {
            return _courseRepository.Update(flight);
        }
    }
}
