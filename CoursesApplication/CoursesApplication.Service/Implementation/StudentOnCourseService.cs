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
        private readonly ISemesterService _semesterService;
        private readonly ICourseService _courseService;

        public StudentOnCourseService(IRepository<StudentOnCourse> studentOnCourseRepository, ISemesterService semesterService, ICourseService courseService)
        {
            _studentOnCourseRepository = studentOnCourseRepository;
            _semesterService = semesterService;
            _courseService = courseService;
        }

        public StudentOnCourse DeleteById(Guid id)
        {
            var studentOnCourse = GetById(id);
            return _studentOnCourseRepository.Delete(studentOnCourse);
        }

        public StudentOnCourse EnrollOnCourse(string studentId, Guid courseId, Guid semesterId, bool reEnrolled)
        {
            var semester = _semesterService.GetById(semesterId);
            var course = _courseService.GetById(courseId);
            var studentOnCourse = new StudentOnCourse()
            {
                ReEnrollment = reEnrolled,
                SemesterId = semesterId,
                CourseId = courseId,
                StudentId = studentId,
            };
            return _studentOnCourseRepository.Insert(studentOnCourse);
        }

        public List<StudentOnCourse> GetAll()
        {
            return _studentOnCourseRepository.GetAll(selector: x => x).ToList();
                
        }

        public List<StudentOnCourse> GetAllByPassengerId(string passengerId)
        {
            return _studentOnCourseRepository.GetAll(selector: x => x,
                predicate: x => x.StudentId == passengerId,
                include: x => x.Include(y => y.Semester)
                .Include(y => y.Course)).ToList();
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
