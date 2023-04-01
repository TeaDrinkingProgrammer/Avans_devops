namespace Domain.Branches;

public interface IRepository
{
    public IBranch Branch(string branchName);
}