using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

public class StoreRepository : IStoreRepository
{
    private readonly IStoreContext _context;
    public StoreRepository(IStoreContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Store>> GetAllStores()
    {
        return await _context
                        .Store
                        .Find(_ => true)
                        .ToListAsync();
    }
    public Task<Store> GetStore(string storeId)
    {
        FilterDefinition<Store> filter = Builders<Store>.Filter.Eq(m => m.storeId, storeId);
        return _context
                .Store
                .Find(filter)
                .FirstOrDefaultAsync();
    }
       
    public async Task Create(Store store)
    {
        await _context.Store.InsertOneAsync(store);
    }
    public async Task<bool> Update(Store store)
    {
        ReplaceOneResult updateResult =
            await _context
                    .Store
                    .ReplaceOneAsync(
                        filter: g => g.id == store.id,
                        replacement: store);
        return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
    }
    public async Task<bool> Delete(string storeId)
    {
        FilterDefinition<Store> filter = Builders<Store>.Filter.Eq(m => m.storeId, storeId);
        DeleteResult deleteResult = await _context
                                            .Store
                                            .DeleteOneAsync(filter);
        return deleteResult.IsAcknowledged
            && deleteResult.DeletedCount > 0;
    }
}