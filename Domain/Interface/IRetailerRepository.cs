using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using MongoDB.Driver;

namespace Domain.Interface
{
    public interface IRetailerRepository
    {
        Task<IEnumerable<RetailerModel>> GetAllRetailers();
        Task<RetailerModel> GetRetailer(string id);

        Task<string> AddRetailer(RetailerModel item);

        Task<DeleteResult> RemoveRetailer(string id);
        Task<UpdateResult> UpdateRetailer(string id, string name, string groupId);
    }
}
