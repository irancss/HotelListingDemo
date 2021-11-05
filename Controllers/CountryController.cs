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
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        //[ResponseCache(CacheProfileName = "120SecondsDuration")]     Removed *** This option added in Startup
        //**********Start For Custom Caching an Action **********//
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        //**********End For Custom Caching an Action **********//
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {

            var countries = await _unitOfWork.Countries.GetAll(requestParams);
            var results = _mapper.Map<IList<CountryDTO>>(countries);
            return Ok(results);

        }


        [HttpGet("{id:int}", Name = "GetCountry")]
        //[ResponseCache(CacheProfileName = "120SecondsDuration")]      Removed *** This option added in Startup
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountry(int id)
        {
            var country = await _unitOfWork.Countries.Get(c => c.CountryId == id, new List<string>() { "Hotels" });
            var results = _mapper.Map<CountryDTO>(country);
            return Ok(results);

        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post attempt in {nameof(CreateCountry)}");
                return BadRequest(ModelState);
            }


            var country = _mapper.Map<Country>(countryDTO);
            await _unitOfWork.Countries.Insert(country);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetCountry", new { id = country.CountryId }, country);


        }


        [Authorize(Roles = "Administrator")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid Update attempt in {nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }


            var country = await _unitOfWork.Countries.Get(c => c.CountryId == id);
            if (country == null)
            {
                _logger.LogError($"Invalid Update attempt in {nameof(UpdateCountry)}");
            }

            _mapper.Map(countryDTO, country);
            _unitOfWork.Countries.Update(country);
            await _unitOfWork.Save();

            return NoContent();


        }


        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid Delete attempt in {nameof(DeleteCountry)}");
                return BadRequest();
            }


            var country = await _unitOfWork.Countries.Get(c => c.CountryId == id);
            if (country == null)
            {
                _logger.LogError($"Invalid Delete attempt in {nameof(DeleteCountry)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Countries.Delete(id);
            await _unitOfWork.Save();

            return NoContent();


        }



    }
}
