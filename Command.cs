using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Shared;

public abstract class Command
{
    public abstract string Encode();
    public abstract int Id();
}

public class RegisterUserCommand : Command
{
    public string Name { get; set; }
    public string Password { get; set; }

    public RegisterUserCommand(string name, string password)
    {
        this.Name = name;
        this.Password = password;
    }

    public override string Encode()
    {
        return $"{this.Name}:{this.Password}";
    }

    public static Command Decode(string message)
    {
        string[] split = message.Split(":");
        return new RegisterUserCommand(split[0], split[1]);
    }

    public override int Id()
    {
        return 10;
    }
}

public class LoginCommand : Command
{
    public string Name { get; set; }
    public string Password { get; set; }

    public LoginCommand(string name, string password)
    {
        this.Name = name;
        this.Password = password;
    }

    public override string Encode()
    {
        return $"{this.Name}:{this.Password}";
    }

    public static Command Decode(string message)
    {
        string[] split = message.Split(":");
        return new LoginCommand(split[0], split[1]);
    }

    public override int Id()
    {
        return 11;
    }
}


public class SendMessageCommand : Command 
{
    public string Sender { get; set; }
    public string Content { get; set; }

    public SendMessageCommand(string sender, string content)
    {
        this.Sender = sender;
        this.Content = content;
    }

    public override string Encode()
    {
        return $"{this.Sender}:{this.Content}";
    }
    public static Command Decode(string message)
    {
        string[] split = message.Split(":");
        return new SendMessageCommand(split[0], split[1]);
    }
    public override int Id()
    {
        return 12;
    }
}

public class SendPrivateMessageCommand : Command
{
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string Content { get; set; }

    public SendPrivateMessageCommand(string sender, string receiver, string content)
    {
        this.Sender = sender;
        this.Receiver = receiver;
        this.Content = content;
    }

    public override string Encode()
    {
        return $"{this.Sender}:{this.Receiver}:{this.Content}";
    }
    public static Command Decode(string message)
    {
        string[] split = message.Split(":");
        return new SendPrivateMessageCommand(split[0], split[1], split[2]);
    }
    public override int Id()
    {
        return 13;
    }
}

public class LogoutCommand : Command
{
    public string UserName { get; set; }

    public LogoutCommand(string username)
    {
        this.UserName = username;
    }

    public override string Encode()
    {
        return $"{this.UserName}";
    }

    public static Command Decode(string message)
    {
        string[] split = message.Split(":");
        return new LogoutCommand(split[0]);
    }

    public override int Id()
    {
        return 14;
    }
}

public class DisconnectCommand : Command
{
    public override string Encode()
    {
        return string.Empty;
    }
    public static Command Decode(string message)
    {
        return new DisconnectCommand();
    }

    public override int Id()
    {
        return 15;
    }
}