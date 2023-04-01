using Domain.Exceptions;

namespace Domain.BacklogStates;

//Pattern used: State
public abstract class BacklogState
{
    private readonly IWriter _writer;
    protected readonly BacklogItem BacklogItem;
    public readonly string StateName;

    protected BacklogState(IWriter writer, BacklogItem backlogItem,string stateName)
    {
        _writer = writer;
        BacklogItem = backlogItem;
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
        BacklogItem.State = backlogState;
        _writer.WriteLine($"Transferring backlog item from {StateName} to {backlogState.StateName}...");
    }
}