using BookService.Data;
using BookServiceITS.Entities;
using BookServiceITS.Repository.Interfaces;
using MongoDB.Driver;

namespace BookService.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IBooksContext _context;

        public BookRepository(Data.IBooksContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task CreateBook(Book book)
        {
            await _context.Books.InsertOneAsync(book);
        }

        public async Task<bool> DeleteBook(string id)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Books
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Book>> GetBook()
        {
            return await _context
                             .Books
                             .Find(p => true)
                             .ToListAsync();
        }

        public async Task<Book> GetBook(string id)
        {
            return await _context
                           .Books
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateBook(Book book)
        {
            var updateResult = await _context
                                       .Books
                                       .ReplaceOneAsync(filter: g => g.Id == book.Id, replacement: book);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}