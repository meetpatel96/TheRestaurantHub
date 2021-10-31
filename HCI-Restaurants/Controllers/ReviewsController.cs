using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HCI_Restaurants.Models;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace HCI_Restaurants.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly hcirestaurantsContext _context;

        public ReviewsController(hcirestaurantsContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.Name = _context.Restaurants.Where(x => x.Id == id).Select(x => x.Name).SingleOrDefault();
            var review = _context.Reviews.Include(r => r.Restaurant).Where(x => x.RestaurantId == id);
            return View(await review.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id","RestaurantId,Rating,RatingText,ReviewText,ReviewTimeFriendly,CustomerName")] Reviews reviews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reviews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Reviews", new { id = reviews.RestaurantId});
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", reviews.RestaurantId);
            return RedirectToAction("Index", "Restaurants");
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (String.IsNullOrEmpty(id.ToString()))
            {
                return NotFound();
            }

            var reviews = await _context.Reviews.FindAsync(id);
            if (reviews == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", reviews.RestaurantId);
            return View(reviews);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RestaurantId,Rating,RatingText,ReviewText,ReviewTimeFriendly,CustomerName")] Reviews reviews)
        {
            if (id != reviews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reviews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewsExists(reviews.Id))
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
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", reviews.RestaurantId);
            return View(reviews);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (String.IsNullOrEmpty(id.ToString()))
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviews = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(reviews);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewsExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
