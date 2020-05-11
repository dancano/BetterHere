using BetterHere.Web.Data;
using BetterHere.Web.Data.Entities;
using BetterHere.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ResultOperators.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BetterHere.Web.Controllers
{
    public class EstablishmentsController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;
        private readonly IComboHelper _comboHelper;

        public EstablishmentsController(
            DataContext context,
            IUserHelper userHelper,
            IBlobHelper imageHelper,
            IConfiguration configuration,
            IMailHelper mailHelper,
            IComboHelper comboHelper)
        {
            _userHelper = userHelper;
            _blobHelper = imageHelper;
            _configuration = configuration;
            _mailHelper = mailHelper;
            _comboHelper = comboHelper;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Establishments.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EstablishmentEntity establishmentEntity = await _context.Establishments
                .Include(m => m.EstablishmentLocations)
                .ThenInclude(m => m.Cities)
                .Include(m => m.EstablishmentLocations)
                .ThenInclude(m => m.Foods)
                .Include(m => m.EstablishmentLocations)
                .ThenInclude(m => m.TypeEstablishment)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (establishmentEntity == null)
            {
                return NotFound();
            }

            return View(establishmentEntity);
        }

        public async Task<IActionResult> FoodsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EstablishmentLocationEntity establishmentLocationEntity = await _context.EstablishmentLocations
                .Include(m => m.Cities)
                .Include(m => m.Foods)
                .ThenInclude(m => m.TypeFoods)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (establishmentLocationEntity == null)
            {
                return NotFound();
            }

            return View(establishmentLocationEntity);
        }

        // GET: EstablishmentEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EstablishmentEntity establishmentEntity)
        {
            if (ModelState.IsValid)
            {
                establishmentEntity.Name = establishmentEntity.Name.ToUpper();
                _context.Add(establishmentEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("Duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Already exits a Establishment");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
                
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EstablishmentEntity establishmentEntity)
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
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("Duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Already exits a Establishment");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
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

            _context.Establishments.Remove(establishmentEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
