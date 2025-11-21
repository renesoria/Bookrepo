using Books_Auth.Models;
using Books_Auth.Models.DTOS;

namespace Books_Auth.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book?> GetOne(Guid id);
        Task<Book> Create(CreateBookDto dto);
        Task<Book?> Update(UpdateBookDto dto, Guid id);
        Task<bool> Delete(Guid id);
    }
}
