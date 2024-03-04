
using MongoDB.Driver;
﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace NBP___Mongo.Model
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public CarModel CarModel { get; set; }

     
        public Mark Mark { get; set; }

        public  String ExteriorColor { get; set; }

        public String InteriorColor { get; set; }

        public String Drivetrain  { get; set; }

     
        public EngineType EngineType { get; set; }

        [JsonIgnore]

        public List<MongoDBRef> Reviews { get; set; }

        public String Description { get; set; }

        public String Year { get; set; }

        public double Price { get; set; }

        public bool RentOrSale { get; set; }

        public bool Available { get; set; }

        [JsonIgnore]
        public MongoDBRef Dealer{ get; set; }

        public string Picture;

        public Car()
        {
          
            Reviews = new List<MongoDBRef>();

        }



    }
}
