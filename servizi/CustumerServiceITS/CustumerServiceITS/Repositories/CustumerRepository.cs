using CustumerServiceITS.Datas;
using CustumerServiceITS.Entities;
using MongoDB.Driver;

namespace CustumerServiceITS.Repositories
{
        public class CustumerRepository : ICustumerRepository
        {
            private readonly ICustumerContext _context;

            public CustumerRepository(ICustumerContext context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }


        public async Task CreateCustumer(Custumer custumer)
        {
            await _context.Custumers.InsertOneAsync(custumer);
        }


        public async Task<bool> DeleteCustumer(string id)
            {
                FilterDefinition<Custumer> filter = Builders<Custumer>.Filter.Eq(p => p.Id, id);

                DeleteResult deleteResult = await _context
                                                    .Custumers
                                                    .DeleteOneAsync(filter);

                return deleteResult.IsAcknowledged
                    && deleteResult.DeletedCount > 0;
            }

            public async Task<IEnumerable<Custumer>> GetCustumer()
            {
                return await _context
                                 .Custumers
                                 .Find(p => true)
                                 .ToListAsync();
            }

            public async Task<Custumer> GetCustumer(string id)
            {
                return await _context
                               .Custumers
                               .Find(p => p.Id == id)
                               .FirstOrDefaultAsync();
            }

            public async Task<bool> UpdateCustumer(Custumer custumer)
            {
                var updateResult = await _context
                                           .Custumers
                                           .ReplaceOneAsync(filter: g => g.Id == custumer.Id, replacement: custumer);

                return updateResult.IsAcknowledged
                        && updateResult.ModifiedCount > 0;
            }

        }
    }

