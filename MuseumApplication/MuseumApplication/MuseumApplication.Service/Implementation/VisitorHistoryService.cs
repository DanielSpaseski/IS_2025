using Microsoft.EntityFrameworkCore;
using MuseumApplication.Domain.DomainModels;
using MuseumApplication.Repository.Interface;
using MuseumApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumApplication.Service.Implementation
{
    public class VisitorHistoryService : IVisitorHistoryService
    {
        private readonly IRepository<VisitorHistory> _repository;

        public VisitorHistoryService(IRepository<VisitorHistory> repository)
        {
            _repository = repository;
        }

        public VisitorHistory? GetVisitorHistoryDetails(Guid? id)
        {
            return _repository.Get(selector: x => x,
                predicate: x => x.Id == id,
                include: x => x.Include(y => y.VisitorInHistories)
                .ThenInclude(z => z.Visitor).Include(y => y.Owner));
        }
    }
}
