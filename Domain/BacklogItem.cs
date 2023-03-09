namespace Domain;

public class BacklogItem
{
    private IWriter Writer;
    public BacklogState State { get; set; }
    public TodoBacklogState TodoBacklogState { get; set; }
    public DoingBacklogState DoingBacklogState { get; set; }
    public ReadyForTestingBacklogState ReadyForTestingBacklogState { get; set; }
    public TestingBacklogState TestingBacklogState { get; set; }
    public TestedBacklogState TestedBacklogState { get; set; }
    
    public DoneBacklogState DoneBacklogState { get; set; }

    public BacklogItem(IWriter writer)
    {
        Writer = writer;
        TodoBacklogState = new TodoBacklogState(Writer, this);
        DoingBacklogState = new DoingBacklogState(Writer, this);
        ReadyForTestingBacklogState = new ReadyForTestingBacklogState(Writer, this);
        TestingBacklogState = new TestingBacklogState(Writer, this);
        TestedBacklogState = new TestedBacklogState(Writer, this);
        DoneBacklogState = new DoneBacklogState(Writer, this);
        State = new TodoBacklogState(writer, this);
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