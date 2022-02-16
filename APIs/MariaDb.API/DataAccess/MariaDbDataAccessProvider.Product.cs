using MariaDb.API.DataAccess.Interfaces;
using MariaDb.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MariaDb.API.DataAccess;

public class MariaDbDataAccessProviderProduct : IMariaDbDataAccessProviderProduct
{
    private readonly MariaDbDataAccess _context;

    public MariaDbDataAccessProviderProduct(MariaDbDataAccess context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductModel>> GetAllProducts()
    {
        return await _context.ProductTable.OrderBy(x => x.ProductId)
            .ToListAsync();
    }
    
    public async Task<ProductModel> GetProduct(Guid Id)
    {
        // return await _context.DataEventRecords
        //     .Include(s => s.SourceInfo)
        //     .FirstAsync(t => t.Id == Id);
        return await _context.ProductTable
            .FirstAsync(t => t.ProductId == Id);
    }
    
    public async Task<ProductModel> AddProduct(ProductModel product)
    {
        _context.ProductTable.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
}