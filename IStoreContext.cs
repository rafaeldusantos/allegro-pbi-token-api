using MongoDB.Driver;

public interface IStoreContext
{
    IMongoCollection<Store> Store { get; }
}