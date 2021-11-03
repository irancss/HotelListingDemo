using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Models
{
    public class UserDTO
    {
        [StringLength(maximumLength:30,ErrorMessage = "your firstName is too Long")]
        public string FirstName { get; set; }
        [StringLength(maximumLength: 30, ErrorMessage = "your lastName is too Long")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(15,ErrorMessage = "your password is limited to {2} to {1} characters",MinimumLength = 7)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
