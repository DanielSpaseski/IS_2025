using MuseumApplication.Domain.DomainModels;
using MuseumApplication.Repository.Interface;
using MuseumApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MuseumApplication.Service.Implementation
{
    public class VisitorService : IVisitorService
    {
        private readonly IRepository<Visitor> _visitorRepository;

        public VisitorService(IRepository<Visitor> visitorRepository)
        {
            _visitorRepository = visitorRepository;
        }

        public Visitor DeleteById(Guid id)
        {
            var visitor = GetById(id);
            return _visitorRepository.Delete(visitor);
        }

        public List<Visitor> GetAll()
        {
            return _visitorRepository.GetAll(selector: x => x).ToList();
        }

        public Visitor? GetById(Guid id)
        {
            return _visitorRepository.Get(selector: x => x,
                predicate: x =>  x.Id == id);
        }

        public Visitor Insert(Visitor visitor)
        {
            return _visitorRepository.Insert(visitor);
        }

        public Visitor Update(Visitor visitor)
        {
            return _visitorRepository.Update(visitor);
        }
    }
}
