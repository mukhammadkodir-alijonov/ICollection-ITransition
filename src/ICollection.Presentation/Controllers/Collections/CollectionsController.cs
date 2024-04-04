using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Collections;
using ICollection.Service.Interfaces.Collections;
using ICollection.Service.Interfaces.Likes;
using ICollection.Service.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Controllers.Collections
{
    [Route("collections")]
    public class CollectionsController : Controller
    {
        private readonly ILikeService _likeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly ICollectionService _collectionService;
        private readonly int _pageSize = 10;

        public CollectionsController(ICollectionService collectionService, IUserService userService, IHttpContextAccessor httpContextAccessor, ILikeService likeService)
        {
            this._likeService = likeService;
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
                ViewBag.UserName = _httpContextAccessor.HttpContext?.User.FindFirst("UserName")?.Value;
                var res = await _collectionService.SearchAsync(new PaginationParams(page, _pageSize), name);
                return View("Index", res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext?.User.FindFirst("UserName")?.Value;
            return View("Create");
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CollectionDto collectionCreateDto)
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext?.User.FindFirst("UserName")?.Value;
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

        [Authorize]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var success = await _collectionService.DeleteCollectionAsync(id);
                SetTempMessage(success, "Collection deleted successfully", "Failed");
                return RedirectToAction("Index", "Collections");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the collection: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [Authorize]
        [HttpGet("update")]
        public ActionResult Update(int id)
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext?.User.FindFirst("UserName")?.Value;
            ViewBag.Id = id;
            return View("Edit");
        }
        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(int id, CollectionUpdateDto collectionUpdateDto)
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext?.User.FindFirst("UserName")?.Value;
            try
            {
                var success = await _collectionService.UpdateCollectionAsync(id, collectionUpdateDto);
                SetTempMessage(success, "Collection updated successfully", "Failed");
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
                TempData["ErrorMessage"] = $"An error occurred while updating the collection: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Collections"); // Redirect to the home page with an error message
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
        [Authorize]
        [HttpGet("likecollection")]
        public async Task<IActionResult> LikeCollection(int collectionId)
        {
            try
            {
                var res = await _likeService.LikeCollectionAsync(collectionId);
                if (res)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Failed to like collection";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize]
        [HttpGet("dislikecollection")]
        public async Task<IActionResult> DislikeCollection(int collectionId)
        {
            try
            {
                var res = await _likeService.DislikeCollectionAsync(collectionId);
                if (res)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Failed to dislike collection";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        private void SetTempMessage(bool success, string successMessage, string errorMessage)
        {
            TempData[success ? "SuccessMessage" : "ErrorMessage"] = success ? successMessage : errorMessage;
        }
    }
}
