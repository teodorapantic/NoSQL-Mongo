using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NBP___Mongo.Model;
using NBP___Mongo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBP___Mongo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;

        }


        [HttpPost]
        [Route("CreateUser/{name}/{surname}/{username}/{password}")]
        public async Task<IActionResult> CreateUser(string name, string surname, string username, string password)
        {
            try
            {
                var result = await userService.CreateUser(name, surname, username, password);
                if (result == "Uspešno kreiran korisnik.")
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet]
        [Route("LogIn/{username}/{password}")]
        public IActionResult LogIn(String username, String password)
        {
            try
            {
                Task<User> res = userService.LogInUser(username, password);
                User res1 = res.Result;
                if (res1 == null)
                {
                    return BadRequest("Korisnik ne postoji ili ste pogresli parametre za prijavu");
                }

                return Ok(res1);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet]
        [Route("GetRentCars/{userID}")]
        public async Task<IActionResult> GetRentCars(string userID)
        {
            try
            {
                List<RentCar> rentCars = await userService.GetRentCars(userID);
                return Ok(rentCars);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetTestDrives/{userID}")]
        public async Task<IActionResult> GetTestDrives(string userID)
        {
            try
            {
                List<TestDrive> testDrives = await userService.GetTestDrives(userID);
                return Ok(testDrives);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}
