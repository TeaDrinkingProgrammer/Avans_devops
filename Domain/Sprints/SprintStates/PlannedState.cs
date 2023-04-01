using Domain.Exceptions;

namespace Domain.Sprints.SprintStates;

public class PlannedState : SprintState
{
    public PlannedState(Sprint sprint) : base(sprint)
    {
    }
    
    public override void AddBacklogItem(BacklogItem backlogItem)
    {
        if (Sprint.BacklogItems.Contains(backlogItem)) return;
        
        Sprint.BacklogItems.Add(backlogItem);
        backlogItem.Sprint = Sprint;
    }
    public override void RemoveBacklogItem(BacklogItem backlogItem)
    {
        Sprint.BacklogItems.Remove(backlogItem);
    }

    public override void ToNextState()
    {
        AdvanceState(Sprint.InProgressState);
    }

    public override bool RunPipeline()
    {
        throw new InvalidOperationException();
    }

    public override void CancelSprint()
    {
        AdvanceState(Sprint.CancelledState);
    }

    public override void setState()
    {
    }
}