using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReceiverRepository:IReceiverRepository
    {
        private readonly AppDbContext _context;

        public ReceiverRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Receiver> Create(Receiver receiver)
        {
            await _context.Receivers.AddAsync(receiver);
            await _context.SaveChangesAsync();
            return receiver;
        } 

        public async Task<List<Receiver>> GetAll()
        {
            return await _context.Receivers
                .Include(a => a.District)
                .Include(a => a.CreatedByUser)
                .ToListAsync();
        }

        public async Task<Receiver?> GetById(int id)
        {
            return await _context.Receivers
                .Include(a => a.District)
                .Include(a => a.CreatedByUser)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Receiver> Update(Receiver receiver)
        {
            _context.Receivers.Update(receiver);
            await _context.SaveChangesAsync();
            return receiver;
        }

        
    }
}
