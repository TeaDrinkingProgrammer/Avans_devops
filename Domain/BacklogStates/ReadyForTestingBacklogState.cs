using Domain.Exceptions;

namespace Domain;

public class ReadyForTestingBacklogState : BacklogState
{
    public ReadyForTestingBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Todo")
    {
        //TODO send notification to tester
    }

    public override void ToTodo()
    {
        AdvanceState(_backlogItem.TodoBacklogState);
        //TODO notification to SCRUMmaster
    }

    public override void ToDoing()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ToReadyForTesting()
    {
        CurrentBranchMessage();
    }

    public override void ToTesting()
    {
        AdvanceState(_backlogItem.TestingBacklogState);
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