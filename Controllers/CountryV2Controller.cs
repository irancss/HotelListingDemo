using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.Data;
using HotelListing.IRepository;
using HotelListing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace HotelListing.Controllers
{
    //**************** For Tasting Version API Checker ***********//
    [ApiVersion("2")]
    [Route("api/Country")]
    [ApiController]
    public class CountryV2Controller : ControllerBase
    {

        //**************** For Tasting Version API Checker ***********//
        private IMapper _mapper;
        private ILogger<CountryV2Controller> _logger;
        private IUnitOfWork _unitOfWork;

        public CountryV2Controller(IMapper mapper, ILogger<CountryV2Controller> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        //**************** For Tasting Version API Checker ***********//
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {

            var countries = await _unitOfWork.Countries.GetAll(requestParams);
            var results = _mapper.Map<IList<CountryDTO>>(countries);
            return Ok(results);

        }
    }
}
