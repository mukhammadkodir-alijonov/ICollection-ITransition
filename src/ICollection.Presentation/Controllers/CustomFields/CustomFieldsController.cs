﻿using ICollection.Service.Dtos.CustomFields;
using ICollection.Service.Interfaces.CustomFields;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Controllers.CustomFields
{
    public class CustomFieldsController : Controller
    {
        private readonly ICustomFieldService _customFieldService;

        public CustomFieldsController(ICustomFieldService customFieldService)
        {
            this._customFieldService = customFieldService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("createcustomfieldforcollection")]
        public async Task<IActionResult> CreateCustomFieldForCollection(CustomFieldDto customFieldDto)
        {
            try
            {
                var success = await _customFieldService.CreateCustomFieldForCollectionAsync(customFieldDto);
                SetTempMessage(success, "CustomField created successfully", "Failed");
                return View(success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while creating the custom field: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpPost("createcustomfieldforitem")]
        public async Task<IActionResult> CreateCustomFieldForItem(CustomFieldDto customFieldDto)
        {
            try
            {
                var success = await _customFieldService.CreateCustomFieldForItemAsync(customFieldDto);
                SetTempMessage(success, "CustomField created successfully", "Failed");
                return View(success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while creating the custom field: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpDelete("deletecustomfield")]
        public async Task<IActionResult> DeleteCustomField(int id)
        {
            try
            {
                var success = await _customFieldService.DeleteCustomFieldAsync(id);
                SetTempMessage(success, "CustomField deleted successfully", "Failed");
                return View(success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the custom field: {ex.Message}";
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