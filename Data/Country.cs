using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Data
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        
        [MaxLength(350)]
        [Required]
        public string Name { get; set; }

        [MaxLength(350)]
        [Required]
        public string ShortName { get; set; }

        #region Relations

        public List<Hotel> Hotels { get; set; }

        #endregion

    }
}
