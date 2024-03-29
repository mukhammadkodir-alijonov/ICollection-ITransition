using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Items;
using ICollection.Service.Interfaces.Items;
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

        public ItemsController(IitemService iitemService)
        {
            this._iitemService = iitemService;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll(int id, int page = 1)
        {
            try
            {
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
        public IActionResult Create()
        {
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
        public async Task<IActionResult> Delete(int id)
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
        private void SetTempMessage(bool success, string successMessage, string errorMessage)
        {
            TempData[success ? "SuccessMessage" : "ErrorMessage"] = success ? successMessage : errorMessage;
        }
    }
}
