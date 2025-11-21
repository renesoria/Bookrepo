using Microsoft.EntityFrameworkCore;
using Books_Auth.Data;
using Books_Auth.Models;
using System;

namespace Books_Auth.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _db;

        public BookRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _db.Books.ToListAsync();
        }

        public async Task<Book?> GetOne(Guid id)
        {
            return await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task Add(Book book)
        {
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Book book)
        {
            _db.Books.Update(book);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Book book)
        {
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
        }
    }
}
