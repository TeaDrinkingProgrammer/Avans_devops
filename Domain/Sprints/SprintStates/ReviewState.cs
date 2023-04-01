using Domain.Exceptions;

namespace Domain.Sprints.SprintStates;

public class ReviewState : SprintState
{
    private new ReviewSprint Sprint { get; set; }

    public ReviewState(ReviewSprint sprint) : base(sprint)
    {
        Sprint = sprint;
    }

    public override bool RunPipeline()
    {
        throw new InvalidOperationException();
    }

    public override void CancelSprint()
    {
        throw new IllegalStateAdvanceException();
    }

    public override void setState()
    {
        if (Sprint.SprintReview == null)
        {
            throw new IllegalStateAdvanceException();
        }
    }
}