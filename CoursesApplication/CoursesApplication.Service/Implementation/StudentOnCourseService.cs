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
    public class StudentOnCourseService : IStudentOnCourseService
    {
        private readonly IRepository<StudentOnCourse> _studentOnCourseRepository;

        public StudentOnCourseService(IRepository<StudentOnCourse> studentOnCourseRepository)
        {
            _studentOnCourseRepository = studentOnCourseRepository;
        }

        public StudentOnCourse DeleteById(Guid id)
        {
            var studentOnCourse = this.GetById(id);
            return _studentOnCourseRepository.Delete(studentOnCourse);
        }

        public StudentOnCourse EnrollOnCourse(string studentId, Guid courseId, Guid semesterId, bool reEnrolled)
        {
            var newStudentOnCourse = new StudentOnCourse
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                CourseId = courseId,
                SemesterId = semesterId,
                ReEnrollment = reEnrolled
            };
            return _studentOnCourseRepository.Insert(newStudentOnCourse);
        }

        public List<StudentOnCourse> GetAll()
        {
            return _studentOnCourseRepository.GetAll(selector: x => x).ToList();
        }

        public List<StudentOnCourse> GetAllByPassengerId(string passengerId)
        {
            return _studentOnCourseRepository.GetAll(selector: x => x,
                predicate: x => x.StudentId == passengerId,
                include: x => x.Include(y => y.Course)
                .Include(y => y.Semester)).ToList();
        }

        public StudentOnCourse? GetById(Guid id)
        {
            return _studentOnCourseRepository.Get(selector: x => x,
                predicate: x => x.Id == id);
        }

        public StudentOnCourse Insert(StudentOnCourse flight)
        {
            return _studentOnCourseRepository.Insert(flight);
        }

        public StudentOnCourse Update(StudentOnCourse flight)
        {
            return _studentOnCourseRepository.Update(flight);
        }
    }
}
