using Domain.Exceptions;

namespace Domain;

public abstract class BacklogState
{
    protected readonly IWriter _writer;
    protected readonly BacklogItem _backlogItem;
    public readonly string StateName;

    protected BacklogState(IWriter writer, BacklogItem backlogItem,string stateName)
    {
        _writer = writer;
        _backlogItem = backlogItem;
        StateName = stateName;
    }

    public abstract void ToTodo();
    public abstract void ToDoing();

    public abstract void ToReadyForTesting();
    public abstract void ToTesting();
    public abstract void ToTested();
    public abstract void ToDone();
    protected void CurrentBranchMessage()
    {
        throw new IllegalStateAdvanceException($"This backlog item is already in {StateName}");
    }
    protected void AdvanceState(BacklogState backlogState)
    {
        _writer.WriteLine($"Transferring backlog item from {StateName} to {backlogState.StateName}...");
    }
}