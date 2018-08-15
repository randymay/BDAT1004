using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BDAT1004.Models;

namespace BDAT1004.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CrimeTrends()
        {
            ViewData["Message"] = "";

            return View();
        }

        public IActionResult OverallMonthlyTrends()
        {
            ViewData["Message"] = "";

            return View();
        }

        public IActionResult OverallWeeklyTrends()
        {
            ViewData["Message"] = "";

            return View();
        }
        public IActionResult Top5Crimes()
        {
            ViewData["Message"] = "";

            return View();
        }
        public IActionResult Top5CrimesHourlyTrends()
        {
            ViewData["Message"] = "";

            return View();
        }

        public IActionResult CrimesByYear()
        {
            ViewData["Message"] = "";

            return View();
        }

        public IActionResult HourlyTrends()
        {
            ViewData["Message"] = "";

            return View();
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
