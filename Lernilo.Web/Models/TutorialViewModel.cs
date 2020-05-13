using Lernilo.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lernilo.Web.Models
{
    public class TutorialViewModel : Tutorial
    {
        [Display(Name = "Picture")]
        public IFormFile LogoFile { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Category")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Category.")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }


    }
}
