using Domain.Exceptions;

namespace Domain;

public class TodoBacklogState : BacklogState
{
    public TodoBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Todo")
    {
    }
    public override void SetState()
    {
        // Doesn't do anything
    }

    public override void ToTodo()
    {
        CurrentBranchMessage();
    }

    public override void ToDoing()
    {
        AdvanceState(_backlogItem.DoingBacklogState);
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
        throw new IllegalStateAdvanceException();
    }
}