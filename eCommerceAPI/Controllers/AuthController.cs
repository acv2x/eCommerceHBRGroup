using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eCommerceAPI.Services;

namespace eCommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]

    public class AuthController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private UserService userService;

        public AuthController(
           ILogger<CategoriesController> logger,
           IConfiguration configuration )
        {
            _logger = logger;
            userService = new UserService(configuration);

        }
    }
}
