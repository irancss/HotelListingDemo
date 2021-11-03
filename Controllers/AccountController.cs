using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMapper _mapper;
        private ILogger<AccountController> _logger;
        private SignInManager<ApiUser> _signInManager;
        private UserManager<ApiUser> _userManager;

        public AccountController(IMapper mapper, ILogger<AccountController> logger, SignInManager<ApiUser> signInManager, UserManager<ApiUser> userManager)
        {
            _mapper = mapper;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {

        }








    }
}
