using MariaDb.API.DataAccess.Interfaces;
using MariaDb.API.Models;

namespace MariaDb.API.Services;

public class BusinessProvider
{
    private readonly IMariaDbDataAccessProviderProduct _dataAccessProducts;
    private readonly IMariaDbDataAccessProviderProduct _dataAccessUsers;

    public BusinessProvider(IMariaDbDataAccessProviderProduct dataAccessProducts, IMariaDbDataAccessProviderProduct dataAccessUsers)
    {
        _dataAccessUsers = dataAccessUsers;
        _dataAccessProducts = dataAccessProducts;
    }

    public async Task<object?> GetProduct(Guid id)
    {
        return await _dataAccessProducts.GetProduct(id);
    }

    public async Task<object?> AddProduct(ProductModel product)
    {
        var newProduct = new ProductModel 
        {
            ProductId = Guid.NewGuid(),
            Category = product.Category,
            Manufacturer = product.Manufacturer,
            ProductName = product.ProductName,
            Quantity = product.Quantity,
            PriceTag = product.PriceTag
        };
            
        await _dataAccessProducts.AddProduct(newProduct);

        return newProduct;
    }

    public async Task DeleteProduct(string id)
    {
        var guid = Guid.Parse(id);
        await _dataAccessProducts.DeleteProduct(guid);
    }

    public async Task<IEnumerable<ProductModel>> GetAllProducts()
    {
        var data = await _dataAccessProducts.GetAllProducts();

        var result = data.Select(product => new ProductModel
        {
            ProductId = product.ProductId,
            Category = product.Category,
            Manufacturer = product.Manufacturer,
            ProductName = product.ProductName,
            Quantity = product.Quantity,
            PriceTag = product.PriceTag
        });
        
        return result;
    }
    
    public async Task<ProductModel> UpdateProduct(Guid productId, ProductModel product)
    {
        var newProduct = new ProductModel
        {
            ProductId = product.ProductId,
            Category = product.Category,
            Manufacturer = product.Manufacturer,
            ProductName = product.ProductName,
            Quantity = product.Quantity,
            PriceTag = product.PriceTag
        };

        await _dataAccessProducts.UpdateProduct(product.ProductId, newProduct);
        return newProduct;
    }
}