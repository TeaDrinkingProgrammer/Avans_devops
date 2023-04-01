namespace Domain.Branches;

public class GitRepository : IRepository
{
    private readonly string _remote;
    private readonly List<IBranch> _branches;
    private readonly IWriter _writer;

    public GitRepository(string remote, IWriter writer)
    {
        _remote = remote;
        _writer = writer;
        _branches = new List<IBranch>();
    }

    public IBranch Branch(string branchName)
    {
        var upstreamBranch = $"{_remote}/{branchName}";
        _branches.Add(new GitBranch(branchName, upstreamBranch, _writer));
        return _branches.Last();
    }
}