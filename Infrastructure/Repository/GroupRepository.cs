using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interface;
using Infrastructure.Config;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly TechTestContext _context = null;

        public GroupRepository(IOptions<Settings> settings)
        {
            _context = new TechTestContext(settings);
        }

        public async Task<IEnumerable<GroupModel>> GetAllGroups()
        {
            return await _context.Group.Find(_ => true).ToListAsync();
        }

        public async Task<GroupModel> GetGroup(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                throw new Exception();
            }

            var filter = Builders<GroupModel>.Filter.Eq("GroupId", objectId);
            return await _context.Group
                .Find(filter)
                .FirstOrDefaultAsync();

        }


        public async Task<string> CreateGroup(GroupModel item)
        {
            await _context.Group.InsertOneAsync(item);

            var newId = _context.Group.FindSync(f => f.CreateDate == item.CreateDate).FirstOrDefault();

            return newId.GroupId.ToString();
        }


        public async Task<DeleteResult> RemoveGroup(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                throw new Exception();
            }

            var filter = Builders<GroupModel>.Filter.Eq("GroupId", objectId);

            return await _context.Group.DeleteOneAsync(filter);
        }

        public async Task<UpdateResult> UpdateGroup(string id, string name)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                throw new Exception();
            }

            var filter = Builders<GroupModel>.Filter.Eq("GroupId", objectId);
            var update = Builders<GroupModel>.Update
                .Set(s => s.Name, name)
                .CurrentDate(s => s.ModificationDate);

            return await _context.Group.UpdateOneAsync(filter, update);
        }
    }
}
