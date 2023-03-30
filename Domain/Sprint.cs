namespace Domain;

public class Sprint
{
    public TeamMember ScrumMaster { get; set; }
    public TeamMember Tester { get; set; }
    public TeamMember ProductOwner { get; set; }
    private readonly List<BacklogItem> _backlogItems = new List<BacklogItem>();
    public IEnumerable<BacklogItem> BacklogItems => _backlogItems.AsReadOnly();

    public Sprint(TeamMember scrumMaster, TeamMember tester, TeamMember productOwner)
    {
        ScrumMaster = scrumMaster;
        Tester = tester;
        ProductOwner = productOwner;
    }

    public void AddBacklogItem(BacklogItem backlogItem)
    {
        if (_backlogItems.Contains(backlogItem)) return;
        
        _backlogItems.Add(backlogItem);
        backlogItem.Sprint = this;
    }
}