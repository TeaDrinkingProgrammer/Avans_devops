using Domain.Exceptions;

namespace Domain.Sprints.SprintStates;

//Pattern used: State
public abstract class SprintState
{
    protected Sprint Sprint { get; }
    protected SprintState(Sprint sprint)
    {
        Sprint = sprint;
    }

    protected void AdvanceState(SprintState sprintState)
    {
        sprintState.SetState();
        Sprint.State = sprintState;
    }
    protected void AddBacklogItemImpl(BacklogItem backlogItem)
    {
        if (Sprint.BacklogItems.Contains(backlogItem)) return;
        
        Sprint.BacklogItems.Add(backlogItem);
        backlogItem.Sprint = Sprint;
    }
    
    protected void RemoveBacklogItemImpl(BacklogItem backlogItem)
    {
        Sprint.BacklogItems.Remove(backlogItem);
    }
    public virtual void AddBacklogItem(BacklogItem backlogItem)
    {
        throw new InvalidOperationException();
    }
    public virtual void RemoveBacklogItem(BacklogItem backlogItem)
    {
        throw new InvalidOperationException();
    }

    public virtual void UploadReview(string review)
    {
        throw new InvalidOperationException();
    }
    public virtual bool RunPipeline()
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

    public virtual void CancelSprint()
    {
        throw new IllegalStateAdvanceException();
    }
    protected abstract void SetState();
}