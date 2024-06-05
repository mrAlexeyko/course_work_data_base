using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using CurseWork.Data;
using CurseWork.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CurseWork.Controllers
{
    public class RoutesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoutesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Routes
        public async Task<IActionResult> Index(string searchDepartureAirport, string searchArrivalAirport, string searchTerminalType, int? page)
        {
            int pageSize = 20; // Кількість рядків на сторінку
            int pageNumber = (page ?? 1);

            IQueryable<CurseWork.Models.Route> routes = _context.Routes;

            if (!string.IsNullOrEmpty(searchDepartureAirport))
            {
                routes = routes.Where(r => r.DepartureAirport.Contains(searchDepartureAirport));
            }

            if (!string.IsNullOrEmpty(searchArrivalAirport))
            {
                routes = routes.Where(r => r.ArrivalAirport.Contains(searchArrivalAirport));
            }

            if (!string.IsNullOrEmpty(searchTerminalType))
            {
                routes = routes.Where(r => r.TerminalType == searchTerminalType[0]); // Пряме порівняння для char
            }

            var pagedList = await routes.OrderBy(r => r.RouteId).ToPagedListAsync(pageNumber, pageSize);
            return View(pagedList);
        }

        // GET: Routes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .FirstOrDefaultAsync(m => m.RouteId == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // GET: Routes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Routes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RouteId,DepartureTime,ArrivalTime,DepartureAirport,ArrivalAirport,TerminalType")] CurseWork.Models.Route route)
        {
            if (ModelState.IsValid)
            {
                _context.Add(route);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(route);
        }

        // GET: Routes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            return View(route);
        }

        // POST: Routes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RouteId,DepartureTime,ArrivalTime,DepartureAirport,ArrivalAirport,TerminalType")] CurseWork.Models.Route route)
        {
            if (id != route.RouteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(route);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteExists(route.RouteId))
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
            return View(route);
        }

        // GET: Routes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .FirstOrDefaultAsync(m => m.RouteId == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route != null)
            {
                _context.Routes.Remove(route);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RouteExists(int id)
        {
            return _context.Routes.Any(e => e.RouteId == id);
        }
    }
}
