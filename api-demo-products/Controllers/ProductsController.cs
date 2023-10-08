using api_demo_products.Helpers;
using api_demo_products.Interfaces;
using api_demo_products.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api_demo_products.Controllers
{
    [ApiController]
    [Route("/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private IProductManager _productManager { get; set; }

        public ProductsController(IProductManager productManager, ILogger<ProductsController> logger)
        {
            _logger = logger;
            _productManager = productManager;
        }

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var addedProduct = _productManager.AddProduct(request);

            if (addedProduct != null)
                return Created(CommonHelpers.GetResourcesLocation(addedProduct.Id.ToString(), HttpContext), addedProduct);

            return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to add a product");
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("")]
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productManager.GetProducts();

            if (products != null && products.Any())
                return Ok(products);
            else
                return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetProduct([FromRoute] int id)
        {
            var product = _productManager.GetProduct(id);

            if (product == null)
                return NotFound();
            else
                return Ok(product);
        }
    }
}