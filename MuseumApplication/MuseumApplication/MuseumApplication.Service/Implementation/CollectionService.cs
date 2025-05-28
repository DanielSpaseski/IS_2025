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
    public class CollectionService : ICollectionService
    {
        private readonly IRepository<Collection> _collectionRepository;

        public CollectionService(IRepository<Collection> collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }

        public Collection DeleteById(Guid id)
        {
            var collection = GetById(id);
            return _collectionRepository.Delete(collection);
        }

        public List<Collection> GetAll()
        {
            return _collectionRepository.GetAll(selector: x => x).ToList();
        }

        public Collection? GetById(Guid id)
        {
            return _collectionRepository.Get(selector: x => x,
                predicate: x => x.Id == id);
        }

        public Collection Insert(Collection collection)
        {
            return _collectionRepository.Insert(collection);
        }

        public Collection Update(Collection collection)
        {
            return _collectionRepository.Update(collection);
        }
    }
}
