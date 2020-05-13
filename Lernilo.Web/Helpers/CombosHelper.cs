using Lernilo.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lernilo.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;

        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboCategories()
        {
            var list = _dataContext.Categories.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = $"{s.Id}"
            })
                .OrderBy(s => s.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Select a category",
                Value = "0"
            });

            return list;
        }
    }
    }
