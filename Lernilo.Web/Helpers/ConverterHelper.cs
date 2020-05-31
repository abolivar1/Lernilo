using Lernilo.Common.Models;
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

        public TutorialResponse ToTutorialResponse(Tutorial tutorial)
        {
            return new TutorialResponse
            {
                Date = tutorial.Date,
                Id = tutorial.Id,
                Description = tutorial.Description,
                PicturePath = tutorial.PicturePath,
                Title = tutorial.Title,
                TotalRate = tutorial.TotalRate,
                Customer = ToUserResponse(tutorial.Customer.User),
                Category = ToCategoryResponse(tutorial.Category),
                Comments = tutorial.Comments.Select(g => new CommentResponse
                {
                    Id = g.Id,
                    Remark = g.Remark,
                    Date = g.Date,
                    Customer = ToUserResponse(g.Customer.User)
                }).ToList(),
                TutorialReports = tutorial.TutorialReports.Select(g => new TutorialReportResponse
                {
                    Id = g.Id,
                    Report = g.Report,
                    Date = g.Date,
                    Customer = ToUserResponse(g.Customer.User)
                }).ToList(),
            };
        }

        public List<TutorialResponse> ToTutorialResponse(List<Tutorial> tutorials)
        {
            List<TutorialResponse> list = new List<TutorialResponse>();
            foreach (Tutorial tutorial in tutorials)
            {
                list.Add(ToTutorialResponse(tutorial));
            }

            return list;
        }

        private UserResponse ToUserResponse(User user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                Address = user.Address,
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PicturePath = user.PicturePath,
            };
        }

        private CategoryResponse ToCategoryResponse(Category category)
        {
            if (category == null)
            {
                return null;
            }

            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public Tutorial ToTutorial(TutorialViewModel model, string path, bool isNew)
        {
            return new Tutorial
            {
                Id = isNew ? 0 : model.Id,
                PicturePath = path,
                Title = model.Title,
                Description = model.Description,
                Category = _context.Categories.Find(model.CategoryId),
                Date = model.Date
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
