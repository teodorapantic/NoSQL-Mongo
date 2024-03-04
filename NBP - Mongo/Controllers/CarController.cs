using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NBP___Mongo.Model;
using NBP___Mongo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NBP___Mongo.Services.Files;
namespace NBP___Mongo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly CarService carService;

        public CarController(CarService carService)
        {
            this.carService = carService;
        }

        [HttpPost]
        [Route("AddCar")]
        public async Task<IActionResult> AddCar(String description, String year, String interiorColor, String exteriorColor, String nameMark, String nameModel, String engineId, double price, bool available, bool rentOrSale, [FromForm] FileUpload file)
        {
            try
            {
                var rez = await carService.AddNewCarAsync(description, year, interiorColor, exteriorColor, nameMark, nameModel, engineId, price, available, rentOrSale, file);
                if (rez)
                {
                    return Ok("Uspesno dodat automobil");
                }
                return BadRequest("Greska");
               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteCar")]
        public async Task<IActionResult> DeleteCar(String id)
        {
            try
            {
                bool rez = await carService.DeleteCar(id);
                if (rez)
                {
                    return Ok("Uspesno obrisan automobil");
                }
                return BadRequest("Automobil nije obrisan");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Mark
        [HttpPost]
        [Route("AddMark")]
        public async Task<IActionResult> AddMark(String name, String origin)
        {
            try
            {
                await carService.AddNewMark(name, origin);
                return Ok("Uspesno dodata marka");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("GetMarka")]

        public async Task<IActionResult> GetMarks()
        {
            try
            {
                List<Mark> marks = await carService.GetAllMarks();

                return Ok(marks);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }


        /// CarModel

        [HttpPost]
        [Route("AddModel")]
        public async Task<IActionResult> AddModel(String name)
        {
            try
            {
                await carService.AddNewModel(name);
                return Ok("Uspesno dodat model");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("GetModelsFromMark/{markId}")]

        public async Task<IActionResult> GetModelsFromMark(String markId)
        {
            try
            {
                List<CarModel> models = await carService.GetModelsFromMark(markId);

                return Ok(models);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("AddModelToMark/{nameMark}/{nameModel}")]
        public async Task<IActionResult> AddModelToMark(String nameMark, String nameModel)
        {
            try
            {
                var rez = await carService.AddModelToMark(nameMark, nameModel);
                if (rez)
                {
                    return Ok("Uspesno dodat model u listu");
                }
                return BadRequest("Greska");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("AddEngine/{fuelType}/{power}/{displacement}")]
        public async Task<IActionResult> AddEngine(String fuelType, int power, String displacement)
        {
            try
            {
                await carService.AddNewEngine(fuelType, power, displacement);
                return Ok("Uspesno dodat motor");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

       

        [HttpGet]
        [Route("GetAllCars")]

        public async Task<IActionResult> GerAllCars()
        {
            try
            {
                var list = await carService.GetCars();

                return Ok(list);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }


        [HttpGet]
        [Route("GetCarsWithFilters/{markName}/{modelName}/{minPrice}/{maxPrice}/{fuelType}/{rentOrSale}")]

        public async Task<IActionResult> GerCarsWithFilters(String markName, String modelName,double minPrice, double maxPrice, String fuelType, bool rentOrSale)
        {
            try
            {
                List<Car> list = await carService.GetCarsWithFilters(markName, modelName, maxPrice,minPrice, fuelType, rentOrSale);

                return Ok(list);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }


        // Reviews

        [HttpPost]
        [Route("AddReview/{userId}/{CarID}/{text}")]

        public async Task<IActionResult> AddReview(String userId, String CarID, String text)
        {
            try
            {
                await carService.AddNewReview(text, userId, CarID);
                return Ok("Uspesno dodat review");
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteReview/{reviewId}")]

        public async Task<IActionResult> DeleteReview(String reviewId)
        {
            try
            {
                bool rez = await carService.DeleteReview(reviewId);
                if (rez)
                {
                    return Ok("Uspesno obrisano");
                }
                return BadRequest("Greska");
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("GetReviewsForCar/{CarID}")]

        public async Task<IActionResult> GetReviewsForCar(String CarID)
        {
            try
            {
                List<Review> list = await carService.GetReviewsForCar(CarID);
                return Ok(list);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Route("GetMoreDetails/{CarID}")]

        public async Task<IActionResult> GetMoreDetails(string CarID)
        {
            return new JsonResult(await carService.GetMoreDetails(CarID));
        }
    }
}
