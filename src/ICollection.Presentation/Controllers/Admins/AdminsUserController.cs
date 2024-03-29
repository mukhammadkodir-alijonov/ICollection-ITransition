using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Users;
using ICollection.Service.Interfaces.Admins;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Controllers.Admins
{
    [Route("adminusers")]
    public class AdminsUserController : Controller
    {
        private readonly IAdminUserService _adminUserService;
        private readonly int _pageSize = 10;

        public AdminsUserController(IAdminUserService adminUserService)
        {
            this._adminUserService = adminUserService;
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(List<int> ids)
        {
            var res = await _adminUserService.DeleteAsync(ids);
            return View(res);
        }
        [HttpPut("block")]
        public async Task<IActionResult> Block(List<int> ids)
        {
            var res = await _adminUserService.BlockAsync(ids);
            return View(res);
        }
        [HttpPut("active")]
        public async Task<IActionResult> Active(List<int> ids)
        {
            var res = await _adminUserService.ActiveAsync(ids);
            return View(res);
        }
        [HttpDelete("deleteimage")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var res = await _adminUserService.DeleteImageAsync(id);
            return View(res);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            var res = await _adminUserService.GetAllAsync(new PaginationParams(page, _pageSize));
            return View(res);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(int id,UserUpdateDto model)
        {
            var res = await _adminUserService.UpdateAsync(id,model);
            return View(res);
        }
    }
}
