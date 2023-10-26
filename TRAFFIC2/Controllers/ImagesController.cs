using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TRAFFIC2.Models;

namespace TRAFFIC2.Controllers
{
    public class ImagesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public ImagesController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
              return _context.Images != null ? 
                          View(await _context.Images.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Images'  is null.");
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageId,HomePageId,ImageFile1,ImageFile2,ImageFile3,ImageFile4")] Image image)
        {
            if (ModelState.IsValid)
            {
                if (image.ImageFile1 != null)
                {
                    image.ImagePath1 = await SaveImage(image.ImageFile1);
                }

                if (image.ImageFile2 != null)
                {
                    image.ImagePath2 = await SaveImage(image.ImageFile2);
                }

                if (image.ImageFile3 != null)
                {
                    image.ImagePath3 = await SaveImage(image.ImageFile3);
                }

                if (image.ImageFile4 != null)
                {
                    image.ImagePath4 = await SaveImage(image.ImageFile4);
                }

                _context.Add(image);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(image);

           
        }
        private async Task<string> SaveImage(IFormFile imageFile)
        {
            string wwwRootPath = _webHostEnviroment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string path = Path.Combine(wwwRootPath, "Images", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return fileName;
        }
    

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            return View(image);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ImageId,HomePageId,ImageFile1,ImageFile2,ImageFile3,ImageFile4")] Image image)
        {
            if (id != image.ImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (image.ImageFile1 != null)
                {
                    image.ImagePath1 = await SaveImage(image.ImageFile1);
                }

                if (image.ImageFile2 != null)
                {
                    image.ImagePath2 = await SaveImage(image.ImageFile2);
                }

                if (image.ImageFile3 != null)
                {
                    image.ImagePath3 = await SaveImage(image.ImageFile3);
                }

                if (image.ImageFile4 != null)
                {
                    image.ImagePath4 = await SaveImage(image.ImageFile4);
                }

                _context.Update(image);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(image);
        }

        //// POST: Images/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(decimal id, [Bind("ImageId,HomePageId,ImageFile1,ImageFile2,ImageFile3,ImageFile4")] Image image)
        //{
        //    if (id != image.ImageId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (image.ImageFile1 != null)
        //            {
        //                image.ImagePath1 = await SaveImage(image.ImageFile1);
        //            }

        //            if (image.ImageFile2 != null)
        //            {
        //                image.ImagePath2 = await SaveImage(image.ImageFile2);
        //            }

        //            if (image.ImageFile3 != null)
        //            {
        //                image.ImagePath3 = await SaveImage(image.ImageFile3);
        //            }

        //            if (image.ImageFile4 != null)
        //            {
        //                image.ImagePath4 = await SaveImage(image.ImageFile4);
        //            }

        //            _context.Add(image);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }

        //         try
        //        {
        //            _context.Update(image);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ImageExists(image.ImageId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(image);
        //}


        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Images == null)
            {
                return Problem("Entity set 'ModelContext.Images'  is null.");
            }
            var image = await _context.Images.FindAsync(id);
            if (image != null)
            {
                _context.Images.Remove(image);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(decimal id)
        {
          return (_context.Images?.Any(e => e.ImageId == id)).GetValueOrDefault();
        }
    }
}
