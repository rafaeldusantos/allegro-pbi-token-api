using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOrganizationRepository
{
    Task<IEnumerable<Organization>> GetAllOrganizations();
    Task<Organization> GetOrganization(string organizationId);
    Task Create(Organization Organization);
    Task<bool> Update(Organization Organization);
    Task<bool> Delete(string organizationId);
}
