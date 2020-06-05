using BetterHere.Web.Data;
using BetterHere.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BetterHere.Web.Controllers
{
    public class TypeFoodsController : Controller
    {
        private readonly DataContext _context;

        public TypeFoodsController(DataContext context)
        {
            _context = context;
        }

        // GET: TypeFoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeFoods.ToListAsync());
        }

        // GET: TypeFoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TypeFoodEntity typeFoodEntity = await _context.TypeFoods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeFoodEntity == null)
            {
                return NotFound();
            }

            return View(typeFoodEntity);
        }

        // GET: TypeFoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeFoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FoodType")] TypeFoodEntity typeFoodEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeFoodEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeFoodEntity);
        }

        // GET: TypeFoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TypeFoodEntity typeFoodEntity = await _context.TypeFoods.FindAsync(id);
            if (typeFoodEntity == null)
            {
                return NotFound();
            }
            return View(typeFoodEntity);
        }

        // POST: TypeFoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoodType")] TypeFoodEntity typeFoodEntity)
        {
            if (id != typeFoodEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeFoodEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeFoodEntityExists(typeFoodEntity.Id))
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
            return View(typeFoodEntity);
        }

        // GET: TypeFoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TypeFoodEntity typeFoodEntity = await _context.TypeFoods
                .Include(t => t.Foods)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeFoodEntity == null)
            {
                return NotFound();
            }

            return View(typeFoodEntity);
        }

        // POST: TypeFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TypeFoodEntity typeFoodEntity = await _context.TypeFoods
                .Include(t => t.Foods)
                .FirstOrDefaultAsync(m => m.Id == id);


            _context.TypeFoods.Remove(typeFoodEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeFoodEntityExists(int id)
        {
            return _context.TypeFoods.Any(e => e.Id == id);
        }
    }
}
