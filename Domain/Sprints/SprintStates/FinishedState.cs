using Domain.Exceptions;

namespace Domain.Sprints.SprintStates;

public class FinishedState : SprintState
{
    public FinishedState(Sprint sprint) : base(sprint)
    {
    }

    public override void UploadReview(string review)
    {
        if (Sprint.GetType() != typeof(ReviewSprint)) throw new IllegalStateAdvanceException();
        
        var reviewSprint = (ReviewSprint) Sprint;
        reviewSprint.SprintReview = review;
    }

    public override void ToNextState()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void ReleaseSprint()
    {
        //TODO: can this be done in a better way?
        if (Sprint.GetType() != typeof(ReleaseSprint)) throw new IllegalStateAdvanceException();
        var releaseSprint = (ReleaseSprint) Sprint;
        AdvanceState(releaseSprint.ReleasedState);
    }

    public override void ReviewSprint()
    {
        //TODO: can this be done in a better way?
        if (Sprint.GetType() != typeof(ReviewSprint)) throw new IllegalStateAdvanceException();
        var reviewSprint = (ReviewSprint) Sprint;
        AdvanceState(reviewSprint.ReviewState);
    }

    public override void CancelSprint()
    {
        AdvanceState(Sprint.CancelledState);
    }

    public override void setState()
    {
    }
}