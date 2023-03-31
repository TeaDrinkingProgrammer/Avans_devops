namespace Domain.Sprints.Export;

public class TeamConsistencyVisitor : ReportVisitor
{
    private string[] content = Array.Empty<string>();
    public TeamConsistencyVisitor(Sprint sprint) : base(sprint)
    {
    }

    public override string[] Export()
    {
        return content;
    }

    public override void VisitSprint(Sprint sprint)
    {
        
    }

    public override void VisitBacklogItem(BacklogItem backlogItem)
    {
        throw new System.NotImplementedException();
    }
}