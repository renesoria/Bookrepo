using Books_Auth.Models;

namespace Books_Auth.Repositories
{
      public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book?> GetOne(Guid id);
        Task Add(Book book);
        Task Update(Book book);
        Task Delete(Book book);
    }
}
