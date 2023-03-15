namespace Domain;

public class BacklogItem
{
    public string Name { get; set; }
    public TeamMember TeamMember { get; set; }
    public ICollection<BacklogItem> Activities { get; set; } = new List<BacklogItem>();

    internal Sprint Sprint { get; set; }

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

    public BacklogItem(string name, IWriter writer, Sprint sprint, TeamMember teamMember)
    {
        Name = name;
        sprint.AddBacklogItem(this);
        TeamMember = teamMember;
        TodoBacklogState = new TodoBacklogState(writer, this);
        DoingBacklogState = new DoingBacklogState(writer, this);
        ReadyForTestingBacklogState = new ReadyForTestingBacklogState(writer, this);
        TestingBacklogState = new TestingBacklogState(writer, this);
        TestedBacklogState = new TestedBacklogState(writer, this);
        DoneBacklogState = new DoneBacklogState(writer, this);
        _state = TodoBacklogState;
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