using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CurseWork.Models
{
    public class AboutModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "Інформація";
            ViewData["Author"] = "Прокопішин Олексій";
            ViewData["Group"] = "IC-22";
            ViewData["Discipline"] = "Курсова робота";
        }
    }
}
