using MariaDb.API.Models;

namespace MariaDb.API.DataAccess.Interfaces;

public interface IMariaDbDataAccessProviderProduct
{
    Task<IEnumerable<ProductModel>> GetAllProducts();
    Task<ProductModel> GetProduct(Guid Id);
    Task<ProductModel> AddProduct(ProductModel product);
}