using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Domain.DomainModels
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public int Floor { get; set; }
        public virtual ICollection<PatientDepartment>? PatientDepartments { get; set; }
    }
}
