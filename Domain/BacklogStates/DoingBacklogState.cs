using Domain.Exceptions;

namespace Domain.BacklogStates;

public class DoingBacklogState : BacklogState
{
    public DoingBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Doing")
    {
    }

    public override void SetState()
    {
        BacklogItem.Sprint?.ScrumMaster.Notify($"Backlogitem {BacklogItem.Name} has been moved to Doing");
    }

    public override void ToDoing()
    {
        CurrentBranchMessage();
    }

    public override void ToReadyForTesting()
    {
        AdvanceState(BacklogItem.ReadyForTestingBacklogState);
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