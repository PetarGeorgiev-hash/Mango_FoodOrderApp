using AutoMapper;
using Mango.Services.AuthApi.Data;
using Mango.Services.AuthApi.Dto;
using Mango.Services.AuthApi.IServices;
using Mango.Services.AuthApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;


        public AuthService(AppDbContext appDbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IMapper mapper, IJwtTokenService jwtTokenService)
        {
            _appDbContext = appDbContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<bool> AssignRole(string email, string role)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user == null)
            {
                return false;
            }

            var isValidRole = await _roleManager.RoleExistsAsync(role);
            if (!isValidRole)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
            await _userManager.AddToRoleAsync(user, role);
            return true;

        }

        public async Task<LoginResponseDto> Login(LoginDto login)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == login.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, login.Password);
            if(user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            var userDto = _mapper.Map<UserDto>(user);
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenService.GenerateToken(user, roles);

            return new LoginResponseDto() { User =  userDto, Token = token };
        }

        public async Task<LoginResponseDto?> Register(RegisterDto register)
        {
            var user = _mapper.Map<ApplicationUser>(register);

            try
            {
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    var createdUser = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == register.Email);
                    var userDto = _mapper.Map<UserDto>(createdUser);
                    var roles = await _userManager.GetRolesAsync(createdUser);
                    return new LoginResponseDto()
                    {
                        User = userDto,
                        Token = _jwtTokenService.GenerateToken(createdUser, roles)
                    };
                }

                var exception = result.Errors.Select(err => err.Description);
                var error = string.Join(", ",exception);
                throw new Exception(error);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
