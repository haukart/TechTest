using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class RetailerModel
    {
        [BsonId]
        public ObjectId RetailerId { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        public string GroupId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModificationDate { get; set; }

    }
}
