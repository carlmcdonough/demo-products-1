using api_demo_products.Models;

namespace api_demo_products.Interfaces
{
    public interface IProductManager
    {
        Product? AddProduct(ProductRequest request);
        List<Product>? GetProducts();
        Product? GetProduct(int id);
    }
}