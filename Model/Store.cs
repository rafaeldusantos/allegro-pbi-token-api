using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Store
{
    [BsonId]
    public ObjectId id { get; set; }
    public string name { get; set; }
    public string storeId { get; set; }
    [BsonElement("updatedAt")]
    public DateTime updatedAt { get; set; }
    [BsonElement("createdAt")]
    public DateTime createdAt { get; set; }
    public Pbi pbi { get; set; }
}

public class Pbi
{
    public string clientId { get; set; }
    public string userName { get; set; }
    public string password { get; set; }
    public Reports reports { get; set; }
}

public class Reports
{
    public string reportId { get; set; }
    public string groupId { get; set; }
}
