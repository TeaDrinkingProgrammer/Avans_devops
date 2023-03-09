using Domain.Exceptions;

namespace Domain;

public class TestedBacklogState : BacklogState
{
    public TestedBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Todo")
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
        AdvanceState(_backlogItem.ReadyForTestingBacklogState);
    }

    public override void ToTesting()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ToTested()
    {
        CurrentBranchMessage();
    }

    public override void ToDone()
    {
        AdvanceState(_backlogItem.DoneBacklogState);
    }
}