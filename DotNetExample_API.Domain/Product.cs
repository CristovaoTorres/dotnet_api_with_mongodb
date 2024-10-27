using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetExample_API.Domain;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
