using Domain.Exceptions;

namespace Domain.Sprints.SprintStates;

public abstract class SprintState
{
    protected Sprint Sprint { get; }
    protected SprintState(Sprint sprint)
    {
        Sprint = sprint;
    }

    public virtual void AddBacklogItem(BacklogItem backlogItem)
    {
        throw new InvalidOperationException();
    }

    public virtual void RemoveBacklogItem(BacklogItem backlogItem)
    {
        throw new InvalidOperationException();
    }

    protected void AdvanceState(SprintState sprintState)
    {
        sprintState.setState();
        Sprint.State = sprintState;
    }

    public virtual void UploadReview(string review)
    {
        throw new InvalidOperationException();
    }
    
    public virtual void ToNextState()
    {
        throw new IllegalStateAdvanceException();
    }
    public virtual void ReleaseSprint()
    {
        throw new IllegalStateAdvanceException();
    }

    public virtual void ReviewSprint()
    {
        throw new IllegalStateAdvanceException();
    }
    public abstract bool RunPipeline();
    public abstract void CancelSprint();
    public abstract void setState();
}