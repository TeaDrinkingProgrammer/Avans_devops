namespace Domain.Sprints.SprintStates;

public class CancelledState : SprintState
{
    public CancelledState(Sprint sprint) : base(sprint)
    {
    }
    protected override void SetState()
    {
        Sprint.ScrumMaster.Notify("Sprint has been cancelled");
        Sprint.Project.ProductOwner.Notify("Sprint has been cancelled");
    }
}