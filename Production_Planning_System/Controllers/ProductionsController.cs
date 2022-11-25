using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Production_Planning_System.Data;
using Production_Planning_System.Models;

namespace Production_Planning_System.Controllers
{
    public class ProductionsController : Controller
    {
        private readonly Production_Planning_SystemContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductionsController(Production_Planning_SystemContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Productions
        public async Task<IActionResult> Index()
        {
            var product =await _context.Production.ToListAsync();
            return View(product);
        }

        // GET: Productions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Production == null)
            {
                return NotFound();
            }

            var production = await _context.Production
                .FirstOrDefaultAsync(m => m.Product_Id == id);
            if (production == null)
            {
                return NotFound();
            }

            return View(production);
        }

        // GET: Productions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Product_Id,Product_Name,Product_Shape,Product_Color,Product_Quantity, Product_Price,ImageName,ProfilePicture")] Production production, IFormFile fileobj)
        {
            if (ModelState.IsValid)
            {
               
                var imgext = Path.GetExtension(fileobj.FileName);

                if (imgext == ".jpg" || imgext == ".png" || imgext == ".jpeg" || imgext == ".jfif")
                {

                    var Productpath = Path.Combine(webHostEnvironment.WebRootPath, "Images", fileobj.FileName);
                    using (FileStream stream = new FileStream(Productpath, FileMode.Create))
                    {
                        await fileobj.CopyToAsync(stream);
                        stream.Close();
                    }

                    production.ProfilePicture = Productpath;
                    production.ImageName = fileobj.FileName;

                    _context.Add(production);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    await Response.WriteAsync("Only .Jpg,.png,.Jpeg,.jfif allowed");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(production);
        }

        // GET: Productions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Production == null)
            {
                return NotFound();
            }

            var production = await _context.Production.FindAsync(id);
            if (production == null)
            {
                return NotFound();
            }
            return View(production);
        }

        // POST: Productions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Product_Id,Product_Name,Product_Shape,Product_Color,Product_Quantity, Product_Price,ImageName,ProfilePicture")] Production production, IFormFile ProfileImage)
        {
            if (id != production.Product_Id)
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
                        var Productpath = Path.Combine(webHostEnvironment.WebRootPath, "Images", ProfileImage.FileName);
                        using (FileStream stream = new FileStream(Productpath, FileMode.Create))
                        {
                            await ProfileImage.CopyToAsync(stream);
                            stream.Close();
                        }

                        production.ProfilePicture = Productpath;
                        production.ImageName = ProfileImage.FileName;
                        _context.Update(production);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                       await Response.WriteAsync("Only .Jpg,.png,.Jpeg,.jfif allowed");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionExists(production.Product_Id))
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
            return View(production);
        }

        // GET: Productions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Production == null)
            {
                return NotFound();
            }

            var production = await _context.Production
                .FirstOrDefaultAsync(m => m.Product_Id == id);
            if (production == null)
            {
                return NotFound();
            }

            return View(production);
        }

        // POST: Productions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Production == null)
            {
                return Problem("Entity set 'Production_Planning_SystemContext.Production'  is null.");
            }
            var production = await _context.Production.FindAsync(id);
            if (production != null)
            {
                _context.Production.Remove(production);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductionExists(int id)
        {
          return _context.Production.Any(e => e.Product_Id == id);
        }
    }
}
