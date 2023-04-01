namespace Domain.Pipelines;

public class Pipeline : IPipeline
{
    public List<Task> Tasks { get; set; }
    public readonly string Name;
    private readonly IVisitor _visitor;

    public Pipeline(string name, IVisitor visitor)
    {
        Name = name;
        Tasks = new List<Task>();
        
        _visitor = visitor;
    }

    public bool Run()
    {
        try
        {
            Accept(_visitor);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
        
    public void Accept(IVisitor visitor)
    {
        visitor.VisitPipeline(this);
        foreach (var task in Tasks)
        {
            task.Accept(visitor);
        }
    }
}