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
    public class VisitService : IVisitService
    {
        private readonly IRepository<Visit> _repository;
        private readonly IRepository<VisitorHistory> visitorHistoryRepository;
        private readonly IRepository<VisitorInHistory> visitorInHistoryRepository;

        public VisitService(IRepository<Visit> repository, IRepository<VisitorHistory> visitorHistoryRepository, IRepository<VisitorInHistory> visitorInHistoryRepository)
        {
            _repository = repository;
            this.visitorHistoryRepository = visitorHistoryRepository;
            this.visitorInHistoryRepository = visitorInHistoryRepository;
        }

        public Visit AddVisitForVisitorAndArtifact(Guid visitorId, Guid artifactId, string userId)
        {
            var visit = new Visit()
            {
                Id = Guid.NewGuid(),
                VisitorId = visitorId,
                ArtifactId = artifactId,
                DateVisited = DateTime.UtcNow,
                UserId = userId
            };
            _repository.Insert(visit);
            return visit;
        }

        public VisitorHistory CreateVisitorHistory(string userId)
        {
            var visits = this.GetAllByCurrentUser(userId);
            var newVisitorHistory = new VisitorHistory()
            {
                Id = Guid.NewGuid(),
                OwnerId = userId
            };
            visitorHistoryRepository.Insert(newVisitorHistory);
            var visitorsInHistory = visits.Select(x => new VisitorInHistory
            {
                VisitorId = x.VisitorId,
                VisitorHistoryId = newVisitorHistory.Id
            }).ToList();
            foreach(var item in visitorsInHistory)
            {
                visitorInHistoryRepository.Insert(item);
            }
            foreach(var item in visits)
            {
                this.DeleteById(item.Id);
            }
            return newVisitorHistory;
        }

        public Visit DeleteById(Guid id)
        {
            // TODO: Implement method
            var visit = GetById(id);
            return _repository.Delete(visit);
        }

        public List<Visit> GetAll()
        {
            return _repository.GetAll(selector: x => x).ToList();
        }

        public List<Visit> GetAllByCurrentUser(string userId)
        {
            return _repository.GetAll(selector: x => x,
                                      predicate: x => x.UserId == userId,
                                      include: x => x.Include(y => y.Visitor).Include(y => y.Artifact).Include(x => x.User)).ToList();
        }

        public Visit? GetById(Guid id)
        {
            return _repository.Get(selector: x => x,
                                   predicate: x => x.Id == id);
        }
    }
}
