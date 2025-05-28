using MuseumApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumApplication.Service.Interface
{
    public interface IVisitService
    {
        public List<Visit> GetAllByCurrentUser(string userId);
        public List<Visit> GetAll();
        public Visit? GetById(Guid id);
        public Visit DeleteById(Guid id);
        public Visit AddVisitForVisitorAndArtifact(Guid visitorId, Guid artifactId, string userId);
        public VisitorHistory CreateVisitorHistory(string userId);
    }
}
