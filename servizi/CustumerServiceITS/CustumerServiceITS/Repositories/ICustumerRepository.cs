using CustumerServiceITS.Entities;

namespace CustumerServiceITS.Repositories
{
    public interface ICustumerRepository
    {
        Task<IEnumerable<Custumer>> GetCustumer();
        Task<Custumer> GetCustumer(string id);
        Task CreateCustumer(Custumer custumer);
        Task<bool> UpdateCustumer(Custumer custumer);
        Task<bool> DeleteCustumer(string id);
    }
}
