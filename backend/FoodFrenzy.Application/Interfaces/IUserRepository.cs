using System;
using System.Collections.Generic;
using System.Text;
using FoodFrenzy.Domain.Entities;

namespace FoodFrenzy.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task SaveChangesAsync();
    }
}