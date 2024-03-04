using MongoDB.Driver;
using NBP___Mongo.DBClient;
using NBP___Mongo.Model;

using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace NBP___Mongo.Services
{
    public class TestDriveService
    {
        private readonly IMongoCollection<Car> carCollection;
        private readonly IMongoCollection<User> userCollection;
        private readonly IMongoCollection<Dealer> dealerCollection;
        private readonly IMongoCollection<TestDrive> testCollection;

        public TestDriveService(IDbClient dbClient)
        {
            this.carCollection = dbClient.GetCarCollection();
            this.userCollection = dbClient.GetUserCollection();
            this.dealerCollection = dbClient.GetDealerCollection();
            this.testCollection = dbClient.GetTestDriveCollection();
        }

        public async Task<int> MakeTestDrive(DateTime TestDate, string CarID, string DealerID, string UserID)
        {
            Car c = await carCollection.Find(p => p.Id == CarID).FirstOrDefaultAsync();
            Dealer d = await dealerCollection.Find(p => p.ID == DealerID).FirstOrDefaultAsync();
            bool found = false;
            if (d != null && c != null)
            {
                if (c.RentOrSale == true) return 0;
                DateTime cmp1 = TestDate.Date;
                TestDrive testt = await testCollection.Find(p => p.Car.Id == CarID && p.TestDate.CompareTo(cmp1) == 0).FirstOrDefaultAsync();

                if (testt != null) return -1;

                foreach (MongoDBRef carRef in d.Cars.ToList())
                {
                    //checking if the dealer really is dealer of that car
                    if (carRef.Id == CarID)
                    {
                        found = true;
                        break;
                    }
                }

                User u = await userCollection.Find(p => p.ID == UserID).FirstOrDefaultAsync();
                if (found && u != null)
                {
                    TestDrive test = new TestDrive
                    {
                        TestDate = TestDate,
                        Car = c,
                        Dealer = new MongoDBRef("dealers", d.ID),
                        User = new MongoDBRef("users", u.ID),

                    };

                    await testCollection.InsertOneAsync(test);

                    u.RentCars.Add(new MongoDBRef("testDrive", test.ID));
                    var update = Builders<User>.Update.Set("TestDrives", u.TestDrives);
                    await userCollection.UpdateManyAsync(p => p.ID == UserID, update);
                    return 1;
                }

            }


            return -2;
        }

        public async Task<bool> DicardTestDrive(string TestDriveID)
        {
            TestDrive t = await testCollection.Find(p => p.ID == TestDriveID).FirstOrDefaultAsync();
            await testCollection.DeleteOneAsync(p => p.ID == TestDriveID);

            List<User> users = await userCollection.Find(p => p.TestDrives.Contains(new MongoDBRef("testDrive", TestDriveID))).ToListAsync();

            foreach (var user in users)
            {
                user.TestDrives.Remove(new MongoDBRef("testDrive", TestDriveID));
                var update = Builders<User>.Update.Set("TestDrives", user.TestDrives);
                await userCollection.UpdateManyAsync(p => p.ID == user.ID, update);
            }

            return true;
        }
    }
}
