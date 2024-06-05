using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using CurseWork.Data;
using CurseWork.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CurseWork.Controllers
{
    public class AircraftsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AircraftsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Aircrafts
        public async Task<IActionResult> Index(string searchModel, string searchAirline, int? page)
        {
            int pageSize = 20; // Кількість рядків на сторінку
            int pageNumber = (page ?? 1);

            IQueryable<Aircraft> aircrafts = _context.Aircrafts;

            if (!string.IsNullOrEmpty(searchModel))
            {
                aircrafts = aircrafts.Where(a => a.Model.Contains(searchModel));
            }

            if (!string.IsNullOrEmpty(searchAirline))
            {
                aircrafts = aircrafts.Where(a => a.Airline.Contains(searchAirline));
            }

            var pagedList = await aircrafts.OrderBy(a => a.AircraftId).ToPagedListAsync(pageNumber, pageSize);
            return View(pagedList);
        }

        // GET: Aircrafts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircrafts
                .FirstOrDefaultAsync(m => m.AircraftId == id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // GET: Aircrafts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aircrafts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AircraftId,PassengerCapacity,Model,Airline")] Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aircraft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aircraft);
        }

        // GET: Aircrafts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircrafts.FindAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }
            return View(aircraft);
        }

        // POST: Aircrafts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AircraftId,PassengerCapacity,Model,Airline")] Aircraft aircraft)
        {
            if (id != aircraft.AircraftId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aircraft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AircraftExists(aircraft.AircraftId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aircraft);
        }

        // GET: Aircrafts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircrafts
                .FirstOrDefaultAsync(m => m.AircraftId == id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // POST: Aircrafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aircraft = await _context.Aircrafts.FindAsync(id);
            if (aircraft != null)
            {
                _context.Aircrafts.Remove(aircraft);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        private bool AircraftExists(int id)
        {
            return _context.Aircrafts.Any(e => e.AircraftId == id);
        }
    }
}
