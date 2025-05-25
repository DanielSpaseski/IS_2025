using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesApplication.Domain.DTO
{
    public class CourseDetailsDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ECTS { get; set; }
        public string SemesterType { get; set; }
    }
}
