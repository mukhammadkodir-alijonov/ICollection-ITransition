using DocumentFormat.OpenXml.Wordprocessing;
using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Users;
using ICollection.Service.Interfaces.Admins;
using ICollection.Service.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Areas.Users.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdminUserService _adminUserService;
        private readonly IUserService _userService;
        private readonly int _pageSize = 10;

        public HomeController(IAdminUserService adminUserService,IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._adminUserService = adminUserService;
            this._userService = userService;
        }
        [HttpGet("users")]
        public IActionResult Index()
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext?.User.FindFirst("UserName")?.Value;
            var res = _userService.GetAllAsync(new PaginationParams(1, _pageSize));
            return View(res);
        }
        [HttpDelete("users/delete")]
        public async Task<IActionResult> Delete(List<int> ids)
        {
            var res = await _adminUserService.DeleteAsync(ids);
            return View(res);
        }
        [HttpPut("users/block")]
        public async Task<IActionResult> Block(List<int> ids)
        {
            var res = await _adminUserService.BlockAsync(ids);
            return View(res);
        }
        [HttpPut("users/active")]
        public async Task<IActionResult> Active(List<int> ids)
        {
            var res = await _adminUserService.ActiveAsync(ids);
            return View(res);
        }
        [HttpDelete("users/deleteimage")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var res = await _adminUserService.DeleteImageAsync(id);
            return View(res);
        }
        [HttpGet("users/getall")]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            var res = await _adminUserService.GetAllAsync(new PaginationParams(page, _pageSize));
            return View(res);
        }
        [HttpPut("users/update")]
        public async Task<IActionResult> Update(int id, UserUpdateDto model)
        {
            var res = await _adminUserService.UpdateAsync(id, model);
            return View(res);
        }
    }
}
