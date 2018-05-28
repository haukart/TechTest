using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Config
{
    public class TechTestContext
    {
        private readonly IMongoDatabase _database = null;

        public TechTestContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<RetailerModel> Retails
        {
            get
            {
                return _database.GetCollection<RetailerModel>("Retailer");
            }
        }

        public IMongoCollection<GroupModel> Group
        {
            get
            {
                return _database.GetCollection<GroupModel>("Group");
            }
        }
    }
}
