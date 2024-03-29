using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Collections;
using ICollection.Service.Interfaces.Collections;
using ICollection.Service.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Controllers.Collections
{
    [Authorize]
    [Route("collections")]
    public class CollectionsController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly ICollectionService _collectionService;
        private readonly int _pageSize = 10;

        public CollectionsController(ICollectionService collectionService, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._userService = userService;
            this._collectionService = collectionService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext?.User.FindFirst("UserName")?.Value;
            var res = await _userService.GetAllCollectionAsync(new PaginationParams(1, _pageSize));
            return View(res);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync(string name, int page = 1)
        {
            try
            {
                var res = await _collectionService.SearchAsync(new PaginationParams(page, _pageSize), name);
                return View(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CollectionDto collectionCreateDto)
        {
            try
            {
                var success = await _collectionService.CreateCollectionAsync(collectionCreateDto);
                SetTempMessage(success, "Collection created successfully", "Failed");
                if (success is true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while creating the collection: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _collectionService.DeleteCollectionAsync(id);
                SetTempMessage(success, "Collection deleted successfully", "Failed");
                return View("Delete", success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the collection: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpGet("update")]
        public ActionResult Update(int id)
        {
            ViewBag.Id = id;
            return View("Edit");
        }
        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAsync(int id, CollectionUpdateDto collectionUpdateDto)
        {
            try
            {
                var success = await _collectionService.UpdateCollectionAsync(id, collectionUpdateDto);
                SetTempMessage(success, "Collection updated successfully", "Failed");
                return View("Edit", success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while updating the collection: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpGet("gettop")]
        public async Task<IActionResult> TopCollection(int page = 1)
        {
            try
            {
                var res = await _collectionService.TopCollection(new PaginationParams(page, _pageSize));
                return View(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private void SetTempMessage(bool success, string successMessage, string errorMessage)
        {
            TempData[success ? "SuccessMessage" : "ErrorMessage"] = success ? successMessage : errorMessage;
        }
    }
}
