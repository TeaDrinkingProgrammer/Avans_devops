using Domain.Notifier.Events;
using Domain.Sprints.Export;
using Domain.Pipelines;
using Domain.Sprints.SprintStates;

namespace Domain.Sprints;

public abstract class Sprint : ISprintVisitable
{
    private readonly DateOnly _date;

    public Project Project { get; }
    
    public IPipeline? Pipeline { get; set; }

    public ICollection<BacklogItem> BacklogItems { get; set; } = new List<BacklogItem>();
    
    public SprintState State { get; set; }
    public PlannedState PlannedState { get; }
    public InProgressState InProgressState { get; }
    public FinishedState FinishedState { get; }
    public CancelledState CancelledState { get; }
    
    protected Sprint(DateOnly date, Project project)
    {
        _date = date;
        Project = project;
        
        PlannedState = new PlannedState(this);
        InProgressState = new InProgressState(this);
        FinishedState = new FinishedState(this);
        CancelledState = new CancelledState(this);
        
        State = PlannedState;
    }

    public abstract void AddBacklogItem(BacklogItem backlogItem);
    public abstract void RemoveBacklogItem(BacklogItem backlogItem);
    public void RunPipeline()
    {
        State.RunPipeline();
    }
    public abstract void ToNextState();
    public abstract void CancelSprint();
    public void Accept(ISprintVisitor visitor)
    {
        visitor.VisitSprint(this);
        foreach (var backlogItem in BacklogItems)
        {
            backlogItem.Accept(visitor);
        }
    }
}
