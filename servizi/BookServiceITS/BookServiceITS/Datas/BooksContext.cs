using BookServiceITS.Entities;
using MongoDB.Driver;

namespace BookService.Data
{
    public class BookContext : IBooksContext
    {
        public BookContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Books = database.GetCollection<Book>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }
        public IMongoCollection<Book> Books { get; }
    }
}
