using Domain.Exceptions;

namespace Domain.Sprints.SprintStates;

public abstract class SprintState
{
    public Sprint Sprint { get; }
    public string StateName { get; }
    public SprintState(Sprint sprint)
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
    public abstract void CancelSprint();
    public abstract void setState();
}