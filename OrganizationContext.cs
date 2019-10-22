using allegro_pbi_token_api.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class OrganizationContext : IOrganizationContext
{
    private readonly IMongoDatabase _db;
    public OrganizationContext(IOptions<Settings> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        _db = client.GetDatabase(options.Value.Database);
    }
    public IMongoCollection<Organization> Organization => _db.GetCollection<Organization>("organizations");
}
