using Domain.Exceptions;

namespace Domain;

public class DoneBacklogState : BacklogState
{
    public DoneBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Done")
    {
    }
    public override void SetState()
    {
        // Doesn't do anything
    }

    public override void ToTodo()
    {
        _backlogItem.Sprint.ScrumMaster.Notify($"Backlogitem {_backlogItem.Name} has been moved from Done to Todo");
        AdvanceState(_backlogItem.TodoBacklogState);
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