namespace Domain.Sprints.Export;

public class BacklogItemsListVisitor : ISprintVisitor
{
    private readonly List<string> _content = new(Array.Empty<string>());
    public BacklogItemsListVisitor()
    {
        _content.Add("--------------------");
    }

    public string[] Export()
    {
        return _content.ToArray();
    }

    public void VisitSprint(Sprint sprint)
    {
        //Does nothing
    }

    public void VisitBacklogItem(BacklogItem backlogItem)
    {
        _content.Add($"{backlogItem.Name}");
        _content.Add($"Developer: {backlogItem.Developer.Name}");
        _content.Add($"State: {backlogItem.State.StateName}");
        _content.Add("--------------------");
    }
}