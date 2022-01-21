using BookServiceITS.Entities;
using MongoDB.Driver;

namespace BookService.Data
{
    public interface IBooksContext
    {
        IMongoCollection<Book> Books { get; }
    }
}