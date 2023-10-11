using api_demo_products.Controllers;
using api_demo_products.Helpers;
using api_demo_products.Interfaces;
using api_demo_products.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace test_api_demo_products
{
    public class ProductsControllerTests
    {
        private Product goodProductResult;
        private List<Product> goodProductsResult;
        private ProductRequest goodProductRequest;
        private ProductRequest badProductRequest;
        ProductsController productsController;

        readonly int goodProductId = 1;
        readonly int badProductId = 0;

        [SetUp]
        public void Setup()
        {

            ILogger<ProductsController> logger = (new Mock<ILogger<ProductsController>>()).Object;
            goodProductResult = new Product
            {
                Id = 1,
                Name = "Coke",
                Description = "Softdrinks Coke Classic 250ml",
                Price = 3
            };

            goodProductsResult = new List<Product>()
            {
                goodProductResult,
                new Product()
                {
                    Id = 2,
                    Name = "Sprite",
                    Description = "Softdrinks Sprite 250ml",
                    Price = 4
                },
            };

            goodProductRequest = new ProductRequest()
            {
                Name = "Coke",
                Description = "Softdrinks Coke Classic 250ml",
                Price = 3
            };

            badProductRequest = new ProductRequest()
            {
                Name = "",
                Description = "",
                Price = 0
            };

            var mockProductManager = new Mock<IProductManager>();

            mockProductManager.Setup(x => x.GetProduct(goodProductId)).Returns(goodProductResult);
            mockProductManager.Setup(x => x.GetProducts()).Returns(goodProductsResult);
            mockProductManager.Setup(x => x.AddProduct(goodProductRequest)).Returns(goodProductResult);

            mockProductManager.Setup(x => x.GetProduct(badProductId)).Returns(default(Product));

            productsController = new ProductsController(mockProductManager.Object ,logger);
        }


        #region GetProductTests
        [Test]
        public void GetProduct_200Ok_Test()
        {
            var result = productsController.GetProduct(goodProductId);
            var expected = new OkObjectResult(goodProductResult);

            Assert.That(result.GetType(), Is.EqualTo(typeof(OkObjectResult)));
            Assert.That(goodProductResult, Is.EqualTo(expected.Value));
        }

        [Test]
        public void GetProduct_404NotFound_Test()
        {
            var result = productsController.GetProduct(badProductId);
            var expected = new NotFoundResult();

            Assert.That(result.GetType(), Is.EqualTo(typeof(NotFoundResult)));
        }

        #endregion

        #region GetProductsTests
        [Test]
        public void GetProducts_200Ok_Test()
        {
            var result = productsController.GetAllProducts();
            var expected = new OkObjectResult(goodProductsResult);

            Assert.That(result.GetType(), Is.EqualTo(typeof(OkObjectResult)));
            Assert.That(goodProductsResult, Is.EqualTo(expected.Value));
        }
        #endregion

        #region AddProductTests
        [Test]
        public void AddProduct_202Created_Test()
        {
            var result = productsController.AddProduct(goodProductRequest);
            var expected = new CreatedResult(string.Empty, goodProductResult);

            Assert.That(result.GetType(), Is.EqualTo(typeof(CreatedResult)));
            Assert.That(goodProductResult, Is.EqualTo(expected.Value));
        }

        [Test]
        public void AddProduct_400BadRequest_Test()
        {
            productsController.ModelState.AddModelError("test-error", "error-message");
            var result = productsController.AddProduct(badProductRequest);
            var expected = new BadRequestResult();

            Assert.That(result.GetType(), Is.EqualTo(typeof(BadRequestResult)));
        }
        #endregion

    }
}