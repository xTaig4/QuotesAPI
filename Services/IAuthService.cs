using QuoteAPI.Entities;
using QuoteAPI.Models;

namespace QuoteAPI.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDTO request);
        Task<string?> LoginAsync(UserDTO request);
    }
}
