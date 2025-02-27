

using AutoMapper;
using LSDCS.Entity.DTOs.User;
using LSDCS.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LSDCS.Web.Areas.Admin.ViewComponents
{
    public class DashBoardHeaderAdminViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public DashBoardHeaderAdminViewComponent(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var map = _mapper.Map<UserDto>(loggedInUser);

            var role = string.Join("", await _userManager.GetRolesAsync(loggedInUser));
            map.Role = role;

            return View(map);
        }

    }
}
