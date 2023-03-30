using Domain.Exceptions;

namespace Domain.Sprints.SprintStates;

public class PlannedState : SprintState
{
    public PlannedState(Sprint sprint) : base(sprint)
    {
    }

    public override void UploadReview(string review)
    {
        throw new Exception();
    }

    public override void ToNextState()
    {
        AdvanceState(Sprint.InProgressState);
    }

    public override void ReleaseSprint()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ReviewSprint()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void CancelSprint()
    {
        AdvanceState(Sprint.CancelledState);
    }

    public override void setState()
    {
    }
}