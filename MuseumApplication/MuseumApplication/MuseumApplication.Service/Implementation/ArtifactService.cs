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
    public class ArtifactService : IArtifactService
    {
        private readonly IRepository<Artifact> _artifactRepository;


        public ArtifactService(IRepository<Artifact> artifactRepository)
        {
            _artifactRepository = artifactRepository;
        }

        public Artifact DeleteById(Guid id)
        {
            var artifact = GetById(id);
            return _artifactRepository.Delete(artifact);
        }

        public List<Artifact> GetAll()
        {
            return _artifactRepository.GetAll(x => x).ToList();
        }

        public Artifact? GetById(Guid id)
        {
            return _artifactRepository.Get(selector: x => x,
                predicate: x => x.Id == id);
        }

        public Artifact Insert(Artifact artifact)
        {
            return _artifactRepository.Insert(artifact);
        }

        public Artifact Update(Artifact artifact)
        {
            return _artifactRepository.Update(artifact);
        }
    }
}
