using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using DTOs;
using Microsoft.AspNetCore.Identity;
using EPLHouse.Cards.DataAccess.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EPLHouse.Test.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using DTOs.APIDtos;
using EPLHouse.Test.DataAccess.Repositories;


namespace EPLHouse.Cards.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _DbContext;
        private readonly IBaseRepository<AppUser> _baseRepository;

        public LoginController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration,
            AppDBContext appDBContext,
            IBaseRepository<AppUser> baseRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _DbContext = appDBContext;
            _baseRepository = baseRepository;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationDto>> Login([FromBody] LoginDto model)
        {
            var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
            if (appUser == null) return BadRequest();
            var result = await _signInManager.PasswordSignInAsync(appUser.UserName, model.Password, false, false);
            if (result.Succeeded)
            {
                AuthenticationDto auth = new AuthenticationDto
                {
                    Token = GenerateJwtToken(model.Email, appUser),
                    UserId = appUser.Id,
                    UserRole = Convert.ToInt32(_DbContext.UserRoles.Where(x => x.UserId == appUser.Id).FirstOrDefault().RoleId)
                };
                return Ok(auth);
            }
            else return BadRequest();
        }

        private string GenerateJwtToken(string email, AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:JwtIssuer"],
                _configuration["Jwt:JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Authorize]
        [HttpPost]
        [Route("CreateNewUser")]
        public UserDtoApi CreateNewUser(UserDtoApi userDtoApi)
        {
            AppUser appUser = new AppUser();
            appUser.UserName = userDtoApi.userName;
            appUser.PasswordHash = userDtoApi.password;
            appUser.Email = userDtoApi.Email;

            _baseRepository.AddNew(appUser);

            var Role = _DbContext.UserRoles.FirstOrDefault();
            Role.RoleId = userDtoApi.userRoles;
            Role.UserId = appUser.Id;

            _DbContext.Add(Role);
            _DbContext.SaveChanges();

            return userDtoApi;
        }

    }
}