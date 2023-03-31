using Domain.Exceptions;

namespace Domain.Sprints.SprintStates;

public abstract class SprintState
{
    protected Sprint Sprint { get; }
    protected SprintState(Sprint sprint)
    {
        Sprint = sprint;
    }
    public void AddBacklogItem(BacklogItem backlogItem)
    {
        if (Sprint.BacklogItems.Contains(backlogItem)) return;
        
        Sprint.BacklogItems.Add(backlogItem);
        backlogItem.Sprint = Sprint;
    }
    public void RemoveBacklogItem(BacklogItem backlogItem)
    {
        Sprint.BacklogItems.Remove(backlogItem);
    }

    protected void AdvanceState(SprintState sprintState)
    {
        sprintState.setState();
        Sprint.State = sprintState;
    }

    public abstract void UploadReview(string review);
    public abstract void ToNextState();
    public abstract void ReleaseSprint();
    public  abstract void ReviewSprint();
    public abstract bool RunPipeline();
    public abstract void CancelSprint();
    public abstract void setState();
}