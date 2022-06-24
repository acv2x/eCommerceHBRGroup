using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eCommerceAPI.Models;
using eCommerceAPI.Services;

namespace eCommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private UserService usersService;

        public UsersController( ILogger<UsersController> logger, IConfiguration configuration )
        {
            _logger = logger;
            usersService = new UserService(configuration);
        }


        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser( [FromBody] User user )
        {
            string message = "Sign in successful";
            User data = null;
            try
            {
                data = usersService.SignIn(user);
            }
            catch ( Exception ex )
            {

                throw ex.GetBaseException();
            }
            return Ok(new { message, data });
        }


        [HttpPost]
        [Route("signup")]

        public IActionResult SignUp( [FromBody] User user )
        {
            string message = "Sign in successful";
            User data = null;
            try
            {
                data = usersService.SignUp(user);
            }
            catch ( Exception ex )
            {

                throw ex.GetBaseException();
            }
            return Ok(new { message, data });
        }

        [HttpGet]
        [Route("get/all")]
        public IActionResult GetAll()
        {
            string message = string.Empty;
            List<User> data = new List<User>() { };

            try
            {
                data = usersService.GetAll();
            }
            catch ( Exception ex )
            {

                throw ex.GetBaseException();
            }

            return Ok(new { data, message });
        }

        [HttpGet]
        [Route("delete/{ID}")]
        public IActionResult DeleteUser([FromRoute] int ID)
        {
            string message = "Deleted Sucessfully!";
            dynamic data = null;

            try
            {
                usersService.Delete(ID);
            }
            catch ( Exception ex )
            {

                throw ex.GetBaseException();
            }

            return Ok(new { message, data});
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdateUser([FromBody] User user)
        {
            string message = "Update successful";
            User data = null;
            try
            {
              
                data = usersService.Update(user);
            }
            catch ( Exception ex )
            {
                throw ex.GetBaseException();
            }

            return Ok(new { data, message });
        }
    }
}
