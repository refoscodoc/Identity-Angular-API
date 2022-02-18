using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.API.Models;

public class TickerModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid TickerId { get; set; }
    public string Company { get; set; }
    public double Value { get; set; }
    public DateTime Date { get; set; }
}