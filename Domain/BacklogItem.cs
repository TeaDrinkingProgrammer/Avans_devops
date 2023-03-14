namespace Domain;

public class BacklogItem
{
    public string Name { get; set; }
    private IWriter Writer;

    public Sprint Sprint { get; set; }

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

    public BacklogItem(string name, IWriter writer, Sprint sprint)
    {
        Name = name;
        Writer = writer;
        Sprint = sprint;
        TodoBacklogState = new TodoBacklogState(Writer, this);
        DoingBacklogState = new DoingBacklogState(Writer, this);
        ReadyForTestingBacklogState = new ReadyForTestingBacklogState(Writer, this);
        TestingBacklogState = new TestingBacklogState(Writer, this);
        TestedBacklogState = new TestedBacklogState(Writer, this);
        DoneBacklogState = new DoneBacklogState(Writer, this);
        State = TodoBacklogState;
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