using System.Net;
using System.Net.Sockets;

namespace Shared;

public interface IConnection
{
    void Send(Command command);
    List<Command> Receive();

    User? GetUser();
    void SetUser(User user);
}

public class SocketConnection : IConnection
{
    private Socket socket;
    private User? user;

    public SocketConnection(Socket socket)
    {
        this.socket = socket;
        this.user = null;
    }

    public User? GetUser()
    {
        return this.user;
    }

    public void SetUser(User user)
    {
        this.user = user;
    }

    public static SocketConnection Connect(byte[] ip, int port)
    {
        IPAddress iPAddress = new IPAddress(ip);
        IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, port);

        Socket socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        socket.Connect(iPEndPoint);

        return new SocketConnection(socket);
    }

    public List<Command> Receive()
    {
        List<Command> commands = new List<Command>();
        if (this.socket.Available != 0)
        {
            byte[] buffer = new byte[1024];
            int read = this.socket.Receive(buffer);
            string content = System.Text.Encoding.UTF8.GetString(buffer, 0, read);

            string[] split = content.Split("|");
            for (int i = 0; i < split.Length - 1; i++)
            {
                string packet = split[i];
                string stringId = packet.Substring(0, 2);
                string message = packet.Substring(2);

                if (stringId == "10")
                {
                    commands.Add(RegisterUserCommand.Decode(message));
                }
                else if (stringId == "11")
                {
                    commands.Add(LoginCommand.Decode(message));
                }
                else if (stringId == "12")
                {
                    commands.Add(SendMessageCommand.Decode(message));
                }
                else if (stringId == "13")
                {
                    commands.Add(SendPrivateMessageCommand.Decode(message));
                }
                else if (stringId == "14")
                {
                    commands.Add(LogoutCommand.Decode(message));
                }
                else if (stringId == "15")
                {
                    commands.Add(DisconnectCommand.Decode(message));
                }
            }
        }

        return commands;
    }

    public void Send(Command commands)
    {
        string toSend = commands.Id() + commands.Encode() + "|";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(toSend);
        socket.Send(buffer);
    }
}