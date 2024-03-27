using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Collections;
using ICollection.Service.Interfaces.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Controllers.Collections
{
    [Authorize]
    public class CollectionsController : Controller
    {
        private readonly ICollectionService _collectionService;
        private readonly int _pageSize = 10;

        public CollectionsController(ICollectionService collectionService)
        {
            this._collectionService = collectionService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Searchc(string name,int page = 1)
        {
            try
            {
                var res = await _collectionService.SearchAsync(new PaginationParams(page, _pageSize),name);
                return View("/Home/Index",res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            try
            {
                var collections = await _collectionService.GetAllCollectionAsync(new PaginationParams(page, _pageSize));
                if (collections is null)
                    return View("Index");
                return View(collections);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while fetching collections: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }

        }
        [HttpPost]
        public async Task<IActionResult> Create(CollectionDto collectionCreateDto)
        {
            try
            {
                var success = await _collectionService.CreateCollectionAsync(collectionCreateDto);
                SetTempMessage(success, "Collection created successfully", "Failed");
                return View("Create", success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while creating the collection: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _collectionService.DeleteCollectionAsync(id);
                SetTempMessage(success, "Collection deleted successfully", "Failed");
                return View("Delete",success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the collection: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpPatch]
        public async Task<IActionResult> Update(int id, CollectionUpdateDto collectionUpdateDto)
        {
            try
            {
                var success = await _collectionService.UpdateCollectionAsync(id, collectionUpdateDto);
                SetTempMessage(success, "Collection updated successfully", "Failed");
                return View("Edit",success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while updating the collection: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpGet]
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
