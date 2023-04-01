using Domain.Sprints.Export;
using Domain.Pipelines;
using Domain.Sprints.SprintStates;

namespace Domain.Sprints;

public abstract class Sprint : ISprintVisitable
{
    //FR-5.2
    public Project Project { get; }
    public TeamMember ScrumMaster { get; set; }
    
    public IPipeline? Pipeline { get;}

    public ICollection<BacklogItem> BacklogItems { get;} = new List<BacklogItem>();
    
    public SprintState State { get; set; }
    public PlannedState PlannedState { get; }
    public InProgressState InProgressState { get; }
    public FinishedState FinishedState { get; }
    public CancelledState CancelledState { get; }
    
    protected Sprint(Project project, TeamMember scrumMaster, IPipeline? pipeline)
    {
        Project = project;
        Pipeline = pipeline;
        ScrumMaster = scrumMaster;
        
        //FR-5.3
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
