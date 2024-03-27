using DocumentFormat.OpenXml.Wordprocessing;
using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Admins;
using ICollection.Service.Dtos.Users;
using ICollection.Service.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Controllers.Users
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly int _pageSize = 10;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
/*        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync(PaginationParams @params, string name)
        {
            try
            {
                var res = await _userService.(@params, name);
                return View("Index", res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            try
            {
                var user = await _userService.GetAllAysnc(new PaginationParams(page, _pageSize));
                if (user is null)
                    return View("Index");
                return View("Index", user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while fetching users: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpDelete]
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
        [HttpDelete]
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
        [HttpPut]
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
        [HttpPut]
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
        [HttpPut]
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
