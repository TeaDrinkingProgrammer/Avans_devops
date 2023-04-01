namespace Domain.Branches;

public class SubversionBranch : IBranch
{
    private readonly string _name;
    private readonly string _server;
    private readonly IWriter _writer;
    
    public SubversionBranch(string name, string server, IWriter writer)
    {
        _name = name;
        _server = server;
        _writer = writer;
    }

    public void pull()
    {
        _writer.WriteLine($"Pulling {_name} from {_server}");
    }
}