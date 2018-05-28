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
    public class RetailerRepository : IRetailerRepository
    {
        private readonly TechTestContext _context = null;

        public RetailerRepository(IOptions<Settings> settings)
        {
            _context = new TechTestContext(settings);
        }

        public async Task<IEnumerable<RetailerModel>> GetAllRetailers()
        {
            return await _context.Retails.Find(_ => true).ToListAsync();
        }

        public async Task<RetailerModel> GetRetailer(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                throw new Exception();
            } 

            var filter = Builders<RetailerModel>.Filter.Eq("RetailerId", objectId);

            return await _context.Retails
                .Find(filter)
                .FirstOrDefaultAsync();

        }

        public async Task<string> AddRetailer(RetailerModel item)
        {
            await _context.Retails.InsertOneAsync(item);

            var newId = _context.Retails.FindSync(f => f.CreateDate == item.CreateDate).FirstOrDefault();

            return newId.RetailerId.ToString();
        }

        public async Task<DeleteResult> RemoveRetailer(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                throw new Exception();
            }

            var filter = Builders<RetailerModel>.Filter.Eq("RetailerId", objectId);

            return await _context.Retails.DeleteOneAsync(filter);
        }

        public async Task<UpdateResult> UpdateRetailer(string id, string name, string groupId)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                throw new Exception();
            }

            var filter = Builders<RetailerModel>.Filter.Eq("RetailerId", objectId);

            var update = Builders<RetailerModel>.Update
                .Set(s => s.Name, name)
                .Set(s => s.GroupId, groupId)
                .CurrentDate(s => s.ModificationDate);

            return await _context.Retails.UpdateOneAsync(filter, update);
        }
    }
}
