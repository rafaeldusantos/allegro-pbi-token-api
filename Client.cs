using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Client
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string ClientId { get; set; }
    public Pbi Pbi { get; set; }
}

public class Pbi
{
    public string ClientId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public Reports Reports { get; set; }
}

public class Reports
{
    public string ReportId { get; set; }
    public string GroupId { get; set; }
}
