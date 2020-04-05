using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRecord.Web.Data.Entities;

namespace TimeRecord.Web.Data
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<TripEntity> Trips { get; set; }

        public DbSet<TripDetailEntity> TripDetails { get; set; }

        public DbSet<ExpenseTypeEntity> ExpenseTypes { get; set; }
    }
}
