using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Areas.Users.Controllers
{
    [ApiController]
    [Area("adminusers")]
    [Authorize(Roles = "admin")]
    public class BaseController : Controller
    {
    }
}
