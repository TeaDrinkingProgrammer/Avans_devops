using Domain.Exceptions;

namespace Domain;

public class TestingBacklogState : BacklogState
{
    public TestingBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Todo")
    {
    }

    public override void ToTodo()
    {
        throw new IllegalStateAdvanceException();
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
        CurrentBranchMessage();
    }

    public override void ToTested()
    {
        AdvanceState(_backlogItem.TestedBacklogState);
    }

    public override void ToDone()
    {
        throw new IllegalStateAdvanceException();
    }
}