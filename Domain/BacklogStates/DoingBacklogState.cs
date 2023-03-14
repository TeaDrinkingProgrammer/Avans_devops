using Domain.Exceptions;

namespace Domain;

public class DoingBacklogState : BacklogState
{
    public DoingBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Doing")
    {
    }

    public override void SetState()
    {
        // Doesn't do anything
    }

    public override void ToTodo()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ToDoing()
    {
        CurrentBranchMessage();
    }

    public override void ToReadyForTesting()
    {
        AdvanceState(_backlogItem.ReadyForTestingBacklogState);
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