using api_demo_products.Interfaces;
using api_demo_products.Models;

namespace api_demo_products.Logic
{
    public class ProductsManager : IProductManager
    {

        private readonly ILogger<ProductsManager> _logger;
        private IProductRepository _productRepository { get; set; }

        public ProductsManager(IProductRepository productRepository, ILogger<ProductsManager> logger)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public Product? AddProduct(ProductRequest request)
        {
            var newProduct = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            var newProductId = _productRepository.SaveProduct(newProduct);

            if (newProductId > 0)
                return GetProduct(newProductId);

            return default;
        }

        public Product? GetProduct(int id)
        {
            return _productRepository.RetrieveProduct(id);
        }

        public List<Product>? GetProducts()
        {
            return _productRepository.RetrieveProducts();
        }
    }
}
