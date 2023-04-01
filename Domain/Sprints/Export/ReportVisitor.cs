namespace Domain.Sprints.Export;

public abstract class ReportVisitor : ISprintVisitor
{
    protected Sprint Sprint { get; }
    
    protected ReportVisitor(Sprint sprint)
    {
        Sprint = sprint;
    }

    public abstract string[] Export();

    public abstract void VisitSprint(Sprint sprint);
    public abstract void VisitBacklogItem(BacklogItem backlogItem);
}
