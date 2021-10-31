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
    public class Covid19Controller : Controller
    {
        private readonly hcirestaurantsContext _context;

        public Covid19Controller(hcirestaurantsContext context)
        {
            _context = context;
        }

        // GET: Covid19
        public async Task<IActionResult> Index()
        {
            var hcirestaurantsContext = _context.Covid19.Include(c => c.Restaurant);
            return View(await hcirestaurantsContext.ToListAsync());
        }

        // GET: Covid19/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var covid19 = await _context.Covid19
                .Include(c => c.Restaurant)
                .FirstOrDefaultAsync(m => m.RestaurantId == id);
            if (covid19 == null)
            {
                return NotFound();
            }

            return View(covid19);
        }

        // GET: Covid19/Create
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id");
            return View();
        }

        // POST: Covid19/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RestaurantId,TakeOut,LimitSeating,IndoorDining,Curbside,Comments")] Covid19 covid19)
        {
            if (ModelState.IsValid)
            {
                _context.Add(covid19);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", covid19.RestaurantId);
            return View(covid19);
        }

        // GET: Covid19/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var covid19 = await _context.Covid19.FindAsync(id);
            if (covid19 == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", covid19.RestaurantId);
            return View(covid19);
        }

        // POST: Covid19/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,RestaurantId,TakeOut,LimitSeating,IndoorDining,Curbside,Comments")] Covid19 covid19)
        {
            if (id != covid19.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(covid19);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Covid19Exists(covid19.Id))
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
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", covid19.RestaurantId);
            return View(covid19);
        }

        // GET: Covid19/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var covid19 = await _context.Covid19
                .Include(c => c.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (covid19 == null)
            {
                return NotFound();
            }

            return View(covid19);
        }

        // POST: Covid19/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var covid19 = await _context.Covid19.FindAsync(id);
            _context.Covid19.Remove(covid19);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Covid19Exists(long id)
        {
            return _context.Covid19.Any(e => e.Id == id);
        }
    }
}
