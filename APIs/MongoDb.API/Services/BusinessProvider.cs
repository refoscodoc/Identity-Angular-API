using MongoDb.API.DataAccess;
using MongoDb.API.Models;
using MongoDB.Driver;

namespace MongoDb.API.Services;

public class BusinessProvider
{
    private readonly TickerContext _context;

    public BusinessProvider(TickerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<IEnumerable<TickerModel>> GetAllTickers(string company)
    {
        return await _context.Tickers.Find(t => true).ToListAsync();
    }
}