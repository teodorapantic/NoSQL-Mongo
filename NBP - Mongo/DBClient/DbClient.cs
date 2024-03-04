using MongoDB.Driver;
using NBP___Mongo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NBP___Mongo.DBClient
{
    public class DbClient : IDbClient
    {

        public MongoClient client;
        public IMongoDatabase database;
        
        public DbClient()
        {
            client = new MongoClient("mongodb+srv://NBP:NBP2023%40@spk-2023.bjooeit.mongodb.net/test");
            database = client.GetDatabase("cardealerDB");
        }

        public IMongoCollection<Car> GetCarCollection()
        {
            return database.GetCollection<Car>("cars");
        }

        public IMongoCollection<CarModel> GetCarModelCollection()
        {
            return database.GetCollection<CarModel>("carModel");

        }

        public IMongoCollection<Dealer> GetDealerCollection()
        {
            return database.GetCollection<Dealer>("dealers");
            
        }

        public IMongoCollection<EngineType> GetEngineTypeCollection()
        {
            return database.GetCollection<EngineType>("engineType");

        }

        public IMongoCollection<Mark> GetMarkCollection()
        {
            return database.GetCollection<Mark>("mark");
        }

        public IMongoDatabase GetMongoDB()
        {
            return database;
        }

        public IMongoCollection<RentCar> GetRentCarCollection()
        {
            return database.GetCollection<RentCar>("rentCar");
        }

        public IMongoCollection<Review> GetReviewCollection()
        {
            return database.GetCollection<Review>("reviews");
        }

        public IMongoCollection<TestDrive> GetTestDriveCollection()
        {
            return database.GetCollection<TestDrive>("testDrive");
        }

        public IMongoCollection<User> GetUserCollection()
        {
            return database.GetCollection<User>("users");
        }



    }
}
