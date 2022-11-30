namespace StoreApi.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StoreApi.Models;

public class StoreService
{
    private readonly IMongoCollection<Item> _itemsCollection;
    private readonly IMongoCollection<User> _usersCollection;
    private readonly IMongoCollection<UsersWishList> _usersWishListCollection;

    public StoreService(
        IOptions<StoreDatabaseSettings> storeDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            storeDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            storeDatabaseSettings.Value.DatabaseName);

        _itemsCollection = mongoDatabase.GetCollection<Item>(
            storeDatabaseSettings.Value.ItemsCollectionName);

        _usersCollection = mongoDatabase.GetCollection<User>(
            storeDatabaseSettings.Value.UsersCollectionName);

        _usersWishListCollection = mongoDatabase.GetCollection<UsersWishList>(
            storeDatabaseSettings.Value.UsersWishListCollectionName);
    }

    public async Task<List<Item>> GetAllItemsAsync() =>
        await _itemsCollection.Find(_ => true).ToListAsync();

    public async Task<Item?> GetItemAsync(string _id) =>
        await _itemsCollection.Find(x => x._id == _id).FirstOrDefaultAsync();

    public async Task CreateItemAsync(Item newItem) =>
        await _itemsCollection.InsertOneAsync(newItem);

    public async Task UpdateItemAsync(string _id, Item updatedItem) =>
        await _itemsCollection.ReplaceOneAsync(x => x._id == _id, updatedItem);

    public async Task RemoveItemAsync(string _id) =>
        await _itemsCollection.DeleteOneAsync(x => x._id == _id);

    public async Task<List<User>> GetAllUsersAsync() =>
        await _usersCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetUserAsync(string _id) =>
        await _usersCollection.Find(x => x._id == _id).FirstOrDefaultAsync();

    public async Task CreateUserAsync(User newUser) =>
        await _usersCollection.InsertOneAsync(newUser);

    public async Task UpdateUserAsync(string _id, User updatedUser) =>
        await _usersCollection.ReplaceOneAsync(x => x._id == _id, updatedUser);

    public async Task RemoveUserAsync(string _id) =>
        await _usersCollection.DeleteOneAsync(x => x._id == _id);

    public async Task<List<UsersWishList>> GetAllUsersWishListAsync() =>
        await _usersWishListCollection.Find(_ => true).ToListAsync();

    public async Task<UsersWishList?> GetUserWishListAsync(string _id) =>
        await _usersWishListCollection.Find(x => x._id == _id).FirstOrDefaultAsync();

    public async Task CreateUserWishListAsync(UsersWishList newUserWishList) =>
        await _usersWishListCollection.InsertOneAsync(newUserWishList);

    public async Task UpdateUserWishListAsync(string _id, UsersWishList updatedUserWishList) =>
        await _usersWishListCollection.ReplaceOneAsync(x => x._id == _id, updatedUserWishList);

    public async Task RemoveUserWishListAsync(string _id) =>
        await _usersWishListCollection.DeleteOneAsync(x => x._id == _id);

}