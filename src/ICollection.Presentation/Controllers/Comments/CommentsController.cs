using ICollection.Service.Dtos.Comments;
using ICollection.Service.Interfaces.Comments;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Controllers.Comments
{
    [Route("comments")]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            this._commentService = commentService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("createcomment")]
        public async Task<IActionResult> CreateComment(CommentDto commentDto)
        {
            try
            {
                var success = await _commentService.CreateCommentAsync(commentDto);
                SetTempMessage(success, "Comment created successfully", "Failed");
                return View(success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while creating the comment: {ex.Message}";
                // Log the exception
                return RedirectToAction("Index", "Home"); // Redirect to the home page with an error message
            }
        }
        [HttpDelete("deletecomment")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var success = await _commentService.DeleteCommentAsync(id);
                SetTempMessage(success, "Comment deleted successfully", "Failed");
                return View(success);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the comment: {ex.Message}";
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
