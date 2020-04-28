using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BetterHere.Web.Data;
using BetterHere.Web.Data.Entities;

namespace BetterHere.Web.Controllers
{
    public class TypeEstablishmentsController : Controller
    {
        private readonly DataContext _context;

        public TypeEstablishmentsController(DataContext context)
        {
            _context = context;
        }

        // GET: TypeEstablishments
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeEstablishments.ToListAsync());
        }

        // GET: TypeEstablishments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeEstablishmentEntity = await _context.TypeEstablishments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeEstablishmentEntity == null)
            {
                return NotFound();
            }

            return View(typeEstablishmentEntity);
        }

        // GET: TypeEstablishments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeEstablishments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameType")] TypeEstablishmentEntity typeEstablishmentEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeEstablishmentEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeEstablishmentEntity);
        }

        // GET: TypeEstablishments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeEstablishmentEntity = await _context.TypeEstablishments.FindAsync(id);
            if (typeEstablishmentEntity == null)
            {
                return NotFound();
            }
            return View(typeEstablishmentEntity);
        }

        // POST: TypeEstablishments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameType")] TypeEstablishmentEntity typeEstablishmentEntity)
        {
            if (id != typeEstablishmentEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeEstablishmentEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeEstablishmentEntityExists(typeEstablishmentEntity.Id))
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
            return View(typeEstablishmentEntity);
        }

        // GET: TypeEstablishments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeEstablishmentEntity = await _context.TypeEstablishments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeEstablishmentEntity == null)
            {
                return NotFound();
            }

            return View(typeEstablishmentEntity);
        }

        // POST: TypeEstablishments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeEstablishmentEntity = await _context.TypeEstablishments.FindAsync(id);
            _context.TypeEstablishments.Remove(typeEstablishmentEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeEstablishmentEntityExists(int id)
        {
            return _context.TypeEstablishments.Any(e => e.Id == id);
        }
    }
}
