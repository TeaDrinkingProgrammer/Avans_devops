using Domain.Notifier.Events;

namespace Domain;

public class Project
{
    private readonly string _title;

    public TeamMember ScrumMaster { get; set; }
    public TeamMember Tester { get; set; }
    public TeamMember ProductOwner { get; set; }
    private readonly TeamMemberNotifier _scrumMasterNotifier;
    private readonly TeamMemberNotifier _testerNotifier;
    private readonly TeamMemberNotifier _productOwnerNotifier;

    public Project(string title, TeamMember scrumMaster, TeamMember tester, TeamMember productOwner)
    {
        _title = title;
        ScrumMaster = scrumMaster;
        Tester = tester;
        ProductOwner = productOwner;
        _scrumMasterNotifier = new TeamMemberNotifier();
        _testerNotifier = new TeamMemberNotifier();
        _productOwnerNotifier = new TeamMemberNotifier();
    }
    public void NotifyScrumMaster(string message)
    {
        _scrumMasterNotifier.Notify(new Notification(ScrumMaster, message ));}
    public IDisposable SubscribeToScrumMaster(IObserver<Notification> observer)
    {
        return _scrumMasterNotifier.Subscribe(observer);
    }
    public void NotifyProductOwner(string message)
    {
        _productOwnerNotifier.Notify(new Notification(ProductOwner, message));
    }
    public IDisposable SubscribeToProductOwner(IObserver<Notification> observer)
    {
        return _productOwnerNotifier.Subscribe(observer);
    }
    public void NotifyTester(string message)
    {
        _testerNotifier.Notify(new Notification(Tester, message));
    }
    public IDisposable SubscribeToTester(IObserver<Notification> observer)
    {
        return _testerNotifier.Subscribe(observer);
    }
}