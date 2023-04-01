namespace Domain.BacklogStates;

public class ReadyForTestingBacklogState : BacklogState
{
    public ReadyForTestingBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Ready")
    {
    }
    public override void SetState()
    {
        BacklogItem.Sprint?.Project.Tester.Notify($"Backlogitem {BacklogItem.Name} is ready for testing");
    }

    public override void ToTodo()
    {
        BacklogItem.Sprint?.ScrumMaster.Notify($"Backlogitem {BacklogItem.Name} has been moved from Ready For Testing to Todo");
        AdvanceState(BacklogItem.TodoBacklogState);
    }

    public override void ToReadyForTesting()
    {
        CurrentBranchMessage();
    }

    public override void ToTesting()
    {
        AdvanceState(BacklogItem.TestingBacklogState);
    }
}