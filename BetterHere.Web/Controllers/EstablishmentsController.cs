using BetterHere.Web.Data;
using BetterHere.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BetterHere.Web.Controllers
{
    public class EstablishmentsController : Controller
    {
        private readonly DataContext _context;

        public EstablishmentsController(DataContext context)
        {
            _context = context;
        }

        // GET: EstablishmentEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Establishments.ToListAsync());
        }

        // GET: EstablishmentEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EstablishmentEntity establishmentEntity = await _context.Establishments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (establishmentEntity == null)
            {
                return NotFound();
            }

            return View(establishmentEntity);
        }

        // GET: EstablishmentEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstablishmentEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LogoEstablishmentPath")] EstablishmentEntity establishmentEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(establishmentEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(establishmentEntity);
        }

        // GET: EstablishmentEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EstablishmentEntity establishmentEntity = await _context.Establishments.FindAsync(id);
            if (establishmentEntity == null)
            {
                return NotFound();
            }
            return View(establishmentEntity);
        }

        // POST: EstablishmentEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LogoEstablishmentPath")] EstablishmentEntity establishmentEntity)
        {
            if (id != establishmentEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(establishmentEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstablishmentEntityExists(establishmentEntity.Id))
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
            return View(establishmentEntity);
        }

        // GET: EstablishmentEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EstablishmentEntity establishmentEntity = await _context.Establishments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (establishmentEntity == null)
            {
                return NotFound();
            }

            return View(establishmentEntity);
        }

        // POST: EstablishmentEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            EstablishmentEntity establishmentEntity = await _context.Establishments.FindAsync(id);
            _context.Establishments.Remove(establishmentEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstablishmentEntityExists(int id)
        {
            return _context.Establishments.Any(e => e.Id == id);
        }
    }
}
