using MuseumApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumApplication.Service.Interface
{
    public interface IVisitorHistoryService
    {
        VisitorHistory? GetVisitorHistoryDetails(Guid? id);
    }
}
