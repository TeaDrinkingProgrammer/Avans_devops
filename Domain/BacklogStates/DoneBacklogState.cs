using Domain.Exceptions;

namespace Domain;

public class DoneBacklogState : BacklogState
{
    public DoneBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Todo")
    {
    }

    public override void ToTodo()
    {
        AdvanceState(_backlogItem.TodoBacklogState);
        //TODO SCRUMmasternotificatie
    }

    public override void ToDoing()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ToReadyForTesting()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ToTesting()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ToTested()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ToDone()
    {
        CurrentBranchMessage();
    }
}