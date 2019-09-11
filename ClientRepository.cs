using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

public class ClientRepository : IClientRepository
{
    private readonly IClientContext _context;
    public ClientRepository(IClientContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Client>> GetAllClients()
    {
        return await _context
                        .Client
                        .Find(_ => true)
                        .ToListAsync();
    }
    public Task<Client> GetClient(string clientId)
    {
        FilterDefinition<Client> filter = Builders<Client>.Filter.Eq(m => m.clientId, clientId);
        return _context
                .Client
                .Find(filter)
                .FirstOrDefaultAsync();
    }
       
    public async Task Create(Client client)
    {
        await _context.Client.InsertOneAsync(client);
    }
    public async Task<bool> Update(Client client)
    {
        ReplaceOneResult updateResult =
            await _context
                    .Client
                    .ReplaceOneAsync(
                        filter: g => g.id == client.id,
                        replacement: client);
        return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
    }
    public async Task<bool> Delete(string clientId)
    {
        FilterDefinition<Client> filter = Builders<Client>.Filter.Eq(m => m.clientId, clientId);
        DeleteResult deleteResult = await _context
                                            .Client
                                            .DeleteOneAsync(filter);
        return deleteResult.IsAcknowledged
            && deleteResult.DeletedCount > 0;
    }
}