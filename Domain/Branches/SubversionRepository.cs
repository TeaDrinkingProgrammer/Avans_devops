namespace Domain.Branches;

public class SubversionRepository : IRepository
{
    private readonly string _server;
    private readonly List<IBranch> _branches;
    private readonly IWriter _writer;

    public SubversionRepository(string server, IWriter writer)
    {
        _server = server;
        _writer = writer;
        _branches = new List<IBranch>();
    }

    public IBranch Branch(string branchName)
    {
        var serverBranch = $"{_server}/{branchName}";
        _branches.Add(new SubversionBranch(branchName, serverBranch, _writer));
        return _branches.Last();
    }
}
