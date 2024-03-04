using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MongoDB.Driver;
using NBP___Mongo.Services.Files;
using NBP___Mongo.DBClient;
using NBP___Mongo.Model;
using MongoDB.Driver.Linq;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Xml.Linq;
using NBP___Mongo.Services.HelperClasses;

namespace NBP___Mongo.Services
{
    public class CarService
    {
        private readonly IMongoCollection<Car> carCollection;
        private readonly IMongoCollection<Mark> markCollection;
        private readonly IMongoCollection<CarModel> modelCollection;
        private readonly IMongoCollection<EngineType> engineCollection;
        private readonly IMongoCollection<Review> reviewCollection;
        private readonly IMongoCollection<User> userCollection;
        private readonly IDbClient dbClient;
        private readonly IMongoCollection<RentCar> rentCollection;
        private readonly IMongoCollection<TestDrive> testCollection;
        private readonly IMongoCollection<Dealer> dealerCollection;
        private IMongoDatabase database;
        private IWebHostEnvironment _webHost;







        public CarService(IDbClient dbClient)
        {
            this.carCollection = dbClient.GetCarCollection();
            this.markCollection = dbClient.GetMarkCollection();
            this.modelCollection = dbClient.GetCarModelCollection();
            this.engineCollection = dbClient.GetEngineTypeCollection();
            this.reviewCollection = dbClient.GetReviewCollection();
            this.userCollection = dbClient.GetUserCollection();
            this.database = dbClient.GetMongoDB();

        }

        public async Task<bool> AddNewCarAsync(String description, String year, String interiorColor, String exteriorColor, String nameMark, String nameModel, String engineId, double price, bool av, bool rentOrSale, FileUpload file)
        {
            Mark mark = await markCollection.Find(c => c.Name == nameMark).FirstOrDefaultAsync();
            CarModel model = await modelCollection.Find(m => m.Name == nameModel).FirstOrDefaultAsync();
            EngineType engine = await engineCollection.Find(e => e.Id == engineId).FirstOrDefaultAsync();

            if (mark == null || model == null || engine == null)
            {
                return false;

            }



         
            Car car = new Car
            {
                Mark = mark,
                CarModel = model,
                EngineType = engine,
                ExteriorColor = exteriorColor,
                InteriorColor = interiorColor,
                Description = description,
                Year = year,
                Price = price,
                Available = av,
                RentOrSale = rentOrSale
                
               
                
            };

            await carCollection.InsertOneAsync(car);

            if (file.file.Length > 0)
            {
                string path = _webHost.WebRootPath + "\\CarsPictures\\";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                string newname = car.Id.ToString() + System.IO.Path.GetExtension(file.file.FileName);
                using (FileStream fileStream = System.IO.File.Create(path + newname))
                {
                    file.file.CopyTo(fileStream);
                    fileStream.Flush();
                }
                car.Picture = newname;
            }

            else car.Picture = "def.jpg";
            var update = Builders<Car>.Update.Set("Picture", car.Picture);
            await carCollection.UpdateOneAsync(p => p.Id == car.Id, update);
            return true;
        
        }

        public async Task<bool> DeleteCar(String id)
        {
            Car car = await carCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (car == null)
            {

                return false;
            }
            if (car.RentOrSale == false)
            {
                List<TestDrive> tests = await testCollection.Find(p => p.Car.Id == id).ToListAsync();
                if (tests.Count != 0)
                {
                    TestDriveService t = new TestDriveService(dbClient);
                    foreach (TestDrive tt in tests)
                    {
                        await t.DicardTestDrive(tt.ID);
                    }
                }
            }
            else if(car.RentOrSale == true)
            {
                List<RentCar> rentals = await rentCollection.Find(p => p.Car.Id == id).ToListAsync();
                if (rentals.Count != 0)
                {
                    RentCarService r = new RentCarService(dbClient);
                    foreach (RentCar rr in rentals)
                    {
                        await r.DicardRentCar(rr.ID);
                    }
                }
            }
            List<Dealer> list = await dealerCollection.Find(p=>true).ToListAsync();
            Dealer d = await dealerCollection.Find(p => p.ID == car.Dealer.Id.ToString()).FirstOrDefaultAsync();
            if (d != null)
            {
                d.Cars.Remove(new MongoDBRef("cars", car.Id));
                var update = Builders<Dealer>.Update.Set("Cars", d.Cars);
                await dealerCollection.UpdateManyAsync(p => p.ID == car.Dealer.Id, update);
            }
            if (car.Picture != "def.jpg")
            {
                string path = _webHost.WebRootPath + "\\CarsPictures\\";
                if (!Directory.Exists(path))
                {
                    Console.WriteLine("Oxi");
                }
                path += car.Picture;
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            }
            await carCollection.DeleteOneAsync(c => c.Id == id);

            return true;
        }



        // Mark service
        public async Task AddNewMark(String name, String origin)
        {
            Mark mark = new Mark
            {
                Name = name,
                Origin = origin
            

            };
            
            await markCollection.InsertOneAsync(mark);

        }

        public async Task<List<Mark>> GetAllMarks()
        {
            return await markCollection.Find(f => true).ToListAsync();

        }

