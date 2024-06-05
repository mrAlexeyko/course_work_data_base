using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using CurseWork.Data;
using CurseWork.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CurseWork.Controllers
{
    public class PassengersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PassengersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Passengers
        public async Task<IActionResult> Index(string searchFirstName, string searchLastName, string searchPassportNumber, string searchInSearch, int? page)
        {
            int pageSize = 20; // Кількість рядків на сторінку
            int pageNumber = (page ?? 1);

            IQueryable<Passenger> passengers = _context.Passengers;

            if (!string.IsNullOrEmpty(searchFirstName))
            {
                passengers = passengers.Where(p => p.FirstName.Contains(searchFirstName));
            }

            if (!string.IsNullOrEmpty(searchLastName))
            {
                passengers = passengers.Where(p => p.LastName.Contains(searchLastName));
            }

            if (!string.IsNullOrEmpty(searchPassportNumber))
            {
                passengers = passengers.Where(p => p.PassportNumber.Contains(searchPassportNumber));
            }

            if (!string.IsNullOrEmpty(searchInSearch))
            {
                bool inSearch = bool.Parse(searchInSearch);
                passengers = passengers.Where(p => p.InSearch == inSearch);
            }

            var pagedList = await passengers.OrderBy(p => p.PassengerId).ToPagedListAsync(pageNumber, pageSize);
            return View(pagedList);
        }

        // GET: Passengers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passengers
                .FirstOrDefaultAsync(m => m.PassengerId == id);
            if (passenger == null)
            {
                return NotFound();
            }

            return View(passenger);
        }

        // GET: Passengers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Passengers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PassengerId,FirstName,LastName,PassportNumber,BirthDate,InSearch")] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passenger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(passenger);
        }

        // GET: Passengers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }
            return View(passenger);
        }

        // POST: Passengers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PassengerId,FirstName,LastName,PassportNumber,BirthDate,InSearch")] Passenger passenger)
        {
            if (id != passenger.PassengerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passenger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassengerExists(passenger.PassengerId))
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
            return View(passenger);
        }

        // GET: Passengers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passengers
                .FirstOrDefaultAsync(m => m.PassengerId == id);
            if (passenger == null)
            {
                return NotFound();
            }

            return View(passenger);
        }

        // POST: Passengers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger != null)
            {
                _context.Passengers.Remove(passenger);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PassengerExists(int id)
        {
            return _context.Passengers.Any(e => e.PassengerId == id);
        }
    }
}
