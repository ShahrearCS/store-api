namespace StoreApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class UsersWishList
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? _id { get; set; }

    public string userName { get; set; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    public string itemId { get; set; } = null!;
}