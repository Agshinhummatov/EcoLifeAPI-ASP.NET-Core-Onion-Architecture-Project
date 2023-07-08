using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.DTOs.Account;
using Services.Helpers;
using Services.Helpers.Enums;
using Services.Helpers.Responses;
using Services.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.AspNetCore.Http;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly JWTSettings _jwtSetting;
      

        public AccountService(UserManager<AppUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IMapper mapper,
                              IOptions<JWTSettings> jwtSetting)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _jwtSetting = jwtSetting.Value;
        }

        public async Task AddRoleToUserAsync(UserRoleDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            await _userManager.AddToRoleAsync(user, role.ToString());
        }

        public async Task<IEnumerable<string>> GetRolesByUserAsync(string userId)
        {
            return await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(userId));
        }

        public async Task CreateRoleAsync()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }

        public async Task<IEnumerable<IdentityRole>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return _mapper.Map<List<UserDto>>(await _userManager.Users.ToListAsync());
        }



       




        public async Task<RegisterResponse> SignUpAsync(RegisterDto model)
        {
            AppUser user = _mapper.Map<AppUser>(model);

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return new RegisterResponse { StatusMessage = "Failed", Errors = result.Errors.Select(m => m.Description).ToList() };

            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            return new RegisterResponse { Errors = null, StatusMessage = "Success" };
        }





        //public async Task<LoginResponse> SignInAsync(LoginDto model)
        //{
        //    var user = await _userManager.FindByEmailAsync(model.Email);

        //    if(user == null)
        //        return new LoginResponse { Token = null, StatusMessage = "Failed", Errors = new List<string>() { "Email or password wrong" } };

        //    if (!await _userManager.CheckPasswordAsync(user, model.Password))
        //        return new LoginResponse { Token = null, StatusMessage = "Failed", Errors = new List<string>() { "Email or password wrong" } };



        //    var roles = await _userManager.GetRolesAsync(user);

        //    string token = GenerateJwtToken(user.UserName, (List<string>)roles);

        //    return new LoginResponse { Errors = null, StatusMessage = "Success", Token = token };

        //}

        public async Task<string?> SignInAsync(LoginDto model)
        {
            var dbUser = await _userManager.FindByEmailAsync(model.Email);

            if (dbUser is null) return null;

            if (!await _userManager.CheckPasswordAsync(dbUser, model.Password))
                return null;


            var roles = await _userManager.GetRolesAsync(dbUser);


            return GenerateJwtToken(dbUser.UserName, dbUser.Id, (List<string>)roles);
        }

        private string GenerateJwtToken(string username, string userId, List<string> roles)
        {
            var claims = new List<Claim>
         {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, username)
         };

            roles.ForEach(role =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSetting.ExpireDays));

            var token = new JwtSecurityToken(
                _jwtSetting.Issuer,
                _jwtSetting.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
