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
    public class TestDrive
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        public DateTime TestDate { get; set; }

        [JsonIgnore]
        public MongoDBRef User { get; set; }

        public Car Car { get; set; }
        [JsonIgnore]

       
        public MongoDBRef Dealer { get; set; }
    }
}
