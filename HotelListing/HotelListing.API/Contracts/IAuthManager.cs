using HotelListing.API.Models.Users;

namespace HotelListing.API.Contracts
{
    public interface IAuthManager
    {
        Task<bool> Register(ApiUserDto userDto);
    }
}
