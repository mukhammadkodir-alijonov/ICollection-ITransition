﻿using ICollection.Domain.Entities.Items;
using ICollection.Service.Interfaces.Tags;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Controllers.Tags
{
    [Route("tags")]
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            this._tagService = tagService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(IEnumerable<string> tags, Item item)
        {
            try
            {
                await _tagService.CreateTagAsync(tags, item);
                return RedirectToAction("Index", "Collection");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Collection");
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(IEnumerable<string> tags, Item item)
        {
            try
            {
                await _tagService.UpdateTagAsync(tags, item);
                return RedirectToAction("Index", "Collection");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Collection");
            }
        }
    }
}
