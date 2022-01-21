using CustumerServiceITS.Entities;
using MongoDB.Driver;

namespace CustumerServiceITS.Datas
{
    public class CustumerContext : ICustumerContext
    {
        public CustumerContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Custumers = database.GetCollection<Custumer>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }
        public IMongoCollection<Custumer> Custumers { get; }


    }
}

