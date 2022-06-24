using Microsoft.AspNetCore.Mvc;
using eCommerceAPI.Models;
using eCommerceAPI.Services;
using Newtonsoft.Json;

namespace eCommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;
        private ProductServices productsService;

        public ProductsController( ILogger<ProductsController> logger, IConfiguration configuration )
        {
            _logger = logger;
            productsService = new ProductServices(configuration);
        }

        [HttpGet]
        [Route("all")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult GetAllProducts( )
        {
            var message = string.Empty;
            List< Product> data = new List<Product>();

            try
            {
                data = productsService.GetAll();
            }
            catch ( Exception ex )
            {
                message = ex.Message;
            }
            return Ok(new { data, message });
        }

        [HttpGet]
        [Route("all/{categoryId}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult GetAllProductsByCategory( [FromRoute] int categoryId )
        {
            var message = string.Empty;
            List<Product> data = new List<Product>();
            try
            {
                data = productsService.GetByCategory(categoryId);
            }
            catch ( Exception ex )
            {

                throw ex.GetBaseException();
            }
            return Ok(new { data, message });
        }

        [HttpGet]
        [Route("{id}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult GetProductByID( [FromRoute] int id )
        {
            var message = string.Empty;
            Product data;
            try
            {
                data = productsService.Get(id);
            }
            catch ( Exception ex )
            {
                throw ex.GetBaseException();
            }
            return Ok(new { data, message });
        }

        [HttpGet]
        [Route("Delete/{id}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult DeleteCategoryByID( [FromRoute] int id )
        {
           var message = string.Empty;

           try
           {
               productsService.Delete(id);
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
        public IActionResult EditCategoryByID( [FromRoute] int id , [FromBody] Product product )
        {

           string message = "Updated successfully";
           Product data = null;
           try
           {
                productsService.Update(product, id);

            }
           catch (System.Exception ex)
           {

            throw ex.GetBaseException();
           }

            return Ok(new { message, data });
        }

        [HttpPost]
        [Route("create")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult CreateCategory( [FromBody] Product product )
        {

            string message = "Created successfully";
            Product data = null;
           try
           {
                productsService.Create(product);

            }
           catch (System.Exception ex)
           {

            throw ex.GetBaseException();
           }

           return Ok(new { message , data });
        }

        [HttpPost]
        [Route("search/{searchTerm}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult SearchProduct( [FromRoute] string searchTerm )
        {
            var message = string.Empty;
            List<Product> data = new List<Product>();

            try
            {
                data = productsService.SearchProduct(searchTerm);
            }
            catch ( Exception ex )
            {
                message = ex.Message;
            }

            return Ok(new { data, message });
        }
    }
}