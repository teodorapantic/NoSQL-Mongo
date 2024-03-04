using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using NBP___Mongo.DBClient;
using NBP___Mongo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace NBP___Mongo.Services
{
    public class RentCarService
    {
        private readonly IMongoCollection<Car> carCollection;
        private readonly IMongoCollection<User> userCollection;
        private readonly IMongoCollection<Dealer> dealerCollection;
        private readonly IMongoCollection<RentCar> rentcarCollection;


        public RentCarService(IDbClient dbClient)
        {
            this.carCollection = dbClient.GetCarCollection();
            this.userCollection = dbClient.GetUserCollection();
            this.dealerCollection = dbClient.GetDealerCollection();
            this.rentcarCollection = dbClient.GetRentCarCollection();
        }

        public async Task<int> MakeCarRental(DateTime OccupiedFrom, DateTime OccupiedUntill, string CarID, string DealerID, string UserID )
        {
            RentCar r = new RentCar();

            Car c = await carCollection.Find(p => p.Id == CarID).FirstOrDefaultAsync();
            Dealer d = await dealerCollection.Find(p => p.ID == DealerID).FirstOrDefaultAsync();
            bool found = false;
            if(d!=null && c!=null)
            {
                if(c.RentOrSale == false) return 0;
                List<RentCar> rentals = await rentcarCollection.Find(p => p.Car.Id == CarID && p.Allowed == true).ToListAsync();
                foreach(RentCar rental in rentals)
                {
                    //problem u mozgu koji ima MongoDb Server!!!
                    DateTime cmp1 = rental.OccupiedFrom.AddDays(1).Date;
                    DateTime cmp2 = rental.OccupiedUntill.AddDays(1).Date;
                    int startCompare = OccupiedFrom.Date.CompareTo(cmp1);
                    int endCompare = OccupiedUntill.Date.CompareTo(cmp2);

                    if((startCompare >= 0 && endCompare <= 0) || (startCompare <= 0 && endCompare >= 0))
                    {
                        return -1;
                    }
                    //check if is booked 
                }
                foreach(MongoDBRef carRef in d.Cars.ToList())
                {
                    //checking if the dealer really is dealer of that car
                    if(carRef.Id == CarID)
                    {
                        found = true;
                        break;
                    }
                }

                User u = await userCollection.Find(p => p.ID == UserID).FirstOrDefaultAsync();
                if (found && u!=null)
                {
                    RentCar rent = new RentCar
                    {
                        OccupiedFrom = OccupiedFrom,
                        OccupiedUntill = OccupiedUntill,
                        Car = c,
                        Dealer = new MongoDBRef("dealers", d.ID),
                        User = new MongoDBRef("users", u.ID),
                        Allowed = false
                    };

                    await rentcarCollection.InsertOneAsync(rent);

                    u.RentCars.Add(new MongoDBRef("rentCar", rent.ID));
                    var update = Builders<User>.Update.Set("RentCars", u.RentCars);
                    await userCollection.UpdateManyAsync(p => p.ID == UserID, update);
                    return 1;

                }

            }
            return -2;
        }

        public async Task<bool> AllowRental(string RentalID)
        {
            RentCar r = await rentcarCollection.Find(p => p.ID == RentalID).FirstOrDefaultAsync();
            if (r != null)
            {
                var update = Builders<RentCar>.Update.Set("Allowed", true);
                await rentcarCollection.UpdateOneAsync(p => p.ID == RentalID, update);
                return true;
            }
            return false;
        }

        public async Task<bool> FrobidRental(string RentalID)
        {
            RentCar r = await rentcarCollection.Find(p => p.ID == RentalID).FirstOrDefaultAsync();
            if (r != null)
            {
                var update = Builders<RentCar>.Update.Set("Allowed", false);
                await rentcarCollection.UpdateOneAsync(p => p.ID == RentalID, update);
                return true;
            }
            return false;
        }


        public async Task<bool> DicardRentCar(string RentCarID)
        {
            RentCar r = await rentcarCollection.Find(p => p.ID == RentCarID).FirstOrDefaultAsync();
            await rentcarCollection.DeleteOneAsync(p => p.ID == RentCarID);

            List<User> users = await userCollection.Find(p => p.TestDrives.Contains(new MongoDBRef("rentCar", RentCarID))).ToListAsync();

            foreach (var user in users)
            {
                user.RentCars.Remove(new MongoDBRef("rentCar", RentCarID));
                var update = Builders<User>.Update.Set("RentCars", user.RentCars);
                await userCollection.UpdateManyAsync(p => p.ID == user.ID, update);
            }

            return true;
        }

    }
}
