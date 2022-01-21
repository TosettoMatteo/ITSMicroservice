using BookServiceITS.Entities;

namespace BookServiceITS.Repository.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBook();
        Task<Book> GetBook(string id);

        Task CreateBook(Book book);
        Task<bool> UpdateBook(Book book);
        Task<bool> DeleteBook(string id);
    }
}