using TimeRecord.Common.Enums;
using TimeRecord.Web.Data.Entities;
using TimeRecord.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeRecord.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Carlos", "Oquendo", "carlosoquendo2@hotmail.com", "3010000000", "Avenida siempre viva", UserType.Admin);
            await CheckUsersAsync();
        }

        private async Task CheckUsersAsync()
        {
            for (int i = 1; i <= 100; i++)
            {
                await CheckUserAsync($"100{i}", "User", $"{i}", $"user{i}@yopmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.User);
            }
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<UserEntity> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            UserType userType)
        {
            UserEntity user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456789o");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                //var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                //await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }
    }
}
