using Domain.Exceptions;

namespace Domain;

public class ReadyForTestingBacklogState : BacklogState
{
    public ReadyForTestingBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Ready")
    {
    }
    public override void SetState()
    {
        _backlogItem.NotifyTester($"Backlogitem {_backlogItem.Name} is ready for testing");
    }

    public override void ToTodo()
    {
        _backlogItem.Sprint.NotifyScrumMaster($"Backlogitem {_backlogItem.Name} has been moved from Ready For Testing to Todo");
        AdvanceState(_backlogItem.TodoBacklogState);
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