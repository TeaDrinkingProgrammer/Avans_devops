namespace Domain.BacklogStates;

public class TestedBacklogState : BacklogState
{
    public TestedBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Tested")
    {
    }

    public override void SetState()
    {
        // Doesn't do anything
    }

    public override void ToReadyForTesting()
    {
        AdvanceState(BacklogItem.ReadyForTestingBacklogState);
    }

    public override void ToTested()
    {
        CurrentBranchMessage();
    }

    public override void ToDone()
    {
        AdvanceState(BacklogItem.DoneBacklogState);
    }
}