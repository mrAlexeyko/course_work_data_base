using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using CurseWork.Data;
using CurseWork.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CurseWork.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Registrations
        public async Task<IActionResult> Index(int? searchTicketId, string searchStatus, DateTime? searchTime, int? page)
        {
            int pageSize = 20; // Кількість рядків на сторінку
            int pageNumber = (page ?? 1);

            IQueryable<Registration> registrations = _context.Registrations;

            if (searchTicketId.HasValue)
            {
                registrations = registrations.Where(r => r.TicketId == searchTicketId);
            }

            if (!string.IsNullOrEmpty(searchStatus))
            {
                registrations = registrations.Where(r => r.Status.Contains(searchStatus));
            }

            if (searchTime.HasValue)
            {
                registrations = registrations.Where(r => r.Time.Date == searchTime.Value.Date);
            }

            var pagedList = await registrations.OrderBy(r => r.RegistrationId).ToPagedListAsync(pageNumber, pageSize);
            return View(pagedList);
        }

        // GET: Registrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .FirstOrDefaultAsync(m => m.RegistrationId == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registrations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Registrations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationId,TicketId,Status,Time")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registration);
        }

        // GET: Registrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            return View(registration);
        }

        // POST: Registrations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistrationId,TicketId,Status,Time")] Registration registration)
        {
            if (id != registration.RegistrationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.RegistrationId))
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
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .FirstOrDefaultAsync(m => m.RegistrationId == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.RegistrationId == id);
        }
    }
}
