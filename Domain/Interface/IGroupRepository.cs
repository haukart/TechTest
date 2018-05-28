using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using MongoDB.Driver;

namespace Domain.Interface
{
    public interface IGroupRepository
    {
        Task<IEnumerable<GroupModel>> GetAllGroups();
        Task<GroupModel> GetGroup(string groupId);

        Task<string> CreateGroup(GroupModel item);

        Task<DeleteResult> RemoveGroup(string groupId);
        Task<UpdateResult> UpdateGroup(string groupId, string name);
    }
}
