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
using HotelListing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private ILogger<HotelController> _logger;


        public HotelController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<HotelController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotels()
        {

            var hotels = await _unitOfWork.Hotels.GetAll();
            var result = _mapper.Map<IList<HotelDTO>>(hotels);
            return Ok(result);

        }

        [Authorize]
        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int id)
        {

            var hotel = await _unitOfWork.Hotels.Get(h => h.HotelId == id, new List<string>() { "Country" });
            var result = _mapper.Map<HotelDTO>(hotel);
            return Ok(result);

        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDTO hotelDTO)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post attempt in {nameof(CreateHotel)}");
                return BadRequest(ModelState);
            }


            var hotel = _mapper.Map<Hotel>(hotelDTO);
            await _unitOfWork.Hotels.Insert(hotel);
            await _unitOfWork.Save();


            return CreatedAtRoute("GetHotel", new { id = hotel.HotelId }, hotel);


        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] UpdateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid Update attempt in {nameof(UpdateHotel)}");
                return BadRequest(ModelState);
            }


            var hotel = await _unitOfWork.Hotels.Get(h => h.HotelId == id);
            if (hotel == null)
            {
                _logger.LogError($"Invalid Update attempt in {nameof(UpdateHotel)}");
                return BadRequest("Submitted data is Invalid");
            }

            _mapper.Map(hotelDTO, hotel);
            _unitOfWork.Hotels.Update(hotel);
            await _unitOfWork.Save();

            return NoContent();

        }




        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid Delete attempt in {nameof(DeleteHotel)}");
                return BadRequest();
            }


            var hotel = await _unitOfWork.Hotels.Get(h => h.HotelId == id);

            if (hotel == null)
            {
                _logger.LogError($"Invalid Delete attempt in {nameof(DeleteHotel)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Hotels.Delete(id);
            await _unitOfWork.Save();

            return NoContent();



        }





    }
}
