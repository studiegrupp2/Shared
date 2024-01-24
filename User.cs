using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace Shared;
public class User 
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

    public string Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public User(string userName, string password)
    {
        this.UserName = userName;
        this.Password = password;
    }
}