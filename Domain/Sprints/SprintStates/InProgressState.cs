namespace Domain.Sprints.SprintStates;

public class InProgressState : SprintState
{
    public InProgressState(Sprint sprint) : base(sprint)
    {
    }
    
    public override void AddBacklogItem(BacklogItem backlogItem)
    {
        AddBacklogItemImpl(backlogItem);
    }
    
    public override void RemoveBacklogItem(BacklogItem backlogItem)
    {
        RemoveBacklogItemImpl(backlogItem);
    }

    public override void ToNextState()
    {
        AdvanceState(Sprint.FinishedState);
    }

    public override void CancelSprint()
    {
        AdvanceState(Sprint.CancelledState);
    }

    protected override void SetState()
    {
    }
}