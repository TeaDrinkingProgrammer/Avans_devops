using Domain.Notifier.Events;

namespace Domain;

public class BacklogItem
{
    public string Name { get; set; }
    public TeamMember Developer { get; set; }
    
    public TeamMember? Tester { get; set; }
    public ICollection<BacklogItem> Activities { get; set; } = new List<BacklogItem>();

    internal Sprint Sprint { get; set; }
    
    TeamMemberNotifier _notifier;

    private BacklogState _state;
    public BacklogState State
    {
        get => _state;
        set
        {
            value.SetState();
            _state = value;
        }
    }
    public TodoBacklogState TodoBacklogState { get; set; }
    public DoingBacklogState DoingBacklogState { get; set; }
    public ReadyForTestingBacklogState ReadyForTestingBacklogState { get; set; }
    public TestingBacklogState TestingBacklogState { get; set; }
    public TestedBacklogState TestedBacklogState { get; set; }
    
    public DoneBacklogState DoneBacklogState { get; set; }

    public BacklogItem(string name, IWriter writer, Sprint sprint, TeamMember developer, TeamMember? tester = null)
    {
        Name = name;
        _notifier = new TeamMemberNotifier();
        sprint.AddBacklogItem(this);
        Developer = developer;
        Tester = tester;
        TodoBacklogState = new TodoBacklogState(writer, this);
        DoingBacklogState = new DoingBacklogState(writer, this);
        ReadyForTestingBacklogState = new ReadyForTestingBacklogState(writer, this);
        TestingBacklogState = new TestingBacklogState(writer, this);
        TestedBacklogState = new TestedBacklogState(writer, this);
        DoneBacklogState = new DoneBacklogState(writer, this);
        _state = TodoBacklogState;
    }
    
    public void NotifyDeveloper(string message)
    {
        _notifier.Notify(new Notification(Developer, message, "email"));
    }
    
    public void NotifyTester(string message)
    {
        if (Tester != null) _notifier.Notify(new Notification(Tester, message, "email"));
    }
    
    public IDisposable Subscribe(IObserver<Notification> observer)
    {
        return _notifier.Subscribe(observer);
    }

    public void ToTodo()
    {
        State.ToTodo();
    }

    public void ToDoing()
    {
        State.ToDoing();
    }

    public void ToReadyForTesting()
    {
        State.ToReadyForTesting();
    }

    public void ToTesting()
    {
        State.ToTesting();
    }

    public void ToTested()
    {
        State.ToTested();
    }

    public void ToDone()
    {
        State.ToDone();
    }
}