namespace Domain.Branches;

public class GitBranch : IBranch
{
    private readonly string _name;
    private readonly string _upstream;
    private readonly IWriter _writer;
    
    public GitBranch(string name, string upstream, IWriter writer)
    {
        _name = name;
        _upstream = upstream;
        _writer = writer;
    }

    public void pull()
    {
        _writer.WriteLine($"Pulling {_name} from {_upstream}");
    }
}