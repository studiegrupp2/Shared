namespace Shared;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class Message
{
    // [Bson]
    // [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

    public string Id { get; set; }
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string Content { get; set; }

    public Message (string sender, string receiver, string content)
    {
        this.Sender = sender;
        this.Receiver = receiver;
        this.Content = content;
    }
}