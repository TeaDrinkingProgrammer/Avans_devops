using Domain.Branches;
using NSubstitute;

namespace Domain.Test;

public class BranchTest
{
    //FR-22.1
    [Fact]
    public void GitRepositoryShouldReturnGitBranch()
    {
        var repoWriter = Substitute.For<IWriter>();
        var repository = new GitRepository("https://origin.git", repoWriter);
        var branch = repository.Branch("master");
        Assert.IsType<GitBranch>(branch);
    }
    
    //FR-22.2
    [Fact]
    public void SubversionRepositoryShouldReturnSubversionBranch()
    {
        var repoWriter = Substitute.For<IWriter>();
        var repository = new SubversionRepository("svn://svn.repository.com", repoWriter);
        var branch = repository.Branch("master");
        Assert.IsType<SubversionBranch>(branch);
    }
    
    //FR-22.1
    [Fact]
    public void ShouldPullBranchFromGitRepository()
    {
        var branchWriter = Substitute.For<IWriter>();
        var branch = new GitBranch("master", "https://origin.git", branchWriter);
        branch.pull();
        branchWriter.Received().WriteLine("Pulling master from https://origin.git");
    }
    
    //FR-22.2
    [Fact]
    public void ShouldPullBranchFromSubversionRepository()
    {
        var branchWriter = Substitute.For<IWriter>();
        var branch = new SubversionBranch("master", "svn://svn.repository.com", branchWriter);
        branch.pull();
        branchWriter.Received().WriteLine("Pulling master from svn://svn.repository.com");
    }

    [Fact]
    public void ShouldPullBranchWhenLinkedToBacklogItem()
    {
        var branchWriter = Substitute.For<IWriter>();
        var backlogWriter = Substitute.For<IWriter>();
        var branch = new GitBranch("master", "https://origin.git", branchWriter);
        new BacklogItem("Backlog Item 1", backlogWriter, new TeamMember("John Doe"))
        {
            Branch = branch
        };
        branchWriter.Received().WriteLine("Pulling master from https://origin.git");
    }
}