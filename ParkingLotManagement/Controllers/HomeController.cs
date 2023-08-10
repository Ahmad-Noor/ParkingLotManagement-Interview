using Microsoft.AspNetCore.Mvc;
using ParkingLotManagement.Infrastructure;
using ParkingLotManagement.Models;
using System.Diagnostics;
using System.Net;

namespace ParkingLotManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IParkingLotRepository _parkingLotRepository;
        private readonly IParkingLotSpaceRepository _parkingLotSpaceRepository;
        private readonly IParkingLotTransactionRepository _parkingLotTransactionRepository;
        private readonly int parkingLotId;
        private readonly decimal hourlyFee;
        public HomeController(ILogger<HomeController> logger,
            IParkingLotRepository parkingLotRepository,
            IParkingLotSpaceRepository parkingLotSpaceRepository,
            IParkingLotTransactionRepository parkingLotTransactionRepository)
        {
            _logger = logger;

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            hourlyFee = configuration.GetValue<decimal>("HourlyFee");

            _parkingLotRepository = parkingLotRepository;
            _parkingLotSpaceRepository = parkingLotSpaceRepository;
            _parkingLotTransactionRepository = parkingLotTransactionRepository;


            // get main branch id
            parkingLotId = _parkingLotRepository.GetAllParkingLots().FirstOrDefault().Id;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CheckinParkingLot(string tagnumber)
        {
            if (string.IsNullOrEmpty(tagnumber))
            {
                Response.Clear();
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                return Json(new { success = false, message = "tag number is null!", status = 500 });
            }

            var parkingLotTransaction = _parkingLotTransactionRepository.GetParkingLotTransactionByTagNumber(tagnumber);
            if (parkingLotTransaction != null)
            {
                Response.Clear();
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                return Json(new { success = false, message = tagnumber + " is Already parked.", status = 500 });
            }


            var parkingLotSpace = _parkingLotSpaceRepository.GetParkingLotSpacesAvailable(parkingLotId);
            if (parkingLotSpace == null)
            {
                Response.Clear();
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                return Json(new { success = false, message = "No Parking Lot Spaces Available", status = 500 });
            }
            parkingLotSpace.Occupied = true;
            _parkingLotSpaceRepository.UpdateParkingLotSpace(parkingLotSpace);


            _parkingLotTransactionRepository.InsertParkingLotTransaction(new() {
                 ParkingLotSpaceId = parkingLotSpace.Id,
                 CheckinTime = DateTime.Now,
                TagNumber = tagnumber
            });


            return Json(new { success = true, message = "Data Saved", status = 200 });

        }

        [HttpPost]
        public ActionResult CheckOutParkingLot(string tagnumber)
        {
            if (string.IsNullOrEmpty(tagnumber))
            { 
                Response.Clear();
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                return Json(new { success = false, message = "tag number is null!", status = 500 });
            }
            var parkingLotTransaction = _parkingLotTransactionRepository.GetParkingLotTransactionByTagNumber(tagnumber);
            if (parkingLotTransaction == null)
            {
                Response.Clear();
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                return Json(new { success = false, message = "No Parking Lot Spaces Available for Tag number"+ tagnumber, status = 500 });
            }

            // parking Lot Space
            var parkingLotSpace = _parkingLotSpaceRepository.GetParkingLotSpaceById(parkingLotTransaction.ParkingLotSpaceId);
            parkingLotSpace.Occupied = false;
            _parkingLotSpaceRepository.UpdateParkingLotSpace(parkingLotSpace);

            // update parking Lot Transaction
            parkingLotTransaction.CheckoutTime= DateTime.Now;


            TimeSpan timeDifference = (TimeSpan)(parkingLotTransaction.CheckoutTime - parkingLotTransaction.CheckinTime);
            int totalMinutes = (int)timeDifference.TotalMinutes;


            parkingLotTransaction.AmountPaid = getAmountPaid(totalMinutes);

            _parkingLotTransactionRepository.UpdateParkingLotTransaction(parkingLotTransaction);

            return Json(new { success = true, message = "Saved", status = 200 }); 
        }

        decimal getAmountPaid(int time)
        { 
            if (time >= 60)
            {
                // Calculate the total fee for the first hour
                decimal totalFee = hourlyFee;

                // Calculate the total fee for additional hours
                int additionalTime = time - 60;
                int additionalHours = (int)Math.Ceiling((decimal)additionalTime / 60);
                totalFee += additionalHours * hourlyFee;

                return totalFee;
            }
            else
            {
                return hourlyFee;
            }
        }

        [HttpGet]
         public IActionResult GetParkingLotTransactionOccupied()
        {
            var occupied = _parkingLotTransactionRepository.GetParkingLotTransactionOccupied()
                .Select((s) => new {
                    tagNumber = s.TagNumber,
                    startTime= s.CheckinTime.ToShortTimeString(),
                    elapsedTime = (int)((DateTime.Now - s.CheckinTime).TotalMinutes / 60)
            }); 
            return Json(occupied);  
        }


        [HttpGet]
        public IActionResult GetHeaderData()
        {
            var parkingLotSpace = _parkingLotSpaceRepository.GetAllParkingLotSpaces();

            var totalSpots = parkingLotSpace.Count();
            var availableSpots = parkingLotSpace.Where(w => !w.Occupied).Count();
            var spotsTaken = parkingLotSpace.Where(w => w.Occupied).Count();
 
            return Json(new{ totalSpots,availableSpots, spotsTaken, hourlyFee });
        }
         
        [HttpGet]
        public IActionResult GetStats()
        {
            var parkingLotSpace = _parkingLotSpaceRepository.GetAllParkingLotSpaces();
            var availableSpots = parkingLotSpace.Where(w => !w.Occupied).Count();

            var avgCars = _parkingLotTransactionRepository.GetAverageCarsForPast30Days();
            var avgRevenue = _parkingLotTransactionRepository.GetAverageRevenueForPast30Days();
            var todayRevenue = _parkingLotTransactionRepository.GetTodayRevenue();


            return Json(new {availableSpots,avgCars, avgRevenue, todayRevenue });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}