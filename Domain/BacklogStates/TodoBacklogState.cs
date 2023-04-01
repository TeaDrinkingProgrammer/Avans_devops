namespace Domain.BacklogStates;

public class TodoBacklogState : BacklogState
{
    public TodoBacklogState(IWriter writer, BacklogItem backlogItem) : base(writer, backlogItem, "Todo")
    {
    }
    public override void SetState()
    {
        // Doesn't do anything
    }

    public override void ToTodo()
    {
        CurrentBranchMessage();
    }

    public override void ToDoing()
    {
        AdvanceState(BacklogItem.DoingBacklogState);
    }
}