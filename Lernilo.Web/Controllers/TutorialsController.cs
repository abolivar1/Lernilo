using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lernilo.Web.Data;
using Lernilo.Web.Helpers;
using Lernilo.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lernilo.Web.Controllers
{
    [Authorize(Roles = "Customer")]

    public class TutorialsController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;

        public TutorialsController(
            DataContext context,
            IImageHelper imageHelper,
            IConverterHelper converterHelper,
            ICombosHelper combosHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tutorials
                .Include(t => t.Category)
                .Where(t => t.Customer.User.Email.ToLower().Equals(User.Identity.Name.ToLower())).OrderBy(t => t.Date)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            TutorialViewModel model = new TutorialViewModel
            {
                Categories = _combosHelper.GetComboCategories()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TutorialViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;

                if (model.LogoFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.LogoFile, "Tutorials");
                }

                var tutorial = _converterHelper.ToTutorial(model, path, true);
                tutorial.Customer = await _context.Customers
                        .FirstOrDefaultAsync(e => e.User.Email.ToLower().Equals(User.Identity.Name.ToLower()));
                tutorial.Date = DateTime.Now.ToUniversalTime();
                _context.Add(tutorial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorial = await _context.Tutorials
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (tutorial == null)
            {
                return NotFound();
            }
            var model = _converterHelper.ToTutorialViewModel(tutorial);
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TutorialViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string path = model.PicturePath;

                    if (model.LogoFile != null)
                    {
                        path = await _imageHelper.UploadImageAsync(model.LogoFile, "Tutorials");
                    }
                    var tutorial = _converterHelper.ToTutorial(model, path, false);
                    _context.Update(tutorial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "An error has ocurred saving the data"); 
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorial = await _context.Tutorials
                .Include(o => o.Category)
                .Include(o => o.Comments)
                .ThenInclude(c => c.Customer)
                .ThenInclude(cu => cu.User)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (tutorial == null)
            {
                return NotFound();
            }

            return View(tutorial);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var tutorial = await _context.Tutorials
                .Include(c => c.Comments)
                .Include(c => c.TutorialReports)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (tutorial.Comments.Count > 0)
            {
                foreach(var comment in tutorial.Comments)
                {
                    _context.Comments.Remove(comment);
                }
                
            }
            if (tutorial.TutorialReports.Count > 0)
            {
                foreach (var report in tutorial.TutorialReports)
                {
                    _context.TutorialReports.Remove(report);
                }

            }
            _context.Tutorials.Remove(tutorial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}