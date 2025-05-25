using HospitalApplication.Domain.IdenitityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Domain.DomainModels
{
    public class TransferRequest : BaseEntity
    {
        public DateTime dateCreated {  get; set; }
        public string? OwnerId { get; set; }
        public HospitalApplicationUser? Owner { get; set; }
        public virtual ICollection<PatientTransfer>? PatientTransfers { get; set; }
    }
}
