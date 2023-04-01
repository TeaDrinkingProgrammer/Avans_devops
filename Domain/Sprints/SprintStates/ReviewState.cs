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

    protected override void SetState()
    {
        if (Sprint.SprintReview == null)
        {
            throw new IllegalStateAdvanceException();
        }
    }
}