using System.ComponentModel.DataAnnotations;

namespace JobsApplication.Domain
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
