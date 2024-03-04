using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NBP___Mongo.Model;

namespace NBP___Mongo.DBClient
{
    public interface IDbClient
    {

        IMongoCollection<User> GetUserCollection();

        IMongoCollection<Car> GetCarCollection();

        IMongoCollection<Dealer> GetDealerCollection();

        IMongoCollection<Mark> GetMarkCollection();

        IMongoCollection<CarModel> GetCarModelCollection();

        IMongoCollection<EngineType> GetEngineTypeCollection();

        IMongoCollection<RentCar> GetRentCarCollection();

        IMongoCollection<TestDrive> GetTestDriveCollection();

       IMongoCollection<Review> GetReviewCollection();
        IMongoDatabase GetMongoDB();
    }
}
