using MongoDB.Driver;
using CustumerServiceITS.Entities;

namespace CustumerServiceITS.Datas
{
    public interface ICustumerContext
    {
        IMongoCollection<Custumer> Custumers { get; }
    }
}
