using LSDCS.Entity.DTOs.MailLog;
using LSDCS.Entity.DTOs.User;
using LSDCS.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.Services.Abstractions
{
    public interface IUserService
    {


        Task<List<UserDto>> GetAllUserWithRoleAsync();

        Task<List<AppRole>> GetAllRolesAsync();

        Task<IdentityResult> CreateUserAsync(UserAddDto userAddDto);

        Task<AppUser> GetAppUserByIdAsync(int userId);

        Task<IdentityResult> UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task<(IdentityResult identityResult,string? email)> DeleteUserAsync(int userId);


        Task<string> GetUSerRoleAsync(AppUser appUser);

        Task<UserProfileDto> GetUserProfileAsync();
        Task<bool> UserProfileUpdateAsync(UserProfileDto userProfileDto);


      
    }
}
