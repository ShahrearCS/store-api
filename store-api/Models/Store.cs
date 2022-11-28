namespace StoreApi.Models;

public class StoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ItemsCollectionName { get; set; } = null!;
    public string UsersCollectionName { get; set; } = null!;
    public string UsersWishListCollectionName { get; set; } = null!;
}