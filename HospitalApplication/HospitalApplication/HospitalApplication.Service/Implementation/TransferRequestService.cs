using HospitalApplication.Domain.DomainModels;
using HospitalApplication.Repository.Interface;
using HospitalApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Service.Implementation
{
    public class TransferRequestService : ITransferRequestService
    {
        private readonly IRepository<TransferRequest> _repository;

        public TransferRequestService(IRepository<TransferRequest> repository)
        {
            _repository = repository;
        }

        public TransferRequest GetTransferRequestDetails(Guid id)
        {
            return _repository.Get(selector: x => x,
                predicate: x => x.Id == id,
                include: x => x.Include(y => y.PatientTransfers)
                .ThenInclude(z => z.Patient).Include(y => y.Owner));
        }
    }
}
