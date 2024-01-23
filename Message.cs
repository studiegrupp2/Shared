namespace Shared;

public abstract class Message
{
    public abstract string Encode();

    public abstract int Id();
}

public class RegisterUserMessage : Message
{
    public string Name { get; set; }
    public string Password { get; set; }

    public RegisterUserMessage(string name, string password)
    {
        this.Name = name;
        this.Password = password;
    }

    public override string Encode()
    {
        return $"{this.Name}:{this.Password}";
    }

    public static Message Decode(string message)
    {
        string[] split = message.Split(":");
        return new RegisterUserMessage(split[0], split[1]);
    }

    public override int Id()
    {
        return 10;
    }
}

public class LoginMessage : Message
{
    public string Name { get; set; }
    public string Password { get; set; }

    public LoginMessage(string name, string password)
    {
        this.Name = name;
        this.Password = password;
    }

    public override string Encode()
    {
        return $"{this.Name}:{this.Password}";
    }

    public static Message Decode(string message)
    {
        string[] split = message.Split(":");
        return new LoginMessage(split[0], split[1]);
    }

    public override int Id()
    {
        return 11;
    }
}
