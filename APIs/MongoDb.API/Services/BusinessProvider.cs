using MongoDb.API.DataAccess;
using MongoDb.API.Models;
using MongoDB.Bson;
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
        // return await _context.Tickers.Find(t => t.Company == company).ToListAsync();
        return await _context.Tickers.Find(t => t.Company == company).ToListAsync();
    }

    public async Task<TickerModel> PostNewTicker(TickerModel ticker)
    {
        var newTicker = new TickerModel
        {
            Id = ObjectId.GenerateNewId(),
            Company = ticker.Company,
            Date = ticker.Date,
            Value = ticker.Value
        };
        
        await _context.Tickers.InsertOneAsync(newTicker);

        return newTicker;
    }
}