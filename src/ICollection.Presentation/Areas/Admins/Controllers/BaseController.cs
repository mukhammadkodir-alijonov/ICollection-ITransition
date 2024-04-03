using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICollection.Presentation.Areas.Admins.Controllers
{
    [ApiController]
    [Area("admins")]
    [Authorize(Roles ="admin")]
    public class BaseController : Controller
    {
    }
}
