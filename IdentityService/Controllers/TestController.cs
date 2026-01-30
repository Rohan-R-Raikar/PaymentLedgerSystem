using IdentityService.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Constants.Permissions;

namespace IdentityService.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("secure")]
        [RequirePermission(TransactionPermissions.Create)]
        public IActionResult Secure()
        {
            return Ok("You have permission!");
        }
    }
}
