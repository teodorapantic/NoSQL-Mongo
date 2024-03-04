using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBP___Mongo.Model
{
    public class EngineType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public String FuelType { get; set; }

        public String Displacement { get; set; }

        public int Power { get; set; }
    }
}
