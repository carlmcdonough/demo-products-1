using api_demo_products.Models;

namespace api_demo_products.Interfaces
{
    public interface IProductRepository
    {
        int SaveProduct(Product newProduct);
        List<Product>? RetrieveProducts();
        Product? RetrieveProduct(int id);
    }
}
