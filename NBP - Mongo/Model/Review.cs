using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Text.Json.Serialization;

namespace NBP___Mongo.Model
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }

        [JsonIgnore]
        public MongoDBRef Car { get; set; }

        public User User { get; set; }

        public String Text { get; set; }

    }
}
