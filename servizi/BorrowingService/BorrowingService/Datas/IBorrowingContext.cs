using BorrowingService.Entities;
using MongoDB.Driver;

namespace BorrowingService.Datas
{
    public interface IBorrowingContext
    {
        IMongoCollection<Borrowing> Borrowings { get; }
    }
}
