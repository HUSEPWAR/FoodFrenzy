using System;
using System.Collections.Generic;
using System.Text;
using FoodFrenzy.Application.Interfaces;
using FoodFrenzy.Domain.Entities;
using FoodFrenzy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace FoodFrenzy.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FoodFrenzyDbContext _context;

        public UserRepository(FoodFrenzyDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}