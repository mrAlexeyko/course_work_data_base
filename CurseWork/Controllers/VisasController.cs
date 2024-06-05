using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using CurseWork.Data;
using CurseWork.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CurseWork.Controllers
{
    public class VisasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Visas
        public async Task<IActionResult> Index(string searchCountry, int? page)
        {
            int pageSize = 20; // Кількість рядків на сторінку
            int pageNumber = (page ?? 1);

            IQueryable<CurseWork.Models.Visa> visas = _context.Visas;

            if (!string.IsNullOrEmpty(searchCountry))
            {
                visas = visas.Where(v => v.Country.Contains(searchCountry));
            }

            var pagedList = await visas.OrderBy(v => v.VisaId).ToPagedListAsync(pageNumber, pageSize);
            return View(pagedList);
        }

        // GET: Visas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visa = await _context.Visas
                .FirstOrDefaultAsync(m => m.VisaId == id);
            if (visa == null)
            {
                return NotFound();
            }

            return View(visa);
        }

        // GET: Visas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Visas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisaId,PassengerId,Country,ExpiryDate")] CurseWork.Models.Visa visa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(visa);
        }

        // GET: Visas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visa = await _context.Visas.FindAsync(id);
            if (visa == null)
            {
                return NotFound();
            }
            return View(visa);
        }

        // POST: Visas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VisaId,PassengerId,Country,ExpiryDate")] CurseWork.Models.Visa visa)
        {
            if (id != visa.VisaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisaExists(visa.VisaId))
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
            return View(visa);
        }

        // GET: Visas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visa = await _context.Visas
                .FirstOrDefaultAsync(m => m.VisaId == id);
            if (visa == null)
            {
                return NotFound();
            }

            return View(visa);
        }

        // POST: Visas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visa = await _context.Visas.FindAsync(id);
            if (visa != null)
            {
                _context.Visas.Remove(visa);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool VisaExists(int id)
        {
            return _context.Visas.Any(e => e.VisaId == id);
        }
    }
}
