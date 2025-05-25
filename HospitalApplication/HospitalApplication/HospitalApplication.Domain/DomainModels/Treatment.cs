using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Domain.DomainModels
{
    public class Treatment : BaseEntity
    {
        public DateOnly DateAdminstered { get; set; }
        public bool FollowUpRequested { get; set; }
        public Guid PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
