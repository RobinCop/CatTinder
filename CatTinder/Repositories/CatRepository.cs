using CatTinder.Data;
using CatTinder.Models;
using CatTinder.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;

namespace CatTinder.Repositories
{
    public class CatRepository : ICatRepository
    {
        private readonly ApplicationDbContext _context;

        public CatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cat>> GetAllCatsAsync()
        {
            return await _context.Cats.ToListAsync();
        }

        public async Task<Cat?> GetCatByIdAsync(int id)
        {   
            return await _context.Cats.FindAsync(id);
        }

        public async Task AddCatAsync(Cat cat)
        {
            await _context.Cats.AddAsync(cat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCatAsync(Cat cat)
        {
            _context.Cats.Update(cat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCatAsync(int id)
        {
            var cat = await _context.Cats.FindAsync(id);
            if (cat == null)
                throw new KeyNotFoundException($"Cat with ID: {id} not found");

            _context.Cats.Remove(cat);
            await _context.SaveChangesAsync();

        }
    }

}
