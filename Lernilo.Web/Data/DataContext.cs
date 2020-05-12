using Lernilo.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lernilo.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tutorial> Tutorials { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<FavouriteTutorial> FavouriteTutorials { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<TutorialReport> TutorialReports { get; set; }

        public DbSet<Manager> Managers { get; set; }
    }

}
