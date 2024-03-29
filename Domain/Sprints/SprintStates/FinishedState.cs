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
        if (Sprint.Pipeline == null)
        {
            throw new IllegalStateAdvanceException();
        }

        if (!Sprint.Pipeline.Run())
        {
            Sprint.ScrumMaster.Notify("Pipeline failed");
        }
        return true;
    }

    public override void CancelSprint()
    {
        AdvanceState(Sprint.CancelledState);
    }

    protected override void SetState()
    {
        Sprint.ScrumMaster.Notify("Sprint is finished");
    }
}