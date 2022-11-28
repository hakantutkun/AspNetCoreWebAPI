using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto)
        {
            var user = _mapper.Map<ApiUser>(userDto);
            user.UserName = userDto.EmailAddress;

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if(result.Succeeded)
                await _userManager.AddToRoleAsync(user, "User");

            return result.Errors;
        }

        public async Task<bool> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.EmailAddress);

            if (user is null)
                return default;

            return await _userManager.CheckPasswordAsync(user, loginDto.Password);
        }
    }
}
