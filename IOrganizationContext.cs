using MongoDB.Driver;

public interface IOrganizationContext
{
    IMongoCollection<Organization> Organization { get; }
}