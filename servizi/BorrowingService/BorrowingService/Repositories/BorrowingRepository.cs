using BorrowingService.Datas;
using BorrowingService.Entities;
using MongoDB.Driver;

namespace BorrowingService.Repositories
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly IBorrowingContext _context;

        public BorrowingRepository(Datas.IBorrowingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task CreateBorrowing(Borrowing borrowing)
        {
            await _context.Borrowings.InsertOneAsync(borrowing);
        }

        public async Task<bool> DeleteBorrowing(string id)
        {
            FilterDefinition<Borrowing> filter = Builders<Borrowing>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Borrowings
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Borrowing>> GetBorrowing()
        {
            return await _context
                             .Borrowings
                             .Find(p => true)
                             .ToListAsync();
        }

        public async Task<Borrowing> GetBorrowing(string id)
        {
            return await _context
                           .Borrowings
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateBorrowing(Borrowing borrowing)
        {
            var updateResult = await _context
                                       .Borrowings
                                       .ReplaceOneAsync(filter: g => g.Id == borrowing.Id, replacement: borrowing);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
