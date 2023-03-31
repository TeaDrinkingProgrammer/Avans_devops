using Domain.Exceptions;

namespace Domain.Sprints.SprintStates;

public class CancelledState : SprintState
{
    public CancelledState(Sprint sprint) : base(sprint)
    {
    }

    public override void UploadReview(string review)
    {
        throw new InvalidOperationException();
    }

    public override void ToNextState()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ReleaseSprint()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ReviewSprint()
    {
        throw new IllegalStateAdvanceException();
    }

    public override bool RunPipeline()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void CancelSprint()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void setState()
    {
        Sprint.Project.ScrumMaster.Notify("Sprint has been cancelled");
        Sprint.Project.ProductOwner.Notify("Sprint has been cancelled");
    }
}