using DocumentFormat.OpenXml.Wordprocessing;
using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.Dtos.Users;
using ICollection.Service.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Controllers.Users
{
    [Route("users")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly int _pageSize = 10;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _userService.DeleteAsync(id);
                if (res)
                {
                    return RedirectToAction("Index", res);
                }
                else
                {
                    TempData["Error"] = "Failed to delete user";
                    return RedirectToAction("Index", "User");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "User");
            }
        }
        [HttpDelete("deleteimage")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            try
            {
                var res = await _userService.DeleteImageAsync(id);
                if (res)
                {
                    return RedirectToAction("Index", res);
                }
                else
                {
                    TempData["Error"] = "Failed to delete image";
                    return RedirectToAction("Index", "User");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "User");
            }
        }
        [HttpPut("updateimage")]
        public async Task<IActionResult> UpdateImage(int id, [FromForm] IFormFile image)
        {
            try
            {
                var res = await _userService.ImageUpdateAsync(id, image);
                if (res)
                {
                    return RedirectToAction("Index", res);
                }
                else
                {
                    TempData["Error"] = "Failed to update image";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPut("updateuser")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userDto)
        {
            try
            {
                var res = await _userService.UpdateAsync(id, userDto);
                if (res)
                {
                    return RedirectToAction("Index", res);
                }
                else
                {
                    TempData["Error"] = "Failed to update user";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPut("updatepassword")]
        public async Task<IActionResult> UpdatePassword(int id, PasswordUpdateDto password)
        {
            try
            {
                var res = await _userService.UpdatePasswordAsync(id, password);
                if (res)
                {
                    return RedirectToAction("Index", res);
                }
                else
                {
                    TempData["Error"] = "Failed to update password";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
