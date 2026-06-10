using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReceiverRepository
    {
        Task<Receiver> Create(Receiver receiver);
        Task<Receiver?> GetById(int id);
        Task<List<Receiver>> GetAll();
        Task<Receiver> Update(Receiver receiver);
    }
}
