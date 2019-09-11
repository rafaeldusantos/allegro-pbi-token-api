using System.Collections.Generic;
using System.Threading.Tasks;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllClients();
    Task<Client> GetClient(string clienId);
    Task Create(Client client);
    Task<bool> Update(Client client);
    Task<bool> Delete(string clienId);
}
