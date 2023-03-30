using Domain.Exceptions;

namespace Domain.Sprints.SprintStates;

public class InProgressState : SprintState
{
    public InProgressState(Sprint sprint) : base(sprint)
    {
    }

    public override void UploadReview(string review)
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ToNextState()
    {
        AdvanceState(Sprint.FinishedState);
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