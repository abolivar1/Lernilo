using Lernilo.Web.Data.Entities;
using Lernilo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lernilo.Web.Helpers
{
    public interface IConverterHelper
    {
        Tutorial ToTutorial(TutorialViewModel model, string path, bool isNew);

        TutorialViewModel ToTutorialViewModel(Tutorial tutorial);
    }

}
