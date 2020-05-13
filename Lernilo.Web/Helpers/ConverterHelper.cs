using Lernilo.Web.Data;
using Lernilo.Web.Data.Entities;
using Lernilo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lernilo.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(
            DataContext context,
            ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }
        public Tutorial ToTutorial(TutorialViewModel model, string path, bool isNew)
        {
            return new Tutorial
            {
                Id = isNew ? 0 : model.Id,
                PicturePath = path,
                Title = model.Title,
                Description = model.Description,
                Category = _context.Categories.Find(model.CategoryId)
            };
        }

        public TutorialViewModel ToTutorialViewModel(Tutorial tutorial)
        {
            return new TutorialViewModel
            {
                Id = tutorial.Id,
                PicturePath = tutorial.PicturePath,
                Title = tutorial.Title,
                Description = tutorial.Description,
                TotalRate = tutorial.TotalRate,
                CategoryId = tutorial.Category.Id,
                Categories = _combosHelper.GetComboCategories(),
                Comments = tutorial.Comments,
                TutorialReports = tutorial.TutorialReports,
                Date = tutorial.Date
            };
        }
    }

}
