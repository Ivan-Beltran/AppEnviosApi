using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Domain.Interfaces;
namespace Infrastructure.Repositories
{
    public class ShipmentReturnRepository:IShipmentReturn
    {
        private readonly AppDbContext _context;

        public ShipmentReturnRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ShipmentReturn> Create(ShipmentReturn shipmentReturn)
        {
            _context.ShipmentReturns.Add(shipmentReturn);
            await _context.SaveChangesAsync();
            return shipmentReturn;
        }
    }
}
