using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.API.Models;

public class TickerModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId? Id { get; set; }
    public string Company { get; set; }
    public decimal Value { get; set; }
    
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime Date { get; set; }
}