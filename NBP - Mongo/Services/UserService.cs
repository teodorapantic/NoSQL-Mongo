using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using NBP___Mongo.DBClient;
using NBP___Mongo.Model;

namespace NBP___Mongo.Services
{
    public class UserService
    {

       
        private readonly IMongoCollection<RentCar> rentCarCollection;
        private readonly IMongoCollection<TestDrive> testDriveCollection;
        private readonly IMongoCollection<User> userCollection;

        public UserService(IDbClient dbClient)
        {
            this.userCollection = dbClient.GetUserCollection();
            this.rentCarCollection = dbClient.GetRentCarCollection();
            this.testDriveCollection = dbClient.GetTestDriveCollection();

        }

        public async Task<string> CreateUser(string name, string surname, string username, string password)
        {  
            var user = await userCollection.Find(p => p.Username == username).FirstOrDefaultAsync();
            if (user != null)
            {
                return "Korisnik sa tim korisničkim imenom već postoji.";
            }
            else
            {
                User user1 = new User
                {
                    Name = name,
                    Surname = surname,
                    Username = username,
                    Password = password,
                };
                userCollection.InsertOne(user1);
                return "Uspešno kreiran korisnik.";
            }
            
        }


        public async Task<User> LogInUser(string username, string password)
        {
            var user = await userCollection.Find(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();
            if (user != null)
            {
                return user;
            }
            return null;
        }

        //public async Task<List<RentCar>> GetRentCars(string userID)
        //{
        //    List<RentCar> rentCars = new List<RentCar>();
        //    var user = await userCollection.Find(p => p.ID == userID).FirstOrDefaultAsync();
        //    if (user != null)
        //    {
        //        var rentCarIds = user.RentCars.Select(x => x.Id).ToList();
        //        rentCars = await rentCarCollection.Find(p => rentCarIds.Contains(p.ID)).ToListAsync();
        //    }
        //    return rentCars;
        //}

        public async Task<List<RentCar>> GetRentCars(string userID)
        {
            List<RentCar> rentCars = new List<RentCar>();
            var user = await userCollection.Find(p => p.ID == userID).FirstOrDefaultAsync();
            if (user != null)
            {
                foreach (var rentCarRef in user.RentCars)
                {

                   

                    var ID = rentCarRef.Id.ToString();
                    var rentCar = await rentCarCollection.Find(p => p.ID == ID).FirstOrDefaultAsync();

                    if (rentCar != null)
                        rentCars.Add(rentCar);
                }
            }
            return rentCars;
        }


        public async Task<List<TestDrive>> GetTestDrives(string userID)
        {
            List<TestDrive> testDrives = new List<TestDrive>();
            var user = await userCollection.Find(p => p.ID == userID).FirstOrDefaultAsync();
            if (user != null)
            {
                foreach (var testDriveRef in user.TestDrives)
                {
                    var Id = testDriveRef.Id.ToString();
                    var testDrive = await testDriveCollection.Find(p => p.ID == Id).FirstOrDefaultAsync();
                    if (testDrive != null)
                        testDrives.Add(testDrive);
                }
            }
            return testDrives;
        }



    }
}
