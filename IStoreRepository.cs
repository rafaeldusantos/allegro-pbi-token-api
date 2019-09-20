using System.Collections.Generic;
using System.Threading.Tasks;

public interface IStoreRepository
{
    Task<IEnumerable<Store>> GetAllStores();
    Task<Store> GetStore(string storeId);
    Task Create(Store store);
    Task<bool> Update(Store store);
    Task<bool> Delete(string storeId);
}
