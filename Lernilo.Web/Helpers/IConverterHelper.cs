using Lernilo.Web.Data.Entities;
using Lernilo.Web.Models;
using System.Collections.Generic;
using Lernilo.Common.Models;

namespace Lernilo.Web.Helpers
{
    public interface IConverterHelper
    {
        Tutorial ToTutorial(TutorialViewModel model, string path, bool isNew);

        TutorialViewModel ToTutorialViewModel(Tutorial tutorial);

        TutorialResponse ToTutorialResponse(Tutorial tutorial);

        List<TutorialResponse> ToTutorialResponse(List<Tutorial> tutorials);

    }

}
