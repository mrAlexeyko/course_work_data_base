using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using CurseWork.Data;
using CurseWork.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CurseWork.Controllers
{
    public class LuggagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LuggagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Luggages
        public async Task<IActionResult> Index(int? searchTicketId, int? page)
        {
            int pageSize = 20; // Кількість рядків на сторінку
            int pageNumber = (page ?? 1);

            IQueryable<Luggage> luggages = _context.Luggages;

            if (searchTicketId.HasValue)
            {
                luggages = luggages.Where(l => l.TicketId == searchTicketId);
            }

            var pagedList = await luggages.OrderBy(l => l.LuggageId).ToPagedListAsync(pageNumber, pageSize);
            return View(pagedList);
        }

        // GET: Luggages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var luggage = await _context.Luggages
                .FirstOrDefaultAsync(m => m.LuggageId == id);
            if (luggage == null)
            {
                return NotFound();
            }

            return View(luggage);
        }

        // GET: Luggages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Luggages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LuggageId,TicketId,Weight,Volume,ExtraFee")] Luggage luggage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(luggage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(luggage);
        }

        // GET: Luggages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var luggage = await _context.Luggages.FindAsync(id);
            if (luggage == null)
            {
                return NotFound();
            }
            return View(luggage);
        }

        // POST: Luggages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LuggageId,TicketId,Weight,Volume,ExtraFee")] Luggage luggage)
        {
            if (id != luggage.LuggageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(luggage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LuggageExists(luggage.LuggageId))
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
            return View(luggage);
        }

        // GET: Luggages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var luggage = await _context.Luggages
                .FirstOrDefaultAsync(m => m.LuggageId == id);
            if (luggage == null)
            {
                return NotFound();
            }

            return View(luggage);
        }

        // POST: Luggages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var luggage = await _context.Luggages.FindAsync(id);
            if (luggage != null)
            {
                _context.Luggages.Remove(luggage);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool LuggageExists(int id)
        {
            return _context.Luggages.Any(e => e.LuggageId == id);
        }
    }
}
