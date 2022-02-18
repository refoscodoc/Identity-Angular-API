using MariaDb.API.Models;

namespace MariaDb.API.DataAccess.Interfaces;

public interface IMariaDbDataAccessProviderProduct
{
    Task<IEnumerable<ProductModel>> GetAllProducts();
    Task<ProductModel> GetProduct(Guid id);
    Task<ProductModel> AddProduct(ProductModel product);
    Task<ProductModel> DeleteProduct(Guid id);
    Task<ProductModel> UpdateProduct(Guid productId, ProductModel product);
}