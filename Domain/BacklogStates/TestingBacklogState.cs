using Domain.Exceptions;

namespace Domain.BacklogStates;

public class TestingBacklogState : BacklogState
{
    public TestingBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Testing")
    {
    }

    public override void SetState()
    {
        // Doesn't do anything
    }
    public override void ToTesting()
    {
        CurrentBranchMessage();
    }

    public override void ToTested()
    {
        AdvanceState(BacklogItem.TestedBacklogState);
    }

    public override void ToDone()
    {
        throw new IllegalStateAdvanceException();
    }
}