using allegro_pbi_token_api.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class StoreContext : IStoreContext
{
    private readonly IMongoDatabase _db;
    public StoreContext(IOptions<Settings> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        _db = client.GetDatabase(options.Value.Database);
    }
    public IMongoCollection<Store> Store => _db.GetCollection<Store>("store");
}
