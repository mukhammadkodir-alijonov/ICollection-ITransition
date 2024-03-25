using ICollection.Service.Dtos.Tags;
using ICollection.Service.Interfaces.Tags;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Controllers.Tags
{
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            this._tagService = tagService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("createtag")]
        public async Task<IActionResult> CreateTagAsync(TagDto tagDto)
        {
            try
            {
                var res = await _tagService.CreateTagAsync(tagDto);
                if (res)
                {
                    return RedirectToAction("Index", "Collection");
                }
                else
                {
                    TempData["Error"] = "Failed to like collection";
                    return RedirectToAction("Index", "Collection");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Collection");
            }
        }
        [HttpDelete("deletetag")]
        public async Task<IActionResult> DeleteTagAsync(int tagId)
        {
            try
            {
                var res = await _tagService.DeleteTagAsync(tagId);
                if (res)
                {
                    return RedirectToAction("Index", "Collection");
                }
                else
                {
                    TempData["Error"] = "Failed to like collection";
                    return RedirectToAction("Index", "Collection");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Collection");
            }
        }
    }
}
