using Microsoft.AspNetCore.Mvc;
using NBP___Mongo.Model;
using NBP___Mongo.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NBP___Mongo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestDriveController : ControllerBase
    {
        private readonly TestDriveService _testDriveService;
        public TestDriveController(TestDriveService service)
        {
            this._testDriveService = service;
        }

        [HttpPost]
        [Route("MakeTestDrive/{TestDate}/{CarID}/{DealerID}/{UserID}")]
        public async Task<IActionResult> MakeTestDrive(DateTime TestDate, string CarID, string DealerID, string UserID)
        {
            int status = await _testDriveService.MakeTestDrive(TestDate, CarID, DealerID, UserID);
            if (status == 0) return BadRequest("Error : You can't make a TestDrive for this car -> This car is not on sale!");
            if (status == -1) return BadRequest("Error : You can't make a TestDrive for this car -> This car is already booked that day!");
            if (status == -2) return BadRequest("Error : We couldn't find Dealer or Car!");
            return Ok("Successfully created TestDrive for Car: "+CarID);
        }

        [HttpDelete]
        [Route("DicardTestDrive/{TestDriveID}")]
        public async Task<IActionResult> DicardTestDrive(string TestDriveID)
        {
            bool status = await _testDriveService.DicardTestDrive(TestDriveID);
            if (status) return Ok("Succesfully discard test drive!");
            return BadRequest("Error");
        }

    }
}
