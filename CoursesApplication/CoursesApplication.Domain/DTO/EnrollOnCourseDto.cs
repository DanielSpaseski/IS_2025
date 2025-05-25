namespace CoursesApplication.Domain.DTO;

public class EnrollOnCourseDto
{
    public Guid CourseId { get; set; }
    public Guid SemesterId { get; set; }
    public bool ReEnroll { get; set; }
}