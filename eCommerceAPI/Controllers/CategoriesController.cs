using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eCommerceAPI.Services;
using eCommerceAPI.Models;

namespace eCommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private CategoriesService categoriesService;
        public CategoriesController(
            ILogger<CategoriesController> logger,
            IConfiguration configuration )
        {
            _logger = logger;
            categoriesService = new CategoriesService(configuration);

        }

        [HttpGet]
        [Route("all")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult GetAllCategories()
        {
            var message = string.Empty;
            dynamic data = null;

            try
            {
                data = categoriesService.GetAll();
            }
            catch ( Exception ex )
            {
                message = ex.Message;
            }
            return Ok(new {data, message});
        }

        [HttpGet]
        [Route("{id}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult GetCategoryByID( [FromRoute] int id )
        {
            var message = string.Empty;
            dynamic data = null;
            try
            {
                data = categoriesService.Get(id);
            }
            catch ( System.Exception )
            {

                throw;
            }
            return Ok(new {  data, message });
        }

        [HttpPost]
        [Route("delete/{id}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult DeleteCategoryByID( [FromRoute] int id )
        {
            var message = string.Empty;

            try
            {
                categoriesService.Delete(id);
                message = "Deleted successfully";
            }
            catch ( Exception ex )
            {

                throw ex.GetBaseException();
            }
            return Ok(new { message });
        }

        [HttpPost]
        [Route("edit/{id}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult EditCategoryByID( [FromRoute] int id, [FromBody] Category category)
        {

            string message = "Updated successfully";
            Product data = null;
            try
            {
                categoriesService.Update(category, id);

            }
            catch ( System.Exception ex )
            {

                throw ex.GetBaseException();
            }

            return Ok(new { message, data });
        }

        [HttpPost]
        [Route("create")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult CreateCategory( [FromBody] Category category)
        {

            string message = "Category successfully";
            Product data = null;
            try
            {
                categoriesService.Create(category);

            }
            catch ( System.Exception ex )
            {

                throw ex.GetBaseException();
            }

            return Ok(new { message, data });
        }
    }
}
