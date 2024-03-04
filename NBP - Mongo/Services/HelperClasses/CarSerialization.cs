using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using NBP___Mongo.Model;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace NBP___Mongo.Services.HelperClasses
{
    public class CarSerialization
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public CarModel CarModel { get; set; }


        public Mark Mark { get; set; }

        public String ExteriorColor { get; set; }

        public String InteriorColor { get; set; }

        public String Drivetrain { get; set; }


        public EngineType EngineType { get; set; }

        [JsonIgnore]

        public List<MongoDBRef> Reviews { get; set; }

        public String Description { get; set; }

        public String Year { get; set; }

        public double Price { get; set; }

        public bool RentOrSale { get; set; }

        public bool Available { get; set; }

        public string Dealer { get; set; }

        public string Picture;

        public CarSerialization()
        {

            Reviews = new List<MongoDBRef>();

        }



    }
}
