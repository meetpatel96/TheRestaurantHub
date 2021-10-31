using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HCI_Restaurants.Models;

namespace HCI_Restaurants.Controllers
{
    public class CuisinesController : Controller
    {
        private readonly hcirestaurantsContext _context;

        public CuisinesController(hcirestaurantsContext context)
        {
            _context = context;
        }

        // GET: Cuisines
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cuisines.ToListAsync());
        }

        // GET: Cuisines/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuisines = await _context.Cuisines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuisines == null)
            {
                return NotFound();
            }

            return View(cuisines);
        }

        // GET: Cuisines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cuisines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CuisineName")] Cuisines cuisines)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cuisines);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cuisines);
        }

        // GET: Cuisines/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuisines = await _context.Cuisines.FindAsync(id);
            if (cuisines == null)
            {
                return NotFound();
            }
            return View(cuisines);
        }

        // POST: Cuisines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CuisineName")] Cuisines cuisines)
        {
            if (id != cuisines.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuisines);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuisinesExists(cuisines.Id))
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
            return View(cuisines);
        }

        // GET: Cuisines/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuisines = await _context.Cuisines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuisines == null)
            {
                return NotFound();
            }

            return View(cuisines);
        }

        // POST: Cuisines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cuisines = await _context.Cuisines.FindAsync(id);
            _context.Cuisines.Remove(cuisines);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuisinesExists(long id)
        {
            return _context.Cuisines.Any(e => e.Id == id);
        }
    }
}
