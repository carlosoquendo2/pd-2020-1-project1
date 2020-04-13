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
            await CheckExpenseTypesAsync();
            await CheckUserAsync("1010", "Carlos", "Oquendo", "carlosoquendo2@hotmail.com", "3010000000", "Avenida siempre viva", UserType.Admin);
            await CheckUserAsync("1011", "Felipe", "Escobar", "carlosoquendo1206@gmail.com", "3010000000", "Avenida siempre viva", UserType.User);
            await CheckUserAsync("1012", "Mario", "Bedoya", "mario.bedoya@yopmail.com", "3010000000", "Avenida siempre viva", UserType.User);
            await CheckUserAsync("1013", "Catalina", "Velez", "catalina.velez@yopmail.com", "3010000000", "Avenida siempre viva", UserType.User);
            await CheckUsersAsync();
        }

        private async Task CheckUsersAsync()
        {
            for (int i = 1; i <= 20; i++)
            {
                await CheckUserAsync($"100{i}", "User", $"{i}", $"user{i}@yopmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.User);
            }
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

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

                if (user.UserType == UserType.User)
                {
                    await CheckTrips(user);
                }
            }

            return user;
        }

        private async Task CheckTrips(UserEntity user)
        {
            for (int i = 0; i < 5; i++)
            {
                await CreateTrip(user);
            }
        }

        private async Task CreateTrip(UserEntity user)
        {
            _context.Trips.Add(new TripEntity
            {
                Name = "Trip example - " + user.LastName,
                Description = "Test description",
                StartDate = DateTime.Today.ToUniversalTime(),
                EndDate = DateTime.Today.AddMonths(1).ToUniversalTime(),
                User = user,
                TripDetails = new List<TripDetailEntity>
                {
                    new TripDetailEntity
                    {
                        ExpenseType = _context.ExpenseTypes.FirstOrDefault(),
                        Name = "Trip detail 1",
                        Expense = 9.00,
                        Currency = CurrencyType.USD.ToString(),
                        Comment = "Trip detail comment 1",
                        Date = DateTime.Today.ToUniversalTime(),
                        AttachmentPath = $"~/images/Vouchers/VoucherTest.jpg"
                    },
                    new TripDetailEntity
                    {
                        ExpenseType = _context.ExpenseTypes.Last(),
                        Name = "Trip detail 2",
                        Expense = 15.50,
                        Currency = CurrencyType.USD.ToString(),
                        Comment = "Trip detail comment 2",
                        Date = DateTime.Today.ToUniversalTime(),
                        AttachmentPath = $"~/images/Vouchers/VoucherTest.jpg"
                    },
                    new TripDetailEntity
                    {
                        ExpenseType = _context.ExpenseTypes.FirstOrDefault(),
                        Name = "Trip detail 3",
                        Expense = 10.20,
                        Currency = CurrencyType.USD.ToString(),
                        Comment = "Trip detail comment 3",
                        Date = DateTime.Today.ToUniversalTime(),
                        AttachmentPath = $"~/images/Vouchers/VoucherTest.jpg"
                    }
                }
            });
            await _context.SaveChangesAsync();
        }

        private async Task CheckExpenseTypesAsync()
        {
            _context.ExpenseTypes.Add(new ExpenseTypeEntity { Name = "Taxi", Active = true });
            _context.ExpenseTypes.Add(new ExpenseTypeEntity { Name = "Breakfast", Active = true });
            _context.ExpenseTypes.Add(new ExpenseTypeEntity { Name = "Dinner", Active = true });
            await _context.SaveChangesAsync();
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }
    }
}
