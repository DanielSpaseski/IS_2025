using MuseumApplication.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumApplication.Domain.DomainModels
{
    public class VisitorHistory : BaseEntity
    {
        public string? OwnerId { get; set; }
        public MuseumApplicationUser Owner { get; set; }
        public virtual ICollection<VisitorInHistory>? VisitorInHistories { get; set; }
    }
}
