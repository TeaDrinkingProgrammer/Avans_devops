using Domain.Exceptions;

namespace Domain.Sprints.SprintStates;

public class ReleasedState : SprintState
{
    private new ReleaseSprint Sprint { get; set; }
    public ReleasedState(ReleaseSprint sprint) : base(sprint)
    {
        Sprint = sprint;
    }

    public override void UploadReview(string review)
    {
        throw new Exception();
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
        //TODO id?
        Sprint.Project.ScrumMaster.Notify("Sprint has been released");
        Sprint.Project.ProductOwner.Notify("Sprint has been released");
    }
}