using Domain.Exceptions;

namespace Domain;

public class DoingBacklogState : BacklogState
{
    public DoingBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Doing")
    {
    }

    public override void SetState()
    {
        _backlogItem.Sprint.Project.ScrumMaster.Notify($"Backlogitem {_backlogItem.Name} has been moved to Doing");
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