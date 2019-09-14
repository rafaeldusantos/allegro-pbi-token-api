using allegro_pbi_token_api.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class ClientContext : IClientContext
{
    private readonly IMongoDatabase _db;
    public ClientContext(IOptions<Settings> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        _db = client.GetDatabase(options.Value.Database);
    }
    public IMongoCollection<Client> Client => _db.GetCollection<Client>("client");
}
