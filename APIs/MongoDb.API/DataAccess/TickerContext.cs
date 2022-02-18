using Microsoft.EntityFrameworkCore;
using MongoDb.API.Models;
using MongoDB.Driver;

namespace MongoDb.API.DataAccess;

public class TickerContext : DbContext
{
    public TickerContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
        var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

        Tickers = database.GetCollection<TickerModel>(configuration["DatabaseSettings:CollectionName"]);
        SeedData(Tickers);
    }
    
    public IMongoCollection<TickerModel> Tickers { get; }

    public static void SeedData(IMongoCollection<TickerModel> tickersCollection)
    {
        bool tickerExists = tickersCollection.Find(t => true).Any();
        if (!tickerExists)
        {
            tickersCollection.InsertManyAsync(GetPreconfiguredTickers());    
        }
    }

    private static IEnumerable<TickerModel> GetPreconfiguredTickers()
    {
        Random rnd = new Random();

        return new List<TickerModel>()
        {
            new TickerModel()
            {
                TickerId = Guid.NewGuid(),
                Company = "Sony",
                Date = DateTime.Today,
                Value = rnd.Next()
            },
            new TickerModel()
            {
                TickerId = Guid.NewGuid(),
                Company = "Sony",
                Date = DateTime.Today.AddDays(-1),
                Value = rnd.Next()
            },
            new TickerModel()
            {
                TickerId = Guid.NewGuid(),
                Company = "Sony",
                Date = DateTime.Today.AddDays(-2),
                Value = rnd.Next()
            }
        };
    }
}