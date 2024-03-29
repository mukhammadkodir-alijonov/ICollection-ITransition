using ICollection.DataAccess.Interfaces.Common;
using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.Interfaces.Admins;
using ICollection.Service.ViewModels.AdminViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Controllers.Admins
{
    [Route("admins")]
    public class AdminsController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUnitOfWork _unitOfWork;
        public readonly int _pageSize = 10;

        public AdminsController(IAdminService adminService,IUnitOfWork unitOfWork)
        {
            this._adminService = adminService;
            this._unitOfWork = unitOfWork;
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(List<int> ids)
        {
            var res = await _adminService.DeleteAsync(ids);
            return View(res);
        }
        [HttpPatch("block")]
        public async Task<IActionResult> Block(List<int> ids)
        {
            var res = await _adminService.BlockAsync(ids);
            return View(res);
        }
        [HttpPatch("active")]
        public async Task<IActionResult> Active(List<int> ids)
        {
            var res = await _adminService.ActiveAsync(ids);
            return View(res);
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
            var res = await _adminService.UpdateAsync(id,model);
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
