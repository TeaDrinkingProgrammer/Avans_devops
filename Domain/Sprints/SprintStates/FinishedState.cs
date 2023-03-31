using Domain.Exceptions;

namespace Domain.Sprints.SprintStates;

public class FinishedState : SprintState
{
    public FinishedState(Sprint sprint) : base(sprint)
    {
    }
    
    public override void AddBacklogItem(BacklogItem backlogItem)
    {
        throw new InvalidOperationException();
    }
    public override void RemoveBacklogItem(BacklogItem backlogItem)
    {
        throw new InvalidOperationException();
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
        if (Sprint.GetType() != typeof(ReleaseSprint)) throw new IllegalStateAdvanceException();
        var releaseSprint = (ReleaseSprint) Sprint;
        if (RunPipeline()) AdvanceState(releaseSprint.ReleasedState);
    }

    public override void ReviewSprint()
    {
        if (Sprint.GetType() != typeof(ReviewSprint)) throw new IllegalStateAdvanceException();
        var reviewSprint = (ReviewSprint) Sprint;
        AdvanceState(reviewSprint.ReviewState);
    }

    public override bool RunPipeline()
    {
        if (Sprint.Pipeline == null || !Sprint.Pipeline.Run())
        {
            throw new IllegalStateAdvanceException();
        }
        return true;
    }

    public override void CancelSprint()
    {
        AdvanceState(Sprint.CancelledState);
    }

    public override void setState()
    {
    }
}