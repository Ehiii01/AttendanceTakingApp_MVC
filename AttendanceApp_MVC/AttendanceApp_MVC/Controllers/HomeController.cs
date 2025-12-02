using System.Diagnostics;
using AttendanceBook.DataAccess.Data;
using AttendanceBook.Models;
using AttendanceBookWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceBookWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AttendanceBookAppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AttendanceBookAppDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index(bool? getAttedanceWithIssues = false)
        {

            var attendanceStatisticResponse = new AttendanceStatisticsResponse
            {
                TotalDailyAttendance = _context.Attendances.Where(a => a.CheckIn.Date == DateTime.Now.Date).Count(),
                TotalCurrentlyCheckedInUsers = _context.Attendances.Where(a => a.IsCheckOut == false && a.CheckIn.Date == DateTime.Now.Date).Count(),
                TotalCurrentlyCheckedOutUsers = _context.Attendances.Where(a => a.IsCheckOut == true && a.CheckOut.Value.Date == DateTime.Now.Date).Count(),
                Top5RecentlyCheckedInUsers = _context.Attendances.Where(a => a.IsCheckOut == false && a.CheckIn.Date == DateTime.Now.Date).OrderByDescending(a => a.CheckIn).Take(5).ToList()

            };

            var attendanceStatistics = new AttendanceStatistics
            {
                TotalAttendanceForCurrentYear = _context.Attendances.Where(a => a.CheckOut.Value.Date.Year == DateTime.Now.Year).Count(),
                TotalAttendance = _context.Attendances.Count(),

                TotalAttendanceForCurrentMonth = _context.Attendances.Where(a =>  a.CheckOut.Value.Date.Month == DateTime.Now.Date.Month).Count(),
                //MonthWithTheHighestAttendance = _context.Attendances.Where(a => a.IsCheckOut == false && a.IsCheckOut == true).GroupBy(a => new { a.CheckIn.Year, a.CheckIn.Month }).Select(g => new
                //{
                //    Year = g.Key.Year,
                //    Month = g.Key.Month,
                //    Count = g.Count()
                //})
                //.OrderDescending(a => a.Count)
                //.FirstOrDefaultAsync()

                AttendanceWithIssues = _context.Attendances.Where(a => a.IsCheckOut == false && a.CheckIn < DateTime.Now.AddDays(-1)).Count(),
                

            };

            attendanceStatisticResponse.Statistics = attendanceStatistics;

            if (getAttedanceWithIssues.HasValue && getAttedanceWithIssues.Value== true)
            {
                attendanceStatisticResponse.AttendanceWithIssues = _context.Attendances.Where(a => a.IsCheckOut == false && a.CheckIn < DateTime.Now.AddDays(-1)).ToList();
            }

            return View(attendanceStatisticResponse);







        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
