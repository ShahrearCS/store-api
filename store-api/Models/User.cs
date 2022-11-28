namespace StoreApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? _id { get; set; }
    public string userName { get; set; } = null!;
    public decimal creditCard { get; set; }
    public string cvv { get; set; } = null!;
    public string emailID { get; set; } = null!;
    public string homeAddress { get; set; } = null!;
    public bool admin { get; set; }
    public string password { get; set; } = null!;
}