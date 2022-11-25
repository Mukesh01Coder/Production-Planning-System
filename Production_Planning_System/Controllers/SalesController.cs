using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Production_Planning_System.Data;
using Production_Planning_System.Models;

namespace Production_Planning_System.Controllers
{
    public class SalesController : Controller
    {
        private readonly Production_Planning_SystemContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public SalesController(Production_Planning_SystemContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var sales = await _context.Sales.ToListAsync();
            return View(sales);
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return Content("Details Not Found!.... Please Enter The Details of Sales");// NotFound();
            }

            var sales = await _context.Sales
                .FirstOrDefaultAsync(m => m.sales_Id == id);
            if (sales == null)
            {
                 return Content("Details Not Found!.... Please First Add Details of Sales");//NotFound();
            }

            return View(sales);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("sales_Id,order_num,Demand,Oder_Type,ImageName,ProfilePicture")] Sales sales, IFormFile ProfileImage)
        {
            if (ModelState.IsValid)
            {
                var imgext = Path.GetExtension(ProfileImage.FileName);

                if (imgext == ".jpg" || imgext == ".png" || imgext == ".jpeg" || imgext == ".jfif")
                {
                    var salespath = Path.Combine(webHostEnvironment.WebRootPath, "Images", ProfileImage.FileName);
                    using (FileStream stream = new FileStream(salespath, FileMode.Create))
                    {
                        await ProfileImage.CopyToAsync(stream);
                        stream.Close();
                    }

                    sales.ProfilePicture = salespath;
                    sales.ImageName = ProfileImage.FileName;


                    _context.Add(sales);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    await Response.WriteAsync("Only .Jpg,.png,.Jpeg,.jfif allowed");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sales);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }
            return View(sales);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("sales_Id,order_num,Demand,Oder_Type,ImageName,ProfilePicture")] Sales sales, IFormFile ProfileImage)
        {
            if (id != sales.sales_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var imgext = Path.GetExtension(ProfileImage.FileName);

                    if (imgext == ".jpg" || imgext == ".png" || imgext == ".jpeg" || imgext == ".jfif")
                    {
                        var salespath = Path.Combine(webHostEnvironment.WebRootPath, "Images", ProfileImage.FileName);
                        using (FileStream stream = new FileStream(salespath, FileMode.Create))
                        {
                            await ProfileImage.CopyToAsync(stream);
                            stream.Close();
                        }

                        sales.ProfilePicture = salespath;
                        sales.ImageName = ProfileImage.FileName;


                        _context.Update(sales);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        await Response.WriteAsync("Only .Jpg,.png,Jpeg,.jfif allowed");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesExists(sales.sales_Id))
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
            return View(sales);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales
                .FirstOrDefaultAsync(m => m.sales_Id == id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sales == null)
            {
                return Problem("Entity set 'Production_Planning_SystemContext.Sales'  is null.");
            }
            var sales = await _context.Sales.FindAsync(id);
            if (sales != null)
            {
                _context.Sales.Remove(sales);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesExists(int id)
        {
          return _context.Sales.Any(e => e.sales_Id == id);
        }
    }
}
