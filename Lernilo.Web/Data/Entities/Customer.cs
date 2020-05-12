using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lernilo.Web.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public User User { get; set; }

        public ICollection<Tutorial> Tutorials { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Rate> Rates { get; set; }

        public ICollection<FavouriteTutorial> FavouriteTutorials { get; set; }

        public ICollection<TutorialReport> TutorialReports { get; set; }
    }
}
