using AutoMapper;
using FluentValidation;
using LSDCS.DataAccess.UnitOfWorks;
using LSDCS.Entity.DTOs.User;
using LSDCS.Entity.Entities;
using LSDCS.Service.Extensions;
using LSDCS.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace LSDCS.Service.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _singInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _user;


        public UserService(IUnitOfWork unitOfWork, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> singInManager, IValidator<AppUser> validator, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _singInManager = singInManager;
            _httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
        }

        public async Task<IdentityResult> CreateUserAsync(UserAddDto userAddDto)
        {
            var map = _mapper.Map<AppUser>(userAddDto);
            map.UserName = userAddDto.Email;
            var result = await _userManager.CreateAsync(map, string.IsNullOrEmpty(userAddDto.Password) ? "" : userAddDto.Password);
            if (result.Succeeded)
            {
                var findRole = await _roleManager.FindByIdAsync(userAddDto.RoleId.ToString());
                await _userManager.AddToRoleAsync(map, findRole.ToString());
                return result;
            }
            else
                return result;
        }

        public async Task<(IdentityResult identityResult, string? email)> DeleteUserAsync(int userId)
        {

            var user = await GetAppUserByIdAsync(userId);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return (result, user.Email);
            }
            else
                return (result, null);
        }

        public async Task<List<AppRole>> GetAllRolesAsync()
        {

            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<List<UserDto>> GetAllUserWithRoleAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var map = _mapper.Map<List<UserDto>>(users);


            foreach (var item in map)
            {
                var findUser = await _userManager.FindByIdAsync(item.Id.ToString());
                var role = string.Join("", await _userManager.GetRolesAsync(findUser));


                item.Role = role;
            }
            return map;
        }

        public async Task<AppUser> GetAppUserByIdAsync(int userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<string> GetUSerRoleAsync(AppUser appUser)
        {
            return string.Join("", await _userManager.GetRolesAsync(appUser));
        }

        public async Task<IdentityResult> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {

            var user = await GetAppUserByIdAsync(userUpdateDto.Id);
            var userRole = await GetUSerRoleAsync(user);
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.RemoveFromRoleAsync(user, userRole);
                var findRole = await _roleManager.FindByIdAsync(userUpdateDto.RoleId.ToString());
                await _userManager.AddToRoleAsync(user, findRole.Name);
                return result;
            }
            else
            {
                return result;
            }
        }



        public async Task<UserProfileDto> GetUserProfileAsync()
        {
            // IHttpContextAccessor üzerinden HttpContext'e ve ardından User'a erişim
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null)
            {
                throw new InvalidOperationException("User is not logged in.");
            }

            var userProfileDto = _mapper.Map<UserProfileDto>(user);
            return userProfileDto;


        }

        public async Task<bool> UserProfileUpdateAsync(UserProfileDto userProfileDto)
        {

            var userId = _user.GetLoggedInUserId();
            var user = await GetAppUserByIdAsync(userId);


            var isVerified = await _userManager.CheckPasswordAsync(user, userProfileDto.CurrentPassword);
            if (isVerified && userProfileDto.NewPassword != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, userProfileDto.CurrentPassword, userProfileDto.NewPassword);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _singInManager.SignOutAsync();
                    await _singInManager.PasswordSignInAsync(user, userProfileDto.NewPassword, true, false); // Kullanıcıyı sistemnden attıktan sonra girişini sağlamış olduk.

                    _mapper.Map(userProfileDto, user);

                  
                    await _userManager.UpdateAsync(user);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (isVerified)
            {
                await _userManager.UpdateSecurityStampAsync(user);

                _mapper.Map(userProfileDto, user);
                await _userManager.UpdateAsync(user);
                return true;
            }
            else
            {
                return false;

            }

           
        }
    }
}
