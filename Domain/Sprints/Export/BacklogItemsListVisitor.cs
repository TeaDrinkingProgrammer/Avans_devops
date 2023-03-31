namespace Domain.Sprints.Export;

public class BacklogItemsListVisitor : ReportVisitor
{
    private readonly List<string> _content = new List<string>(Array.Empty<string>());
    public BacklogItemsListVisitor(Sprint sprint) : base(sprint)
    {
        _content.Add("--------------------");
    }

    public override string[] Export()
    {
        var result = new List<string>();
        for (var i = 0; i < _content.Count; i++)
        {
        }

        return _content.ToArray();
    }

    public override void VisitSprint(Sprint sprint)
    {
        //Does nothing
    }

    public override void VisitBacklogItem(BacklogItem backlogItem)
    {
        _content.Add($"{backlogItem.Name}");
        _content.Add($"Developer: {backlogItem.Developer.Name}");
        _content.Add($"State: {backlogItem.State.StateName}");
        _content.Add("--------------------");
    }
}