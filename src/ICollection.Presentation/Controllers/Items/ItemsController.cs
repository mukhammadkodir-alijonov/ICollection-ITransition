using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Items;
using ICollection.Service.Interfaces.Items;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Controllers.Items
{
    public class ItemsController : Controller
    {
        private readonly IitemService _iitemService;
        private readonly int _pageSize = 10;

        public ItemsController(IitemService iitemService)
        {
            this._iitemService = iitemService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("getallitem")]
        public async Task<IActionResult> GetAllItems(int page = 1)
        {
            try
            {
                var items = await _iitemService.GetAllItemAsync(new PaginationParams(page, _pageSize));
                if (items is null)
                    return View("Index");
                return View(items);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while fetching items: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }

        }
        [HttpPost("createitem")]
        public async Task<IActionResult> CreateItem(ItemDto itemDto)
        {
            try
            {
                var success = await _iitemService.CreateItemAsync(itemDto);
                SetTempMessage(success, "Item created successfully", "Failed");
                return View(success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while creating the item: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpDelete("deleteitem")]
        public async Task<IActionResult> DeleteItem(int id)
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
        [HttpPut("updateitem")]
        public async Task<IActionResult> UpdateItem(int id, ItemDto item)
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
