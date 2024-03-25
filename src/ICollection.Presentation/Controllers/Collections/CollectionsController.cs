using ICollection.Service.Common.Utils;
using ICollection.Service.Dtos.Collections;
using ICollection.Service.Interfaces.Collections;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Controllers.Collections
{
    [Route("collections")]
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

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllCollections(int page = 1)
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
        [HttpPost("createcollection")]
        public async Task<IActionResult> CreateCollection(CollectionDto collectionCreateDto)
        {
            try
            {
                var success = await _collectionService.CreateCollectionAsync(collectionCreateDto);
                SetTempMessage(success, "Collection created successfully", "Failed");
                return View(success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while creating the collection: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpDelete("deletecollection")]
        public async Task<IActionResult> DeleteCollection(int id)
        {
            try
            {
                var success = await _collectionService.DeleteCollectionAsync(id);
                SetTempMessage(success, "Collection deleted successfully", "Failed");
                return View(success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the collection: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpPatch("updatecollection")]
        public async Task<IActionResult> UpdateCollection(int id, CollectionDto collectionUpdateDto)
        {
            try
            {
                var success = await _collectionService.UpdateCollectionAsync(id, collectionUpdateDto);
                SetTempMessage(success, "Collection updated successfully", "Failed");
                return View(success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while updating the collection: {ex.Message}";
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
