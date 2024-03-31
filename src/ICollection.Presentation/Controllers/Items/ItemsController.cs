using DocumentFormat.OpenXml.Office2010.Excel;
using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Items;
using ICollection.Service.Interfaces.Items;
using ICollection.Service.Interfaces.Likes;
using ICollection.Service.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Controllers.Items
{
    [Route("items")]
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly IitemService _iitemService;
        private readonly int _pageSize = 10;
        private readonly ILikeService _likeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public ItemsController(IitemService iitemService,IHttpContextAccessor httpContextAccessor,IUserService userService,ILikeService likeService)
        {
            this._likeService = likeService;
            this._httpContextAccessor = httpContextAccessor;
            this._userService = userService;
            this._iitemService = iitemService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll(int id, int page = 1)
        {
            try
            {
                ViewBag.UserName = _httpContextAccessor.HttpContext?.User.FindFirst("UserName")?.Value;
                ViewBag.CollectionId = id;
                var items = await _iitemService.GetAllItemAsync(id, new PaginationParams(page, _pageSize));
                if (items is null)
                    return View("Index");
                return View("Index",items);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while fetching items: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }

        }
        [HttpGet("create")]
        public IActionResult Create(int id)
        {
            ViewBag.Id = id;
            return View("Create");
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(ItemDto itemDto)
        {
            try
            {
                var success = await _iitemService.CreateItemAsync(itemDto);
                SetTempMessage(success, "Item created successfully", "Failed");
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while creating the item: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var success = await _iitemService.DeleteItemAsync(id);
                SetTempMessage(success, "Item deleted successfully", "Failed");
                return View(success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the item: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpGet("update")]
        public IActionResult Update(int id)
        {
            ViewBag.Id = id;
            return View("Update");
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(int id, ItemDto item)
        {
            try
            {
                var success = await _iitemService.UpdateItemAsync(id, item);
                SetTempMessage(success, "Item updated successfully", "Failed");
                return View(success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while updating the item: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpPost("likeitem")]
        public async Task<IActionResult> LikeItem(int itemId)
        {
            try
            {
                var res = await _likeService.LikeItemAsync(itemId);
                if (res)
                {
                    return RedirectToAction("Index", "Items");
                }
                else
                {
                    TempData["Error"] = "Failed to like item";
                    return RedirectToAction("Index", "Items");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Items");
            }
        }
        [HttpPut("dislikeitem")]
        public async Task<IActionResult> DislikeItem(int itemId)
        {
            try
            {
                var res = await _likeService.DislikeItemAsync(itemId);
                if (res)
                {
                    return RedirectToAction("Index", "Item");
                }
                else
                {
                    TempData["Error"] = "Failed to dislike item";
                    return RedirectToAction("Index", "Item");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Item");
            }
        }
        private void SetTempMessage(bool success, string successMessage, string errorMessage)
        {
            TempData[success ? "SuccessMessage" : "ErrorMessage"] = success ? successMessage : errorMessage;
        }
    }
}
