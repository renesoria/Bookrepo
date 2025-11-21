using Books_Auth.Models;
using Books_Auth.Models.DTOS;
using Books_Auth.Repositories;

namespace Books_Auth.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;

        public BookService(IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Book?> GetOne(Guid id)
        {
            return await _repo.GetOne(id);
        }

        public async Task<Book> Create(CreateBookDto dto)
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Year = dto.Year
            };

            await _repo.Add(book);
            return book;
        }

        public async Task<Book?> Update(UpdateBookDto dto, Guid id)
        {
            var existing = await _repo.GetOne(id);
            if (existing == null) return null;

            existing.Title = dto.Title;
            existing.Year = dto.Year;

            await _repo.Update(existing);
            return existing;
        }

        public async Task<bool> Delete(Guid id)
        {
            var book = await _repo.GetOne(id);
            if (book == null) return false;

            await _repo.Delete(book);
            return true;
        }
    }
}
