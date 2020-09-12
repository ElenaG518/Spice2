using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice2.Data;
using Spice2.Models;
using Spice2.Utility;

namespace Spice2.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        // _db here is our local object
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
            //this db here is the object which we retrieve from our container using dependency injection
            //what we do here is assign _db the value fo the retrieved context, which is db.
        {
            _db = db;
        }

        //in order to maintain the syntax convention, we must make all actions methods as Task of Async
        //by using async await pattern
        
        //GET
        public async Task<IActionResult> Index()
        {

            return View(await _db.Category.ToListAsync());
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                //if valid
                _db.Category.Add(category);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            }
            return View(category);
        }

        //GET - DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _db.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _db.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Update(category);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _db.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var category = await _db.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            _db.Category.Remove(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}