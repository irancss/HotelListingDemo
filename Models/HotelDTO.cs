using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Models
{
    public class HotelDTO : CreateHotelDTO
    {
        public int HotelId { get; set; }
        public CountryDTO Country { get; set; }
    }

    public class CreateHotelDTO
    {

        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Hotel name is too Long")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 750, ErrorMessage = "Hotel Address is too Long")]
        public string Address { get; set; }

        [Required]
        [Range(1, 5)]
        public double Rating { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
