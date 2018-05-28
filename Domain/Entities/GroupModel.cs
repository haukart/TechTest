using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class GroupModel
    {
        [BsonId]
        public ObjectId GroupId { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public DateTime CreateDate { get; set; }

        public DateTime? ModificationDate { get; set; }
    }
}
