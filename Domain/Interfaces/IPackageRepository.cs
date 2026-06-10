using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPackageRepository
    {
        Task<Package> CreateAsync(Package package);
        Task<List<Package>> GetByShipmentIdAsync(int shipmentId);
    }
}
