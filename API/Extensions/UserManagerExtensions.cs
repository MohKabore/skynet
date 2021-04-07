using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public  static class UserManagerExtensions
    {
        public static async Task<AppUser> FindUserByClaimsPrincipleWithAddressAsync(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
                var email = user.FindFirstValue(ClaimTypes.Email);
                return await input.Users.Include(a=> a.Address).SingleOrDefaultAsync(a=>a.Email == email);
        }

        public static async Task<AppUser> FindByEmailClaimsPrincipe(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
                var email = user.FindFirstValue(ClaimTypes.Email);
                return await input.Users.SingleOrDefaultAsync(a=>a.Email == email);
        }
    }
}