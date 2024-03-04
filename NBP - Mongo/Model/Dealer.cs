using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace NBP___Mongo.Model
{
    public class Dealer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string Name { get; set; }
        
        public string Username { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }

        [JsonIgnore]
        public List<MongoDBRef> Cars { get; set; }
        

        public Dealer()
        {
            Cars = new List<MongoDBRef>();
        }

    }
}
