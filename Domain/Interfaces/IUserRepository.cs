using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task <User> Create(User user);

        Task<User?> GetByEmailWithRole(string email);

        Task<User?> GetById(int id);

        Task<User> Update(User user);

        Task<IDbContextTransaction> BeginTransactionAsync();

    }
}
