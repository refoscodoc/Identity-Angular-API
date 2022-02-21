using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.API.Models;

public class TickerModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId? Id { get; set; }
    
    [BsonElement]
    public string Company { get; set; }
    
    [BsonElement]
    public decimal Value { get; set; }
    
    [BsonElement]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime Date { get; set; }
}