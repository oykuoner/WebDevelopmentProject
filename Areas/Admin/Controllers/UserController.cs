using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using LSDCS.Entity.DTOs.MailLog;
using LSDCS.Entity.DTOs.User;
using LSDCS.Entity.Entities;
using LSDCS.Service.Extensions;
using LSDCS.Service.Services.Abstractions;
using LSDCS.Service.Services.Concrete;
using LSDCS.Web.Consts;
using LSDCS.Web.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.ComponentModel.DataAnnotations;
using System.Data;
using static LSDCS.Web.ResultMessages.Message;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace LSDCS.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = $"{RoleConsts.Admin}")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _singInManager;
        private readonly IToastNotification _toastNotification;
        private readonly IValidator<AppUser> _validator;
        private readonly IUserService _userService;


        public UserController(UserManager<AppUser> userManager, IMapper mapper, RoleManager<AppRole> roleManager, IToastNotification toastNotification, IValidator<AppUser> validator, SignInManager<AppUser> singInManager, IUserService userService)
        {
            this._userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _toastNotification = toastNotification;
            _validator = validator;
            _singInManager = singInManager;
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var result = await _userService.GetAllUserWithRoleAsync();

            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> UserAdd()
        {
            var roles = await _userService.GetAllRolesAsync();

            return View(new UserAddDto { Roles = roles });
        }


        [HttpPost]
        public async Task<IActionResult> UserAdd(UserAddDto userAddDto)
        {
            var map = _mapper.Map<AppUser>(userAddDto);
            var validation = await _validator.ValidateAsync(map);
            var roles = await _userService.GetAllRolesAsync();

            if (ModelState.IsValid) // Model geçerliyse
            {
                var result = await _userService.CreateUserAsync(userAddDto);

                if (result.Succeeded) // Kullanıcı başarıyla oluşturulduysa
                {


                    // Başarı mesajı göster
                    _toastNotification.AddSuccessToastMessage(Message.User.Add(userAddDto.Email), new ToastrOptions { Title = "İşlem Başarılı!" });
                    return RedirectToAction("Index", "User", new { Area = "Admin" }); // Kullanıcı listesine yönlendir

                }
                else // Kullanıcı oluşturma başarısız olduysa
                {

                    result.AddToIdentityModelState(this.ModelState);
                    FluentValidationsExtension.AddToModelState(validation, this.ModelState);
                    return View(new UserAddDto { Roles = roles });
                }
            }

            return View(new UserAddDto { Roles = roles });
        }



        [HttpGet]
        public async Task<IActionResult> UserUpdate(int userId)
        {
            var user = await _userService.GetAppUserByIdAsync(userId);
            var roles = await _userService.GetAllRolesAsync();

            var map = _mapper.Map<UserUpdateDto>(user);
            map.Roles = roles;
            return View(map);
        }


        [HttpPost]
        public async Task<IActionResult> UserUpdate(UserUpdateDto userUpdateDto)
        {
            var user = await _userService.GetAppUserByIdAsync(userUpdateDto.Id);
            if (user != null)
            {

                var roles = await _userService.GetAllRolesAsync();
                if (ModelState.IsValid)
                {
                    var map = _mapper.Map(userUpdateDto, user);
                    var validation = await _validator.ValidateAsync(map);
                    if (validation.IsValid)
                    {

                        user.UserName = userUpdateDto.Email;
                        user.SecurityStamp = Guid.NewGuid().ToString();

                        var result = await _userService.UpdateUserAsync(userUpdateDto);

                        if (result.Succeeded)
                        {

                            _toastNotification.AddSuccessToastMessage(Message.User.Update(userUpdateDto.Email), new ToastrOptions { Title = "İşlem Başarılı!" });
                            return RedirectToAction("Index", "User", new { Area = "Admin" }); // Kullanıcı listesine yönlendir
                        }

                        else
                        {
                            result.AddToIdentityModelState(this.ModelState);

                            return View(new UserUpdateDto { Roles = roles });
                        }
                    }
                    else
                    {
                        FluentValidationsExtension.AddToModelState(validation, this.ModelState);
                        return View(new UserUpdateDto { Roles = roles });
                    }


                }

            }
            return NotFound();

        }


        [HttpGet]
        public async Task<IActionResult> HardDelete(int userId)
        {



            var result = await _userService.DeleteUserAsync(userId);

            if (result.identityResult.Succeeded)
            {
                _toastNotification.AddSuccessToastMessage(Message.User.Delete(result.email), new ToastrOptions { Title = "İşlem Başarılı!" });
                return RedirectToAction("Index", "User", new { Area = "Admin" }); // Kullanıcı listesine yönlendir
            }
            else
            {
                // Rol bulunamadıysa hata mesajı göster
                result.identityResult.AddToIdentityModelState(this.ModelState);

            }

            return NotFound();

        }


        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {
            //var user = await _userManager.GetUserAsync(HttpContext.User);
            //var map = _mapper.Map<UserProfileDto>(user);

            var map = await _userService.GetUserProfileAsync();

            return View(map);
        }

        [HttpPost]
        public async Task<IActionResult> UserProfile(UserProfileDto userProfileDto)
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var result = await _userService.UserProfileUpdateAsync(userProfileDto);
                if (result)
                {
                    _toastNotification.AddSuccessToastMessage("Profil güncelleme işlemi tamamlandı.", new ToastrOptions { Title = "İşlem Başarılı!" });
                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Profil güncelleme işlemi tamamlanamadı.", new ToastrOptions { Title = "İşlem Başarısız" });
                    return View();
                }
            }
            else
            {
                return NotFound();
            }


     
        }
    }
}
