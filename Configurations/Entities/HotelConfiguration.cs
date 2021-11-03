using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
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
        }
    }
}
