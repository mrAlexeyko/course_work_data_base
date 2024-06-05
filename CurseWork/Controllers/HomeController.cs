using CurseWork.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CurseWork.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Title"] = "About";
            ViewData["Message"] = "Your application description page.";
            ViewData["Author"] = "Олексій Прокопішин";
            ViewData["Group"] = "ІС-22";
            ViewData["Discipline"] = "Ваша Дисципліна";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
