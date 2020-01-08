using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice2.Data;
using Spice2.Models;

namespace Spice2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CouponController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CouponController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _db.Coupon.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coupon coupons)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }

                    coupons.Picture = p1;
                }

                _db.Coupon.Add(coupons);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(coupons);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coupon = await _db.Coupon.FindAsync(id);

            if (coupon == null)
            {
                return NotFound();
            }

            return View(coupon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Coupon coupons)
        {
            if (coupons.Id == 0)
            {
                return NotFound();
            }

            var couponFromDb = await _db.Coupon.FindAsync(coupons.Id);

            if (ModelState.IsValid)
            {

                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }


                    couponFromDb.Picture = p1;
                }

                couponFromDb.Name = coupons.Name;
                couponFromDb.CouponType = coupons.CouponType;
                couponFromDb.Discount = coupons.Discount;
                couponFromDb.MinimumAmount = coupons.MinimumAmount;
                couponFromDb.isActive = coupons.isActive;



                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(coupons);
        }

        //GET - DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var couponFromDb = await _db.Coupon.SingleOrDefaultAsync(m => m.Id == id);

            if (couponFromDb == null)
            {
                return NotFound();
            }

            return View(couponFromDb);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var couponFromDb = await _db.Coupon.SingleOrDefaultAsync(m => m.Id == id);

            if (couponFromDb == null)
            {
                return NotFound();
            }

            return View(couponFromDb);
        }

        //POST Delete Coupon
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var couponFromDb = await _db.Coupon.FindAsync(id);

            if (couponFromDb != null)
            {
                
                _db.Coupon.Remove(couponFromDb);
                await _db.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }


    }
}