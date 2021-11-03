using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Configurations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class DataBaseContext : IdentityDbContext<ApiUser>
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().HasData(
                new Country()
                {
                    CountryId = 1,
                    Name = "jamaica",
                    ShortName = "JM"
                },
                new Country()
                {
                    CountryId = 2,
                    Name = "Bahamas",
                    ShortName = "Bs"
                },
                new Country()
                {
                    CountryId = 3,
                    Name = "Cayman Island",
                    ShortName = "CI"
                }
            );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel()
                {
                    HotelId = 1,
                    Name = "Sandals Resort",
                    Address = "Negril",
                    Rating = 4.5,
                    CountryId = 1
                },
                new Hotel()
                {
                    HotelId = 2,
                    Name = "Comfort Suites",
                    CountryId = 2,
                    Address = "George Town",
                    Rating = 4.4,
                },
                new Hotel()
                {
                    HotelId = 3,
                    Name = "Grand Palldium",
                    CountryId = 3,
                    Address = "Nassua",
                    Rating = 4.6,
                }
            );

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
