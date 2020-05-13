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

    }
}