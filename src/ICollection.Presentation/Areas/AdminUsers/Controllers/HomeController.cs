using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Users;
using ICollection.Service.Interfaces.Admins;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Areas.Users.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdminUserService _adminUserService;
        private readonly int _pageSize = 10;

        public HomeController(IAdminUserService adminUserService, IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._adminUserService = adminUserService;
        }
        [HttpGet("adminusers")]
        public IActionResult Index()

        {
            ViewBag.UserName = _httpContextAccessor.HttpContext?.User.FindFirst("UserName")?.Value;
            var res = _adminUserService.GetAllAsync(new PaginationParams(1, _pageSize));
            return View("Index", res);
        }
        [HttpDelete("adminusers/delete")]
        public async Task<IActionResult> Delete(List<int> ids)
        {
            var res = await _adminUserService.DeleteAsync(ids);
            return View(res);
        }
        [HttpPut("adminusers/block")]
        public async Task<IActionResult> Block(List<int> ids)
        {
            var res = await _adminUserService.BlockAsync(ids);
            return View(res);
        }
        [HttpPut("adminusers/active")]
        public async Task<IActionResult> Active(List<int> ids)
        {
            var res = await _adminUserService.ActiveAsync(ids);
            return View(res);
        }
        [HttpDelete("adminusers/deleteimage")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var res = await _adminUserService.DeleteImageAsync(id);
            return View(res);
        }
        [HttpGet("adminusers/getall")]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            var res = await _adminUserService.GetAllAsync(new PaginationParams(page, _pageSize));
            return View(res);
        }
        [HttpPut("adminusers/update")]
        public async Task<IActionResult> Update(int id, UserUpdateDto model)
        {
            var res = await _adminUserService.UpdateAsync(id, model);
            return View(res);
        }
    }
}
