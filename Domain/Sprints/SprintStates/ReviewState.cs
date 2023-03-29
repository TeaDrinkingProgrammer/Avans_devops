using System.ComponentModel;
using Domain.Exceptions;

namespace Domain.Sprints.SprintStates;

public class ReviewState : SprintState
{
    private new ReviewSprint Sprint { get; set; }

    public ReviewState(ReviewSprint sprint) : base(sprint)
    {
        Sprint = sprint;
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

    public override void CancelSprint()
    {
        //TODO can a sprint be cancelled after it has been released?
        throw new IllegalStateAdvanceException();
    }

    public override void setState()
    {
        if (Sprint.SprintReview == null)
        {
            throw new IllegalStateAdvanceException();
        }
        // TODO trigger development pipeline
    }
}