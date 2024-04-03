using ICollection.DataAccess.Interfaces.Common;
using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.Interfaces.Admins;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Areas.Admins.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdminService _adminService;
        private readonly IUnitOfWork _unitOfWork;
        public readonly int _pageSize = 10;
        public HomeController(IAdminService adminService, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._adminService = adminService;
            this._unitOfWork = unitOfWork;
        }
        [HttpGet("admins")]
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext?.User.FindFirst("UserName")?.Value;
            var res = await _adminService.GetAllAsync(new PaginationParams(1, _pageSize));
            return View("Index", res);
        }
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(List<int> ids)
        {
            var res = await _adminService.DeleteAsync(ids);
            return res ? RedirectToAction("Index", "Home") : NotFound();
        }
        [HttpPost("block")]
        public async Task<IActionResult> Block(List<int> ids)
        {
            var res = await _adminService.BlockAsync(ids);
            return res ? RedirectToAction("Index", "Home") : NotFound();
        }
        [HttpPost("active")]
        public async Task<IActionResult> Active(List<int> ids)
        {
            var res = await _adminService.ActiveAsync(ids);
            return res ? RedirectToAction("Index", "Home") : NotFound();
        }
        [HttpDelete("deleteimage")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var res = await _adminService.DeleteImageAsync(id);
            return View(res);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            var res = await _adminService.GetAllAsync(new PaginationParams(page, _pageSize));
            return View(res);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, AdminUpdateDto model)
        {
            var res = await _adminService.UpdateAsync(id, model);
            return View(res);
        }
        [HttpPatch("updateimage")]
        public async Task<IActionResult> UpdateImage(int id, IFormFile file)
        {
            var res = await _adminService.UpdateImageAsync(id, file);
            return View(res);
        }
        [HttpPut("updatepassword")]
        public async Task<IActionResult> UpdatePassword(int id, PasswordUpdateDto dto)
        {
            var res = await _adminService.UpdatePasswordAsync(id, dto);
            return View(res);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(AdminRegisterDto model)
        {
            var res = await _adminService.CreateAdminAsync(model);
            return View(res);
        }
    }
}
