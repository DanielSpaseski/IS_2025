using Microsoft.AspNetCore.Identity;

namespace JobsApplication.Domain.IdentityModels
{
    public class JobsApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
