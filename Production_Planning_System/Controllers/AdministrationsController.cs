using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Production_Planning_System.Data;

using Production_Planning_System.Models;
using System.IO;
using Microsoft.AspNetCore.Identity;
using System;
using Production_Planning_System.Areas.Identity.Data;


namespace Production_Planning_System.Controllers
{
    public class AdministrationsController : Controller
    {
        private readonly Production_Planning_SystemContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
       
      

        public AdministrationsController(Production_Planning_SystemContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

  


public async Task<IActionResult> Index()
        {
            
            var employee = await _context.Administration.ToListAsync();
            return View(employee);
        }

        public IActionResult New()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New([Bind("Id,FirstName,LastName,FullName, Age ,Gender,Position,Office, Salary,ImageName,ProfilePicture")] Administration administration, IFormFile ProfileImage)
        {
            if (ModelState.IsValid)
            {
                var imgext = Path.GetExtension(ProfileImage.FileName);
              
                  if(imgext == ".jpg" || imgext == ".png" || imgext==".jpeg"|| imgext == ".jfif")
                {
                    var path = Path.Combine(webHostEnvironment.WebRootPath, "Images", ProfileImage.FileName);
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await ProfileImage.CopyToAsync(stream);
                        stream.Close();
                    }

                    administration.ProfilePicture = path;
                    administration.ImageName = ProfileImage.FileName;
                    administration.FullName = administration.FirstName + " " + administration.LastName;

                    _context.Add(administration);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    await Response.WriteAsync("Only .Jpg,.png,.Jpeg,.jfif allowed");
                }

                return RedirectToAction(nameof(Index));
            }
           
            return View(administration);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Administration == null)
            {
                return RedirectToAction(nameof(Index));
            }
            // var displayimgdetails = await _context.Administration.FindAsync(id);
            var administration = await _context.Administration.FindAsync(id);
            if (administration == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(administration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,FullName, Age ,Gender,Position,Office, Salary,ImageName,ProfilePicture")] Administration administration, IFormFile ProfileImage)
        {

            if (id != administration.Id)
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
                        var path = Path.Combine(webHostEnvironment.WebRootPath, "Images", ProfileImage.FileName);
                        using (FileStream stream = new FileStream(path, FileMode.Create))
                        {
                            await ProfileImage.CopyToAsync(stream);
                            stream.Close();
                        }

                        administration.ProfilePicture = path;
                        administration.ImageName = ProfileImage.FileName;
                        administration.FullName = administration.FirstName + " " + administration.LastName;



                        _context.Update(administration);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                      await  Response.WriteAsync("Only .Jpg,.png,.jfif allowed");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministrationExists(administration.Id))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(administration);

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Administration == null)
            {
                return NotFound();
            }
           
            var administration = await _context.Administration
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administration == null)
            {
                return NotFound();
            }

            return View(administration);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Administration == null)
            {
                return NotFound();
            }

            var administration = await _context.Administration
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administration == null)
            {
                return NotFound();
            }

            return View(administration);
        }

        // POST: Productions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Administration == null)
            {
                return Problem("Entity set 'Production_Planning_SystemContext.Administration'  is null.");
            }
            var administration = await _context.Administration.FindAsync(id);
            if (administration != null)
            {
                _context.Administration.Remove(administration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        



        private bool AdministrationExists(int id)
        {
            return _context.Administration.Any(e => e.Id == id);
        }
    }
}