        public async Task<bool> AddModelToMark(String nameMark, String nameModel)
        {

          
            Mark mark = await markCollection.Find(c => c.Name == nameMark).FirstOrDefaultAsync();
            CarModel model = await modelCollection.Find(m => m.Name == nameModel).FirstOrDefaultAsync();

            if (mark == null || model == null)
            {
                return false;

            }

      
            model.Mark = new MongoDBRef("mark", mark.Id);

           

           

            var update2 = Builders<CarModel>.Update.Set("Mark", model.Mark);
            await modelCollection.UpdateOneAsync(m => m.Name == model.Name, update2);

            MongoDBRef r = new MongoDBRef("models", model.Id);
            if (mark.Models == null)
            {
                mark.Models = new List<MongoDBRef>();
              
                mark.Models.Add(r);
            }
            else
            {
                mark.Models.Add(r);
            }


            var update = Builders<Mark>.Update.Set("Models", mark.Models);
            await markCollection.UpdateOneAsync(p => p.Name == mark.Name, update);
            return true;
        }



        // Model service

        public async Task AddNewModel(String name)
        {
            CarModel model = new CarModel
            {
                Name = name,
            };

            await modelCollection.InsertOneAsync(model);

        }

        public async Task<List<CarModel>> GetModelsFromMark(String markId)
        {
            Mark mark = await markCollection.Find(m => m.Id == markId).FirstOrDefaultAsync();

           
            return await modelCollection.Find(f => f.Mark.Id == mark.Id).ToListAsync();

        }


        // EngineType service

        public async Task AddNewEngine(String fuelType, int power, String displacement)
        {
            EngineType engine = new EngineType
            {
                FuelType = fuelType,
                Displacement = displacement,
                Power = power
        

            };

            await engineCollection.InsertOneAsync(engine);

        }

       

        public async Task<List<Car>> GetCars()
        {
            return  await carCollection.Find(c => true).ToListAsync();

            
        }

        public async Task<List<Car>> GetCarsWithFilters(String markName, String modelName, double maxPrice,double minPrice, String fuelType,bool rentOrSale)
        {


            var builder = Builders<Car>.Filter;
            var filter = builder.Eq(x => x.RentOrSale,rentOrSale);

            if (!string.IsNullOrWhiteSpace(markName) && markName != "")
            {
                var markNameFilter = builder.Eq(x => x.Mark.Name, markName);
                filter &= markNameFilter;
            }

            if (!string.IsNullOrWhiteSpace(modelName) && modelName != "")
            {
                var modelNameFilter = builder.Eq(x => x.CarModel.Name, modelName);
                filter &= modelNameFilter;
            }

            if (!string.IsNullOrWhiteSpace(fuelType) && fuelType != "")
            {
                var fuelTypeFilter = builder.Eq(x => x.EngineType.FuelType, fuelType);
                filter &= fuelTypeFilter;
            }

            if ( maxPrice != -1)
            {
                var priceFilter =  builder.Lt(x => x.Price, maxPrice);
                filter &= priceFilter;
            }

            if (minPrice != -1)
            {

                var priceFilter2 = builder.Gt(x => x.Price, minPrice);
                filter &= priceFilter2;
            }

            var result = await carCollection.Find(filter).ToListAsync();
            return result;

           


        }


        //Review 

        public async Task<bool> AddNewReview(String text, String userId, String CarID)
        {
            Car car = await carCollection.Find(c => c.Id == CarID).FirstOrDefaultAsync();
            User user = await userCollection.Find(u => u.ID == userId).FirstOrDefaultAsync();

            if (car == null || user == null )
            {
                return false;

            }

            Review review = new Review
            {
                Text = text,
                Car = new MongoDBRef("car", CarID),
                User = user
            };


            await reviewCollection.InsertOneAsync(review);

            car.Reviews.Add(new MongoDBRef("review", review.Id));
            user.MyReviews.Add(new MongoDBRef ("myreview", review.Id));

            var update = Builders<Car>.Update.Set("Reviews", car.Reviews);
            var update2 = Builders<User>.Update.Set("MyReviews", user.MyReviews);

            await carCollection.UpdateOneAsync(p => p.Id == car.Id, update);
            await userCollection.UpdateOneAsync(p => p.ID == user.ID, update2);




            return true;

        }

        public async Task<bool> DeleteReview(String id)
        {
            Review review = await reviewCollection.Find(c => c.Id == id).FirstOrDefaultAsync();

            if (review == null)
            {

                return false;
            }
            await reviewCollection.DeleteOneAsync(c => c.Id == id);
            return true;
        }

        public async Task<List<Review>> GetReviewsForCar(String carId)
        {
            return await reviewCollection.Find(r => r.Car.Id == carId).ToListAsync();
        }


        public async Task<CarSerialization> GetMoreDetails(string CarID)
        {
            var car = await carCollection.Find(p => p.Id == CarID).FirstOrDefaultAsync();

            CarSerialization carReturn = new CarSerialization
            {
                Id = car.Id,
                CarModel = car.CarModel,
                Mark = car.Mark,
                ExteriorColor = car.ExteriorColor,
                InteriorColor = car.InteriorColor,
                Drivetrain = car.Drivetrain,
                EngineType = car.EngineType,
                Reviews = car.Reviews,
                Description = car.Description,
                Year = car.Year,
                Price = car.Price,
                RentOrSale = car.RentOrSale,
                Available = car.Available,
                Dealer = car.Dealer.Id.ToString(),
                Picture = car.Picture,
            };

            return carReturn;
        }

        



    }
}
