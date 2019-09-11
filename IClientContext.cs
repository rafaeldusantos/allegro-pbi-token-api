using MongoDB.Driver;

public interface IClientContext
{
    IMongoCollection<Client> Client { get; }
}