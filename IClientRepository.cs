using System.Collections.Generic;
using System.Threading.Tasks;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllClients();
    Task<Client> GetClient(string name);
    Task Create(Client client);
    Task<bool> Update(Client game);
    Task<bool> Delete(string name);
}
