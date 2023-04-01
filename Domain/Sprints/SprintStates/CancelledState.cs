using Domain.Exceptions;

namespace Domain.Sprints.SprintStates;

public class CancelledState : SprintState
{
    public CancelledState(Sprint sprint) : base(sprint)
    {
    }

    public override void setState()
    {
        Sprint.Project.ScrumMaster.Notify("Sprint has been cancelled");
        Sprint.Project.ProductOwner.Notify("Sprint has been cancelled");
    }
}