namespace StoreApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Item
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? _id { get; set; }
    public string itemImage { get; set; } = null!;
    public string itemName { get; set; } = null!;
    public decimal itemPrice { get; set; }
    public string itemCategory { get; set; } = null!;
    public string itemTags { get; set; } = null!;
    public string itemDescription { get; set; } = null!;
}