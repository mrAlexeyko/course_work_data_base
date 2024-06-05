using Microsoft.AspNetCore.Mvc;
using CurseWork.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CurseWork.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CheckDatabaseConnection()
        {
            try
            {
                var aircrafts = await _context.Aircrafts.ToListAsync();
                return Ok(aircrafts);
            }
            catch (Exception ex)
            {
                return BadRequest($"Помилка при підключенні до бази даних: {ex.Message}");
            }
        }
    }
}
