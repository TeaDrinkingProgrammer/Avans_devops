using Domain.Pipelines;
using Domain.Sprints.SprintStates;

namespace Domain.Sprints;

public class ReleaseSprint : Sprint
{
    public ReleasedState ReleasedState { get;}
    public ReleaseSprint(Project project, TeamMember scrumMaster, IPipeline? pipeline) : base(project, scrumMaster, pipeline)
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