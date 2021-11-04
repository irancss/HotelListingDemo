using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Models
{
    public class RequestParams
    {
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;
        public int _pageSize { get; set; } = 10;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > PageSize) ? MaxPageSize : value; }
        }

    }
}
