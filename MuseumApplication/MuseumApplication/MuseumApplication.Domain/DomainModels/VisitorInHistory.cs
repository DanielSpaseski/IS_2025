using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumApplication.Domain.DomainModels
{
    public class VisitorInHistory : BaseEntity
    {
        public Guid VisitorId { get; set; }
        public Visitor? Visitor { get; set; }
        public VisitorHistory? VisitorHistory { get; set; }
        public Guid VisitorHistoryId { get; set; }
    }
}
