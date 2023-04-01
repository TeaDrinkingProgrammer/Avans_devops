using Domain.Sprints.SprintStates;

namespace Domain.Sprints;

public class ReleaseSprint : Sprint
{
    public ReleasedState ReleasedState { get;}
    public ReleaseSprint(DateOnly date, Project project) : base(date, project)
    {
        ReleasedState = new ReleasedState(this);
    }
    public override void AddBacklogItem(BacklogItem backlogItem)
    {
        State.AddBacklogItem(backlogItem);
    }

    public override void RemoveBacklogItem(BacklogItem backlogItem)
    {
        State.RemoveBacklogItem(backlogItem);
    }

    public override void ToNextState()
    {
        State.ToNextState();
    }

    public override void CancelSprint()
    {
        State.CancelSprint();
    }

    public void Release()
    {
        State.ReleaseSprint();
    }
}