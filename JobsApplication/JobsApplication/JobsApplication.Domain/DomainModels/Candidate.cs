using System.ComponentModel.DataAnnotations;

namespace JobsApplication.Domain.DomainModels
{
    public class Candidate : BaseEntity
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Application>? Applications { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
