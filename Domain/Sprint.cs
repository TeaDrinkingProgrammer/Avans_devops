using Domain.Notifier;
using Domain.Notifier.Events;

namespace Domain;

public class Sprint
{
    public TeamMember ScrumMaster { get; set; }
    public TeamMember ProductOwner { get; set; }

    private readonly TeamMemberNotifier _notifier;
    
    private readonly List<BacklogItem> _backlogItems = new List<BacklogItem>();
    public IEnumerable<BacklogItem> BacklogItems => _backlogItems.AsReadOnly();

    public Sprint(TeamMember scrumMaster, TeamMember productOwner)
    {
        ScrumMaster = scrumMaster;
        ProductOwner = productOwner;
        _notifier = new TeamMemberNotifier();
    }

    public void AddBacklogItem(BacklogItem backlogItem)
    {
        if (_backlogItems.Contains(backlogItem)) return;
        
        _backlogItems.Add(backlogItem);
        backlogItem.Sprint = this;
    }
    
    public void NotifyScrumMaster(string message)
    {
        _notifier.Notify(new Notification(ScrumMaster, message, "email"));
    }

    public IDisposable Subscribe(IObserver<Notification> observer)
    {
        return _notifier.Subscribe(observer);
    }
}