using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.DTOs.Account;
using Services.Helpers.Enums;
using Services.Helpers.Responses;
using Services.Services;
using Services.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Controllers
{
   
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IAccountService service, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }



        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> SignUp([FromBody] RegisterDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _service.SignUpAsync(request));
        }




        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateRole()
        {
            await _service.CreateRoleAsync();
            return Ok();
        }


        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> AddRoleToUser([FromBody] UserRoleDto request)
        {
            await _service.AddRoleToUserAsync(request);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRolesByUser(string userId)
        {
            return Ok(await _service.GetRolesByUserAsync(userId));
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _service.GetRolesAsync());
        }

        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _service.GetUsersAsync());
        }


      
        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> SignIn([FromBody] LoginDto request)
        {
            return Ok(await _service.SignInAsync(request));
        }

    }
}
