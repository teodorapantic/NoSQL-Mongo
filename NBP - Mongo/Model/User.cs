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
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public List<MongoDBRef> RentCars { get; set; }




        [JsonIgnore]
        public List<MongoDBRef> TestDrives { get; set; }

        [JsonIgnore]
        public List<MongoDBRef> MyReviews { get; set; }



        public User()
        {
            RentCars = new List<MongoDBRef>();
            TestDrives = new List<MongoDBRef>();
            MyReviews = new List<MongoDBRef>();
        }
        
    }
}
