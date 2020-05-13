using Lernilo.Web.Data.Entities;
using Lernilo.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lernilo.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext context,
            IUserHelper userHelper)
        {
            _dataContext = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("Alex Jhojan", "Bolivar Vargas", "123456", "alex.jhojanx@gmail.com", "Admin");
            var customer = await CheckUserAsync("Alex Jhojan", "Bolivar Vargas", "123456", "alex@yopmail.com", "Customer");
            var customer2 = await CheckUserAsync("Sergio", "Vargas", "123456", "sergio@yopmail.com", "Customer");
            await CheckCustomerAsync(customer, customer2);
            await CheckManagerAsync(manager);
        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");

        }

        private async Task<User> CheckUserAsync(string firstName, string lastName, string document, string email, string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    Document = document,
                    PicturePath = $"~/images/Tournaments/alex.png"
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);
            }
            /*var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            await _userHelper.ConfirmEmailAsync(user, token);*/

            return user;
        }

        private async Task CheckCustomerAsync(User user, User user2)
        {
            if (!_dataContext.Customers.Any())
            {
                _dataContext.Customers.Add(new Customer { User = user });
                _dataContext.Customers.Add(new Customer { User = user2 });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckManagerAsync(User user)
        {
            if (!_dataContext.Managers.Any())
            {
                _dataContext.Managers.Add(new Manager { User = user });
                await _dataContext.SaveChangesAsync();
            }
        }
        
    }
}
