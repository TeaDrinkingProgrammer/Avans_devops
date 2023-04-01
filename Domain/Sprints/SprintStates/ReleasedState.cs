using Domain.Exceptions;

namespace Domain.Sprints.SprintStates;

public class ReleasedState : SprintState
{
    private new ReleaseSprint Sprint { get; set; }
    public ReleasedState(ReleaseSprint sprint) : base(sprint)
    {
        Sprint = sprint;
    }

    public override void setState()
    {
        Sprint.Project.ScrumMaster.Notify("Sprint has been released");
        Sprint.Project.ProductOwner.Notify("Sprint has been released");
    }
}