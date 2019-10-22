using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly IOrganizationContext _context;
    public OrganizationRepository(IOrganizationContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Organization>> GetAllOrganizations()
    {
        return await _context
                        .Organization
                        .Find(_ => true)
                        .ToListAsync();
    }
    public Task<Organization> GetOrganization(string organizationId)
    {
        FilterDefinition<Organization> filter = Builders<Organization>.Filter.Eq(m => m.organizationId, organizationId);
        return _context
                .Organization
                .Find(filter)
                .FirstOrDefaultAsync();
    }
       
    public async Task Create(Organization organization)
    {
        await _context.Organization.InsertOneAsync(organization);
    }
    public async Task<bool> Update(Organization organization)
    {
        ReplaceOneResult updateResult =
            await _context
                    .Organization
                    .ReplaceOneAsync(
                        filter: g => g.id == organization.id,
                        replacement: organization);
        return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
    }
    public async Task<bool> Delete(string organizationId)
    {
        FilterDefinition<Organization> filter = Builders<Organization>.Filter.Eq(m => m.organizationId, organizationId);
        DeleteResult deleteResult = await _context
                                            .Organization
                                            .DeleteOneAsync(filter);
        return deleteResult.IsAcknowledged
            && deleteResult.DeletedCount > 0;
    }
}