using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HCI_Restaurants.Models;
using System.ComponentModel;

namespace HCI_Restaurants.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly hcirestaurantsContext _context;

        public RestaurantsController(hcirestaurantsContext context)
        {
            _context = context;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "RestName" : "";
            ViewData["CuisineSortParm"] = string.IsNullOrEmpty(sortOrder) ? "CuisineType" : "";
            ViewData["DeliverySortParm"] = string.IsNullOrEmpty(sortOrder) ? "Delivery" : "";
            ViewData["TakeawaySortParm"] = string.IsNullOrEmpty(sortOrder) ? "Takeaway" : "";

            var rest = from r in _context.Restaurants
                       select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                rest = rest.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "RestName":
                    rest = rest.OrderBy(r => r.Name);
                    break;
                case "CuisineType":
                    rest = rest.OrderBy(r => r.Cuisines);
                    break;
                case "Delivery":
                    rest = rest.OrderBy(r => r.HasDelivery);
                    break;
                case "Takeaway":
                    rest = rest.OrderBy(r => r.HasTakeaway);
                    break;
                default:
                    rest = rest.OrderBy(r => r.Id);
                    break;
            }

            //var hcirestaurantsContext = _context.Restaurants.Include(r => r.CityNavigation).Include(r => r.Cuisine);
            //return View(await hcirestaurantsContext.ToListAsync());

            return View(await rest.ToListAsync());
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurants = await _context.Restaurants
                .Include(r => r.CityNavigation)
                .Include(r => r.Cuisine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurants == null)
            {
                return NotFound();
            }

            return View(restaurants);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id");
            ViewData["CuisineId"] = new SelectList(_context.Cuisines, "Id", "Id");
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CityId,CuisineId,Cuisines,Currency,Establishment,HasDelivery,HasTakeaway,Address,City,StateCode,Locality,LocalityVerbose,ZipCode,MenuUrl,Name,Telephone,PriceRange,Timings,Url,AggregateRating,RatingText")] Restaurants restaurants)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurants);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id", restaurants.CityId);
            ViewData["CuisineId"] = new SelectList(_context.Cuisines, "Id", "Id", restaurants.CuisineId);
            return View(restaurants);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurants = await _context.Restaurants.FindAsync(id);
            if (restaurants == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id", restaurants.CityId);
            ViewData["CuisineId"] = new SelectList(_context.Cuisines, "Id", "Id", restaurants.CuisineId);
            return View(restaurants);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CityId,CuisineId,Cuisines,Currency,Establishment,HasDelivery,HasTakeaway,Address,City,StateCode,Locality,LocalityVerbose,ZipCode,MenuUrl,Name,Telephone,PriceRange,Timings,Url,AggregateRating,RatingText")] Restaurants restaurants)
        {
            if (id != restaurants.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurants);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantsExists(restaurants.Id))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id", restaurants.CityId);
            ViewData["CuisineId"] = new SelectList(_context.Cuisines, "Id", "Id", restaurants.CuisineId);
            return View(restaurants);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurants = await _context.Restaurants
                .Include(r => r.CityNavigation)
                .Include(r => r.Cuisine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurants == null)
            {
                return NotFound();
            }

            return View(restaurants);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var restaurants = await _context.Restaurants.FindAsync(id);
            _context.Restaurants.Remove(restaurants);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantsExists(long id)
        {
            return _context.Restaurants.Any(e => e.Id == id);
        }
    }
}
