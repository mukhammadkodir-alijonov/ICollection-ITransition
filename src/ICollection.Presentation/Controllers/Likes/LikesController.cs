using ICollection.Service.Interfaces.Likes;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Controllers.Likes
{
    [Route("likes")]
    public class LikesController : Controller
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService)
        {
            this._likeService = likeService;
        }
        [HttpPost("likecollection")]
        public async Task<IActionResult> LikeCollection(int collectionId, int userId)
        {
            try
            {
                var res = await _likeService.LikeCollectionAsync(collectionId, userId);
                if (res)
                {
                    return RedirectToAction("Index", "Home");
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
        [HttpPost("likeitem")]
        public async Task<IActionResult> LikeItem(int itemId, int userId)
        {
            try
            {
                var res = await _likeService.LikeItemAsync(itemId, userId);
                if (res)
                {
                    return RedirectToAction("Index", "Item");
                }
                else
                {
                    TempData["Error"] = "Failed to like item";
                    return RedirectToAction("Index", "Item");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Item");
            }
        }
        [HttpPut("dislikecollection")]
        public async Task<IActionResult> DislikeCollection(int collectionId, int userId)
        {
            try
            {
                var res = await _likeService.DislikeCollectionAsync(collectionId, userId);
                if (res)
                {
                    return RedirectToAction("Index", "Collection");
                }
                else
                {
                    TempData["Error"] = "Failed to dislike collection";
                    return RedirectToAction("Index", "Collection");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Collection");
            }
        }
        [HttpPut("dislikeitem")]
        public async Task<IActionResult> DislikeItem(int itemId, int userId)
        {
            try
            {
                var res = await _likeService.DislikeItemAsync(itemId, userId);
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
    }
}
