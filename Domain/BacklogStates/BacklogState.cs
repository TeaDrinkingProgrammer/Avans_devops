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

    public abstract void SetState();
    public virtual void ToTodo()
    {
        throw new IllegalStateAdvanceException();
    }
    public virtual void ToDoing()
    {
        throw new IllegalStateAdvanceException();
    }

    public virtual void ToReadyForTesting()
    {
        throw new IllegalStateAdvanceException();
    }
    
    public virtual void ToTesting()
    {
        throw new IllegalStateAdvanceException();
    }

    public virtual void ToTested()
    {
        throw new IllegalStateAdvanceException();
    }

    public virtual void ToDone()
    {
        throw new IllegalStateAdvanceException();
    }
    
    protected void CurrentBranchMessage()
    {
        throw new IllegalStateAdvanceException($"This backlog item is already in {StateName}");
    }
    protected void AdvanceState(BacklogState backlogState)
    {
        backlogState.SetState();
        _backlogItem.State = backlogState;
        _writer.WriteLine($"Transferring backlog item from {StateName} to {backlogState.StateName}...");
    }
}